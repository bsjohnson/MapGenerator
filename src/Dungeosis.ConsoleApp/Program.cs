using System;
using System.IO;

namespace Dungeosis.ConsoleApp
{
    class Program
    {
        public const int DefaultMapWidth = 1000;
        public const int DefaultMapHeight = 1000;

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Dungeosis.");
            Console.WriteLine("Args: " + (args.Length == 0 ? "none" : String.Join(", ", args)));

            MapGeneratorConfig config = new();
            Console.WriteLine("Seed: " + config.Seed);

            var map = new MapGenerator(config).Generate();

            WriteMapToFile(map);
        }

        private static void WriteMapToFile(Map map)
        {
            File.WriteAllTextAsync("map.txt", map.GetGridAsString()).Wait();
        }
    }
}