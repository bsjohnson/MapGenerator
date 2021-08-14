using System;
using System.IO;
using Dungeosis;
using System.Text.Json;

namespace Dungeosis.ConsoleApp {
    class Program {
        public const int DefaultMapWidth = 1000;
        public const int DefaultMapHeight = 1000;

        public static void Main(string[] args) {
            Console.WriteLine("Starting Dungeosis.");
            Console.WriteLine("Args: " + (args.Length == 0 ? "none" : String.Join(", ", args)));
            
            InstantiateEnvironment();

            var seed = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Console.WriteLine("Seed: " + seed);

            var map = new MapGenerator(seed).Generate();

            WriteMapToFile(map);
        }

        private static void WriteMapToFile(Map map) {
            File.WriteAllTextAsync("map.txt", map.GetGridAsString()).Wait();
        }

        private static void InstantiateEnvironment() {
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
                Console.WriteLine("Defaulting MAP_WIDTH to 125."); 
                Environment.SetEnvironmentVariable("MAP_WIDTH", Program.DefaultMapWidth.ToString());
            }

            if (mapHeight < 10) {
                Console.WriteLine("Defaulting MAP_HEIGHT to 75.");
                Environment.SetEnvironmentVariable("MAP_HEIGHT",  Program.DefaultMapHeight.ToString());
            }
        }
    }
}