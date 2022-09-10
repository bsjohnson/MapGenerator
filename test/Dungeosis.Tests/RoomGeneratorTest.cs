using System;
using Xunit;
using Dungeosis;

namespace Dungeosis.Tests {
    public class RoomGeneratorTest {
        const int maxXBound = 200;
        const int maxYBound = 100;
        
        [Fact]
        public void Generate_DoesNotGenerateRoomsOutsideTheGivenBounds() {
            RoomGenerator generator = new();

            for (int i = 0; i < 250; i++) {
                // rooms are randomly generated, so test a bunch and print the room details with any failure.
                Room room = generator.Generate(maxXBound, maxYBound);

                Assert.True(room.X >= 0, "X coord should not be negative: " + room.ToString());
                Assert.True(room.X < maxXBound, "X coord should not be greater than the maximum X boundary: " + room.ToString());
                Assert.True(room.Y >= 0, "Y coord should not be negative: " + room.ToString());
                Assert.True(room.Y < maxYBound, "Y coord should not be greater than the maximum Y boundary: " + room.ToString());
                Assert.True(room.X + room.Width < maxXBound, "Width of room should not exceed max X boundary: " + room.ToString());
                Assert.True(room.Y + room.Height < maxYBound, "Height of room should not exceed max X boundary" + room.ToString());

                int x1 = room.X + room.Width;
                int y1 = room.Y + room.Height;

                Assert.True(x1 >= 0, "X1 coord should not be negative: " + room.ToString());
                Assert.True(x1 < maxXBound, "X1 coord should not be greater than the maximum X boundary: " + room.ToString());
                Assert.True(y1 >= 0, "Y1 coord should not be negative: " + room.ToString());
                Assert.True(y1 < maxYBound, "Y1 coord should not be greater than the maximum Y boundary: " + room.ToString());
            }
        }

        [Fact]
        public void Generate_GeneratesRoomsOnOddCoordinates() {
            RoomGenerator generator = new();
            int maxXBound = 200;
            int maxYBound = 100;

            for (int i = 0; i < 250; i++) {
                Room room = generator.Generate(maxXBound, maxYBound);

                Assert.True(room.X % 2 == 1, "X coordinate should have been odd.");
                Assert.True(room.Y % 2 == 1, "Y coordinate should have been odd.");
            }
        }

        [Fact]
        public void Generate_GeneratesRoomsWithOddDimensions() {
            RoomGenerator generator = new();
            int maxXBound = 200;
            int maxYBound = 100;

            for (int i = 0; i < 250; i++) {
                Room room = generator.Generate(maxXBound, maxYBound);

                Assert.True(room.Width % 2 == 1, "room width should have been odd.");
                Assert.True(room.Y % 2 == 1, "room height should have been odd.");
            }
        }
    }
}