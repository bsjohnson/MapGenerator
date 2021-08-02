
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeosis {
    public class Map {
        public int width { get; set; }
        public int height { get; set; }
        public int[,] grid { get; set; }

        public Map() : this(GetDefaultWidth(), GetDefaultHeight()) {}

        public Map (int width, int height) {
            this.width = width;
            this.height = height;
            this.grid = new int[width, height];
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
            return $"Map:: width={this.width}, height={this.height}";
        }
        
        public string getGridAsString() {
            var output = new StringBuilder();

            for (int x = 0; x < grid.GetLength(0); x++) {
                for (int y = 0; y < grid.GetLength(1); y++) {
                    if (grid[x,y] > 0) {
                        output.Append((char)(grid[x,y] + 64));
                    } else {
                        output.Append(" ");
                    }
                }
                output.Append("\n");
            }

            return output.ToString();
        }
    }
}