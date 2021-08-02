using System;
using System.Windows;

namespace Dungeosis {
    
    public class Room {
        public Room(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        const int CellCode = 1;
        public int x { set; get; }
        public int y { set; get; }
        public int width { set; get; }
        public int height { set; get; }

        public bool intersects(Room room) {
            if (room == null) return false;
            
            var room_x1 = room.x + room.width;
            var room_y1 = room.y + room.height;
            
            var this_x1 = this.x + this.width;
            var this_y1 = this.y + this.width;

            return this.x <= room_x1 && this_x1 >= room.x && this.y <= room_y1 && this_y1 >= room.y;
        }

        public override string ToString() {
            return $"Room:: x={this.x}, y={this.y}, width={this.width}, height={this.height}";
        }
    }
}