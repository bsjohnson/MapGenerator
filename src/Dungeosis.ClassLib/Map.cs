
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Dungeosis {
    public class Map {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Grid { get; set; }

        public Map() : this(GetDefaultWidth(), GetDefaultHeight()) {}

        public Map (int width, int height) {
            this.Width = width;
            this.Height = height;
            this.Grid = new int[width, height];
        }

        public bool Contains(Coordinate point) {
             return Contains(point.X, point.Y);
        }

        public bool Contains(int x, int y) {
             return x >= 0 && x < this.Width && y >= 0 && y < this.Height;
        }

        public int GetRegionAt(Coordinate coordinate) {
            return GetRegionAt(coordinate.X, coordinate.Y);
        }

        public int GetRegionAt(int x, int y) {
            if (!Contains(x, y)) throw new ArgumentException($"x={x}, y={y} out of bounds for the map!");

            return this.Grid[x, y];
        }

        public void SetRegionAt(int region, Coordinate coordinate) {
            SetRegionAt(region, coordinate.X, coordinate.Y);
        }

        public void SetRegionAt(int region, int x, int y) {
            if (!Contains(x, y)) throw new ArgumentException($"x={x}, y={y} out of bounds for the map!");

            Grid[x, y] = region;
        }

        public static int GetDefaultHeight() {
            try {
                return Int32.Parse(Environment.GetEnvironmentVariable("MAP_HEIGHT"));
            } catch (FormatException) {
                Console.WriteLine("Unable to parse MAP_HEIGHT from ENV. Using default value.");
                return 240;
            }
        }

        public static int GetDefaultWidth() {
           try {
                return Int32.Parse(Environment.GetEnvironmentVariable("MAP_WIDTH"));
            } catch (FormatException) {
                Console.WriteLine("Unable to parse MAP_WIDTH from ENV. Using default value.");
                return 320;
            } 
        }

        public override string ToString() {
            return $"Map(width={this.Width}, height={this.Height})";
        }
        
        public string GetGridAsString() {
            var output = new StringBuilder();

            for (int y = 0; y < Grid.GetLength(1); y++) {
                for (int x = 0; x < Grid.GetLength(0); x++) {
                    if (Grid[x,y] != 0) {
                        output.Append((char)(this.GetRegionAt(x, y) + 64));
                    } else {
                        output.Append(' ');
                    }
                }
                output.Append(Environment.NewLine);
            }

            return output.ToString();
        }
    }
}