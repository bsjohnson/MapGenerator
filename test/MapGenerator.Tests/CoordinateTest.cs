using Xunit;

namespace MapGenerator.Tests
{
    public class CoordinateTest
    {
        [Fact]
        public void Coordinate_shouldHaveXAndYCoordinates()
        {
            Coordinate coordinate = new(10, 20);

            Assert.Equal(10, coordinate.X);
            Assert.Equal(20, coordinate.Y);
        }

        [Fact]
        public void Coordinate_canBeAddedToDirectionVector()
        {
            Coordinate coordinate = new(10, 20);

            Coordinate newCoordinate = coordinate + Direction.North;

            Assert.Equal(10, newCoordinate.X);
            Assert.Equal(19, newCoordinate.Y);

            newCoordinate += Direction.East;

            Assert.Equal(11, newCoordinate.X);
            Assert.Equal(19, newCoordinate.Y);

            newCoordinate += Direction.South;

            Assert.Equal(11, newCoordinate.X);
            Assert.Equal(20, newCoordinate.Y);

            newCoordinate += Direction.West;

            Assert.Equal(10, newCoordinate.X);
            Assert.Equal(20, newCoordinate.Y);
        }

        [Fact]
        public void IsNextTo_ReturnsTrueIfCoordinatesAreNextToEachOther()
        {
            Coordinate origin = new(1, 1);
            Coordinate north = new(1, 0);
            Coordinate south = new(1, 2);
            Coordinate east = new(2, 1);
            Coordinate west = new(0, 1);

            Assert.True(origin.IsNextTo(north));
            Assert.True(origin.IsNextTo(south));
            Assert.True(origin.IsNextTo(east));
            Assert.True(origin.IsNextTo(west));
        }

        [Fact]
        public void IsNextTo_ReturnsFalseIfCoordinatesAreMoreThanOneSpaceApart()
        {
            Coordinate origin = new(2, 2);
            Coordinate north = new(2, 0);
            Coordinate south = new(2, 4);
            Coordinate east = new(4, 2);
            Coordinate west = new(0, 2);

            Assert.False(origin.IsNextTo(north));
            Assert.False(origin.IsNextTo(south));
            Assert.False(origin.IsNextTo(east));
            Assert.False(origin.IsNextTo(west));
        }
    }
}