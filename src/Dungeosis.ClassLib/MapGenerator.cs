using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Dungeosis
{
    public class MapGenerator {
        public MapGeneratorConfig Config { get; set; }
        public List<Room> Rooms { get; set; }
        private readonly RoomGenerator roomGenerator;
        private readonly Random random;
        private List<int> regions;
        private int currentRegion;


        public MapGenerator() : this(new MapGeneratorConfig()) {}

        public MapGenerator(MapGeneratorConfig config) {
            this.random = new Random(config.Seed);
            this.roomGenerator = new RoomGenerator(config.Seed);
            this.Config = config;
        }

        public Map Generate() {
            Console.WriteLine("Generating new Map...");
            Map map = new Map(this.Config.Width, this.Config.Height);

            this.currentRegion = 0;
            this.Rooms = new List<Room>();
            this.regions = new List<int>();
            
            GenerateRooms(map.Width, map.Height);
            CarveRooms(map);
            CarveMaze(map);            
            ConnectRegions(map);

            if (Config.CullDeadEnds) RemoveDeadEnds(map);

            Console.WriteLine("Done.");

            return map;
        }

        private int CreateNewRegion() {
            this.regions.Add(++this.currentRegion);
            return this.currentRegion;
        }

        private void GenerateRooms(int maxXBound, int maxYBound) {
            Console.WriteLine("Generating Rooms...");

            int roomAttempts = Config.RoomCollisionThreshold;
            List<Room> discaredRooms = new List<Room>();

            while (roomAttempts > 0) {
                Room newRoom = this.roomGenerator.Generate(maxXBound, maxYBound);
                if (this.Rooms.Find(room => room.Intersects(newRoom)) != null) {
                    roomAttempts--;
                    continue;
                }
                newRoom.Region = this.CreateNewRegion();
                this.Rooms.Add(newRoom);
            }
        }

        private static bool CanCarve(Map map, Coordinate cell, Vector2 direction) {
            var coordToCheck = cell + (direction * 2);
            if (!map.Contains(coordToCheck)) return false;

            return map.GetRegionAt(coordToCheck) == 0;
        }

        private void CarveRooms(Map map) {
            Console.WriteLine("Carving Rooms into Map...");
            //TODO: Investigate a better way to do this.
            foreach (Room room in this.Rooms) {
                for (var x = room.X; x < room.X + room.Width; x++) {
                    for (var y = room.Y; y < room.Y + room.Height; y++) {
                        map.SetRegionAt(room.Region, x, y);
                    }
                }
            }
        }

        private void CarveMaze(Map map) {
            Console.WriteLine("Carving maze...");
            for (var y = 1; y < map.Grid.GetLength(1); y += 2) { // Iterate through each cell of the grid
                for (var x = 1; x < map.Grid.GetLength(0); x += 2) {
                    if (map.Grid[x, y] != 0) continue; // It's already been carved and is not a "solid" cell

                    this.GrowMaze(map, new Coordinate(x, y));
                }
            }
        }

        private void GrowMaze(Map map, Coordinate start) {
            var cells = new List<Coordinate>();
            
            Vector2 lastDirection = Vector2.Zero; // Use Zero vector to force direction pick since Vectors aren't nullable.

            CreateNewRegion();

            map.SetRegionAt(this.currentRegion, start);

            cells.Add(start);

            while(cells.Count > 0) {
                var currentCell = cells.Last();
                var uncarvedCellDirections = new List<Vector2>();

                // Get cells around us that are not already carved out.
                foreach (var direction in Direction.Cardinals) {
                    if (CanCarve(map, currentCell, direction)) {
                        uncarvedCellDirections.Add(direction);
                    }
                }
      
                if (uncarvedCellDirections.Count > 0) {
                    Vector2 direction;

                    bool cannotContinue = !uncarvedCellDirections.Contains(lastDirection);
                    bool shouldRandomlyTurn = random.Next(100) <= Config.CorridorDirectionChangeChance;

                    if (cannotContinue || shouldRandomlyTurn) {
                        direction = uncarvedCellDirections[random.Next(uncarvedCellDirections.Count)];
                    } else {
                        direction = lastDirection;
                    }

                    map.SetRegionAt(this.currentRegion, currentCell + direction);
                    map.SetRegionAt(this.currentRegion, currentCell + (direction * 2));
                    
                    cells.Add(currentCell + (direction * 2));

                    lastDirection = direction;
                } else {
                    cells.RemoveAt(cells.Count - 1); 
                    lastDirection = Vector2.Zero;
                }
            }
        }

        private void ConnectRegions(Map map) {
            Console.WriteLine("Connecting Map regions...");
            Console.WriteLine($"  Total regions to connect: {this.currentRegion}");

            // Get all cells that could be connection points.
            var connectorRegions = GetConnectedRegionDictionary(map);

            var connectors = connectorRegions.Keys.ToList();
            var mergedRegions = new Dictionary<int, int>();
            var openRegions = new HashSet<int>();
            
            foreach (int region in this.regions) {
                mergedRegions[region] = region;
                openRegions.Add(region);
            }

            while (openRegions.Count > 1) {
                if (connectors.Count == 0) {
                    Console.WriteLine("** Available connectors empty, but unmerged regions remain.");
                    Console.WriteLine($"** {String.Join(", ", openRegions)}");
                    break;
                }
                var connector = connectors[random.Next(connectors.Count)];
                map.SetRegionAt('=' - 64, connector);

                var regions = connectorRegions[connector].Select(region => mergedRegions[region]).ToList();
                var destination = regions.First();
                var sources = regions.Skip(1).ToList();

                foreach (int region in this.regions) {
                    if (sources.Contains(mergedRegions[region])) {
                        mergedRegions[region] = destination;
                    }
                }

                openRegions.ExceptWith(sources);
 
                connectors.RemoveAll(coordinate => {
                    if (connector.IsNextTo(coordinate)) return true;
                    
                    var regions = connectorRegions[coordinate].Select(region => mergedRegions[region]).ToHashSet();

                    if (regions.Count > 1) return false;

                    if (random.Next(25) == 1 && !IsNextToRegion(map, coordinate, '=' - 64)) {
                        map.SetRegionAt('=' - 64, coordinate);
                    }

                    return true;
                });
            }
        }

        private static bool IsNextToRegion(Map map, Coordinate coordinate, int region) {
            foreach (Vector2 direction in Direction.Cardinals) {
                var toCheck = coordinate + direction;
                if (map.Contains(toCheck) && map.GetRegionAt(toCheck) == region) {
                    return true;
                } 
            }
            return false;
        }

        private static Dictionary<Coordinate, List<int>> GetConnectedRegionDictionary(Map map) {
            var connectorRegions = new Dictionary<Coordinate, List<int>>();

            for (int y = 1; y < map.Grid.GetLength(1); y++) {
                for(int x = 1; x < map.Grid.GetLength(0); x++) {
                    var connector = new Coordinate(x, y);
                    List<int> connectedRegions = GetConnectedRegions(map, connector);
                    if (connectedRegions.Any()) {
                        connectorRegions[connector] = connectedRegions;
                    }
                }
            }

            return connectorRegions;
        }

        private static List<int> GetConnectedRegions(Map map, Coordinate cell) {
            var connectedRegions = new HashSet<int>();

            if (map.GetRegionAt(cell) != 0) return connectedRegions.ToList();
            
            foreach(Vector2 direction in Direction.Cardinals) {
                var toCheck = cell + direction;

                if (map.Contains(toCheck)) {
                    var region = map.GetRegionAt(toCheck);
                    if (region != 0) {
                        connectedRegions.Add(map.GetRegionAt(toCheck));
                    }
                }
            }

            if (connectedRegions.Count >= 2) {
                return connectedRegions.ToList();
            }

            return new List<int>(); // if the cell is only touching one region it's not "connected", so return an empty list.
        }

        public static void RemoveDeadEnds(Map map) {
            Console.WriteLine("Removing dead ends from Maze...");

            var done = false;

            while (!done) {
                done = true;

                for (int y = 0; y < map.Grid.GetLength(1); y++) {
                    for (int x = 0; x < map.Grid.GetLength(0); x++) {
                        Coordinate cell = new Coordinate(x, y);

                        if (map.GetRegionAt(cell) == 0) continue;

                        var exists = 0;
                        foreach (var direction in Direction.Cardinals) {
                            if (!map.Contains(cell + direction)) continue;
                            if (map.GetRegionAt(cell + direction) != 0) exists++;
                        }

                        if (exists != 1) continue;

                        done = false;
                        map.SetRegionAt(0, cell);
                    }
                }
            }
        }
    }
}