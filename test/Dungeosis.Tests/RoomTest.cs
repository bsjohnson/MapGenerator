using System;
using Xunit;
using Dungeosis;

namespace Dungeosis.Tests {
    public class RoomTest {
        [Fact]
        public void Intersects_ReturnsTrueForContainedRooms() {
            Room room1 = new Room(0, 0, 10, 10);
            Room room2 = new Room(1, 1, 5, 5);

            Assert.True(room1.intersects(room2), "A room containing another should have intersected");            
            Assert.True(room2.intersects(room1), "A room contained in another should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForOverlappedRooms() {
            Room room1 = new Room(5, 5, 10, 10);
            Room room2 = new Room(2, 2, 10, 10);

            Assert.True(room1.intersects(room2), "A room being overlapped by another should have intersected");            
            Assert.True(room2.intersects(room1), "A room overlapping another should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForIdenticalRooms() {
            Room room1 = new Room(5, 5, 25, 25);
            Room room2 = new Room(5, 5, 25, 25);

            Assert.True(room1.intersects(room2), "An identical room should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForRoomsOnSameHorizontalAxis() {
            Room room1 = new Room(5, 5, 10, 10);
            Room room2 = new Room(10, 5, 10, 10);

            Assert.True(room1.intersects(room2), "A room overlapping on the same X axis should have intersected");            
            Assert.True(room2.intersects(room1), "A room being overlapped on the same X axis should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForRoomsOnSameVerticalAxis() {
            Room room1 = new Room(5, 5, 10, 10);
            Room room2 = new Room(5, 10, 10, 10);

            Assert.True(room1.intersects(room2), "A room overlapping on the same Y axis should have intersected");            
            Assert.True(room2.intersects(room1), "A room being overlapped on the same Y axis should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsFalseIfRoomsDoNotOverlap() {
            Room room1 = new Room(5, 5, 10, 10);
            Room room2 = new Room(17, 17, 5, 5);

            Assert.False(room1.intersects(room2), "A room not overlapping another should not have intersected");            
            Assert.False(room2.intersects(room1), "A room not overlapping another should not have intersected (inverse check)");
        }
    }
}
