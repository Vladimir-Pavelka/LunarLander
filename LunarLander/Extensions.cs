using System;

namespace LunarLander
{
    public static class Extensions
    {
        public static Vector ToVector(this (double x, double y) tuple)
        {
            if (IsCloseToZero(tuple.x) && IsCloseToZero(tuple.y)) return Vector.Zero;

            var resultMagnitude = Math.Sqrt(tuple.x * tuple.x + tuple.y * tuple.y);
            var resultAngle = Utils.Asin(tuple.y / resultMagnitude);

            return new Vector(resultMagnitude, resultAngle);
        }

        private static bool IsCloseToZero(double number) => Math.Abs(number) < 0.00000000001;
    }
}