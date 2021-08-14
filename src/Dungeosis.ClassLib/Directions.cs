using System.Numerics;
using System.Collections.Generic;

namespace Dungeosis {
    public class Direction {
        public static readonly Vector2 North = new(0.0f, -1.0f);
        public static readonly Vector2 South = new(0.0f, 1.0f);
        public static readonly Vector2 East = new(1.0f, 0.0f);
        public static readonly Vector2 West = new(-1.0f, 0.0f);

        public static readonly Vector2[] Cardinals = new[] {
            North, South, East, West
        };
    }
}