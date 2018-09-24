using System;

namespace LunarLander
{
    public static class Utils
    {
        public static double Sin(double angle) => Math.Sin(ToRadians(angle));
        public static double Cos(double angle) => Math.Cos(ToRadians(angle));
        public static double Atan2(double y, double x) => ToDegrees(Math.Atan2(y, x));

        private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
        private static double ToDegrees(double radians) => radians / Math.PI * 180.0;
    }
}