using System;

namespace Dungeosis {
    public class RoomGenerator {
        const int MaxWidth = 20;
        const int MaxHeight = 20;
        const int MinWidth = 3;
        const int MinHeight = 3;
        private readonly Random random;
        
        public RoomGenerator() : this((int)DateTimeOffset.Now.ToUnixTimeMilliseconds()) {}
        public RoomGenerator(int seed) {
            this.random = new Random(seed);
        }

        public Room Generate(int maxXBound, int maxYBound) {
            var x = this.random.Next(0, maxXBound - MaxWidth - 3); // Using a edge buffer here in case we need to adjust positions.
            var y = this.random.Next(0, maxYBound - MaxHeight - 3);
            var width = this.random.Next(MinWidth, MaxWidth);
            var height = this.random.Next(MinHeight, MaxHeight);

            // Ensure rooms are placed on odd numbered tiles, and the edges are on odd numbered tiles.
            if (x % 2 == 0) x++;
            if (y % 2 == 0) y++;
            if (width % 2 == 0) width++;
            if (height % 2 == 0) height++;

            return new Room(x, y, width, height);
        }
    }
}