using System;
using Xunit;
using Dungeosis;

namespace Dungeosis.Tests {
    public class RoomTest {
        [Fact]
        public void Intersects_ReturnsTrueForContainedRooms() {
            Room room1 = new(0, 0, 10, 10);
            Room room2 = new(1, 1, 5, 5);

            Assert.True(room1.Intersects(room2), "A room containing another should have intersected");            
            Assert.True(room2.Intersects(room1), "A room contained in another should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForOverlappedRooms() {
            Room room1 = new(61, 39, 5, 11);
            Room room2 = new(61, 29, 7, 19);

            Assert.True(room1.Intersects(room2), "A room being overlapped by another should have intersected");            
            Assert.True(room2.Intersects(room1), "A room overlapping another should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForIdenticalRooms() {
            Room room1 = new(5, 5, 25, 25);
            Room room2 = new(5, 5, 25, 25);

            Assert.True(room1.Intersects(room2), "An identical room should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForRoomsOnSameHorizontalAxis() {
            Room room1 = new(5, 5, 10, 10);
            Room room2 = new(10, 5, 10, 10);

            Assert.True(room1.Intersects(room2), "A room overlapping on the same X axis should have intersected");            
            Assert.True(room2.Intersects(room1), "A room being overlapped on the same X axis should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueForRoomsOnSameVerticalAxis() {
            Room room1 = new(5, 5, 10, 10);
            Room room2 = new(5, 10, 10, 10);

            Assert.True(room1.Intersects(room2), "A room overlapping on the same Y axis should have intersected");            
            Assert.True(room2.Intersects(room1), "A room being overlapped on the same Y axis should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsTrueIfRoomCornersOverlap() {
            Room room1 = new(5, 5, 10, 10);
            Room room2 = new(2, 2, 3, 3);
            Room room3 = new(0, 5, 2, 2);

            Assert.True(room1.Intersects(room2), "A room touching another on its top left corner should have intersected");            
            Assert.True(room2.Intersects(room1), "A room touching another on its bottom right corner should have intersected");
            Assert.True(room2.Intersects(room3), "A room touching another on its bottom left corner should have intersected");            
            Assert.True(room3.Intersects(room2), "A room touching another on its top right corner should have intersected");
        }

        [Fact]
        public void Intersects_ReturnsFalseIfRoomsDoNotOverlap() {
            Room room1 = new(5, 5, 10, 10);
            Room room2 = new(20, 20, 5, 5);

            Assert.False(room1.Intersects(room2), "A room not overlapping another should not have intersected");            
            Assert.False(room2.Intersects(room1), "A room not overlapping another should not have intersected (inverse check)");
        }

        [Fact]
        public void Intersects_HasADefaultThresholdOfTwo() {
            Room room1 = new(20, 20, 10, 10);
            Room room2 = new(9, 20, 10, 10);
            
            Assert.True(room1.Intersects(room2), "Rooms west within 2 spaces should be considered overlapping");
            Assert.False(room1.Intersects(room2, 0), "Rooms west should not have been considered overlapping with zero threshold.");

            room1 = new(20, 20, 10, 10);
            room2 = new(20, 9, 10, 10);

            Assert.True(room1.Intersects(room2), "Rooms north within 2 spaces should be considered overlapping");
            Assert.False(room1.Intersects(room2, 0), "Rooms north should not have been considered overlapping with zero threshold.");

            room1 = new(20, 20, 10, 10);
            room2 = new(31, 20, 10, 10);

            Assert.True(room1.Intersects(room2), "Rooms eaast within 2 spaces should be considered overlapping");
            Assert.False(room1.Intersects(room2, 0), "Rooms east should not have been considered overlapping with zero threshold.");

            room1 = new(20, 20, 10, 10);
            room2 = new(20, 31, 10, 10);

            Assert.True(room1.Intersects(room2), "Rooms south within 2 spaces should be considered overlapping");
            Assert.False(room1.Intersects(room2, 0), "Rooms south should not have been considered overlapping with zero threshold.");
        }
    }
}
