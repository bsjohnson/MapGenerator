using System;
using System.IO;
using Dungeosis;

namespace Dungeosis.ConsoleApp {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Starting Dungeosis.");
            Console.WriteLine("Args: " + (args.Length == 0 ? "none" : String.Join(", ", args)));
            
            instantiateEnvironment();

            var seed = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Console.WriteLine("Seed: " + seed);

            var mapGenerator = new MapGenerator(seed);
            var map = mapGenerator.generate();

            writeMapToFile(map);
        }

        private static async void writeMapToFile(Map map) {
            await File.WriteAllTextAsync("map.txt", map.getGridAsString());
        }

        private static void instantiateEnvironment() {
            int mapWidth = 0;
            int mapHeight = 0;

            try {
                mapWidth = Int32.Parse(Environment.GetEnvironmentVariable("MAP_WIDTH"));
            } catch (FormatException e) {
                Console.WriteLine("Error parsing MAP_WIDTH: " + e.Message);
            } catch (ArgumentException) {
                Console.WriteLine("MAP_WIDTH not set in ENV");
            }

            try {
                mapHeight = Int32.Parse(Environment.GetEnvironmentVariable("MAP_HEIGHT"));
            } catch (FormatException e) {
                Console.WriteLine("Error parsing MAP_HEIGHT: " + e.Message);
            } catch (ArgumentException) {
                Console.WriteLine("MAP_HEIGHT not set in ENV");
            }

            if (mapWidth < 10) {
                Console.WriteLine("Defaulting MAP_WIDTH to 325."); 
                Environment.SetEnvironmentVariable("MAP_WIDTH", "325");
            }

            if (mapHeight < 10) {
                Console.WriteLine("Defaulting MAP_HIEGHT to 325.");
                Environment.SetEnvironmentVariable("MAP_HEIGHT", "325");
            }
        }
    }
}