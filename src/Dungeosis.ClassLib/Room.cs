using System;
using System.Windows;

namespace Dungeosis {
    
    public class Room {
                const int BorderThreshold = 2;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Region { get; set; }

        public Room(int x, int y, int width, int height) {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public bool Intersects(Room room, int threshold = Room.BorderThreshold) {
            if (room == null) return false;
            
            var room_x1 = room.X + room.Width;
            var room_y1 = room.Y + room.Height;
            
            var this_x0 = this.X - threshold;
            var this_y0 = this.Y - threshold;
            var this_x1 = this.X + this.Width + threshold;
            var this_y1 = this.Y + this.Height + threshold;

            return this_x0 <= room_x1 && this_x1 >= room.X && this_y0 <= room_y1 && this_y1 >= room.Y;
        }

        public override string ToString() {
            return $"Room:: x={this.X}, y={this.Y}, width={this.Width}, height={this.Height}";
        }
    }
}