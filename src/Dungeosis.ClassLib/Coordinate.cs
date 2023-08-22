using System;
using System.Numerics;

namespace Dungeosis
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool IsNextTo(Coordinate coordinate)
        {
            if (this == coordinate) return true;
            if (this.X == coordinate.X && Math.Abs(this.Y - coordinate.Y) < 2) return true;
            if (this.Y == coordinate.Y && Math.Abs(this.X - coordinate.X) < 2) return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Coordinate coordinate = (Coordinate)obj;

            return this.X == coordinate.X && this.Y == coordinate.Y;
        }

        public static bool operator ==(Coordinate x, Coordinate y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Coordinate x, Coordinate y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return (this.X << 2) ^ this.Y;
        }

        public static Coordinate operator +(Coordinate coordinate, Vector2 vector)
        {
            return new Coordinate(coordinate.X + (int)vector.X, coordinate.Y + (int)vector.Y);
        }

        public override string ToString()
        {
            return $"Coordinate({this.X}, {this.Y})";
        }
    }
}