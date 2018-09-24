using System;

namespace LunarLander
{
    public static class TupleExtensions
    {
        public static Vector ToVector(this (double x, double y) tuple)
        {
            if (IsCloseToZero(tuple.x) && IsCloseToZero(tuple.y)) return Vector.Zero;

            var resultMagnitude = Math.Sqrt(tuple.x * tuple.x + tuple.y * tuple.y);
            var resultAngle = Math.Asin(tuple.y / resultMagnitude);

            return new Vector(resultMagnitude, resultAngle);
        }

        private static bool IsCloseToZero(double number) => Math.Abs(number) < 0.00000000001;
    }
}