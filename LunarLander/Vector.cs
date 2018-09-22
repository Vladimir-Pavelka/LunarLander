using System;

namespace LunarLander
{
    public class Vector
    {
        public double Magnitude { get; }
        public double Angle { get; }

        public Vector(double magnitude, double angle)
        {
            Magnitude = magnitude;
            Angle = angle;
        }

        public Vector Add(Vector other)
        {
            var thisX = Magnitude * Math.Cos(Angle);
            var thisY = Magnitude * Math.Sin(Angle);

            var otherX = other.Magnitude * Math.Cos(other.Angle);
            var otherY = other.Magnitude * Math.Sin(other.Angle);

            var resultX = thisX + otherX;
            var resultY = thisY + otherY;

            var resultMagnitude = Math.Sqrt(resultX * resultX + resultY * resultY);
            var resultAngle = Math.Asin(resultY / resultMagnitude);

            return new Vector(resultMagnitude, resultAngle);
        }

        public static Vector Zero { get; } = new Vector(0, 0);
    }
}