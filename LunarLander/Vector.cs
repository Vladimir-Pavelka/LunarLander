using System;
using System.Diagnostics;

namespace LunarLander
{
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public class Vector
    {
        public double Magnitude { get; }
        public double Angle { get; }

        private readonly Lazy<(double x, double y)> _components;

        public double X => _components.Value.x;
        public double Y => _components.Value.y;

        public Vector(double magnitude, double angle)
        {
            Magnitude = magnitude;
            Angle = angle % 360;
            _components = new Lazy<(double x, double y)>(ToComponents);
        }

        public static Vector Zero { get; } = new Vector(0, 0);

        public Vector Add(Vector other) => (X + other.X, Y + other.Y).ToVector();

        public Vector Multiply(double x) => new Vector(Magnitude * x, Angle);

        public Vector Avg(Vector other) => ((X + other.X) / 2, (Y + other.Y) / 2).ToVector();

        private (double, double) ToComponents()
        {
            var x = Magnitude * Utils.Cos(Angle);
            var y = Magnitude * Utils.Sin(Angle);

            return (x, y);
        }
    }
}