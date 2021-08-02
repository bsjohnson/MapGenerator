using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeosis {
    public class MapGenerator {
        const int RoomBorderThreshold = 5;
        public List<Room> rooms { get; set; }
        private RoomGenerator roomGenerator;
        private List<int> regions;
        private int currentRegion;

        public MapGenerator() : this((int)DateTimeOffset.Now.ToUnixTimeMilliseconds()) {}

        public MapGenerator(int seed) {
            this.roomGenerator = new RoomGenerator(seed);
        }

        public Map generate() 
        {
            var map = new Map();

            this.currentRegion = 0;
            this.rooms = new List<Room>();
            this.regions = new List<int>();

            this.generateRooms(map.width, map.height);
            this.carveRooms(map);

            return map;
        }

        private void generateRooms(int maxXBound, int maxYBound) {
            var roomAttempts = 15;

            do {
                var newRoom = this.roomGenerator.generate(maxXBound, maxYBound);
                if (this.rooms.Any(room => room.intersects(newRoom))) {
                    roomAttempts--;
                } else {
                    this.rooms.Add(newRoom);
                }
            } while (roomAttempts > 0);
        }

        private void carveRooms(Map map) {
            //TODO: Find a way to speed this up.
            foreach (Room room in this.rooms) {
                for (var x = room.x; x <= room.x + room.width; x++) {
                    for (var y = room.y; y <= room.y + room.height; y++) {
                        map.grid[x, y] = this.currentRegion;
                    }
                }
                this.currentRegion++;
            }
        }
    }
}