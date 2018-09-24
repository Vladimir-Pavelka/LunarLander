using System;

namespace LunarLander
{
    public static class Utils
    {
        public static double Sin(double angle) => Math.Sin(ToRadians(angle));
        public static double Cos(double angle) => Math.Cos(ToRadians(angle));
        public static double Asin(double angle) => ToRadians(Math.Asin(angle));

        private static double ToRadians(double angle) => angle * Math.PI / 180.0;
    }
}