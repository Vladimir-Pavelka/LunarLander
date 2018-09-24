using System;
using System.Drawing;

namespace Gui
{
    public static class Utils
    {
        public static Bitmap RotateImage(Image b, float angle)
        {
            // The original bitmap needs to be drawn onto a new bitmap which will probably be bigger 
            // because the corners of the original will move outside the original rectangle.
            // An easy way (OK slightly 'brute force') is to calculate the new bounding box is to calculate the positions of the 
            // corners after rotation and get the difference between the maximum and minimum x and y coordinates.
            var wOver2 = b.Width / 2.0f;
            var hOver2 = b.Height / 2.0f;
            var radians = -(angle / 180.0 * Math.PI);
            // Get the coordinates of the corners, taking the origin to be the centre of the bitmap.
            var corners = new[]
            {
                new PointF(-wOver2, -hOver2),
                new PointF(+wOver2, -hOver2),
                new PointF(+wOver2, +hOver2),
                new PointF(-wOver2, +hOver2)
            };

            for (var i = 0; i < 4; i++)
            {
                var p = corners[i];
                var newP = new PointF((float)(p.X * Math.Cos(radians) - p.Y * Math.Sin(radians)),
                    (float)(p.X * Math.Sin(radians) + p.Y * Math.Cos(radians)));
                corners[i] = newP;
            }

            // Find the min and max x and y coordinates.
            var minX = corners[0].X;
            var maxX = minX;
            var minY = corners[0].Y;
            var maxY = minY;
            for (var i = 1; i < 4; i++)
            {
                var p = corners[i];
                minX = Math.Min(minX, p.X);
                maxX = Math.Max(maxX, p.X);
                minY = Math.Min(minY, p.Y);
                maxY = Math.Max(maxY, p.Y);
            }

            // Get the size of the new bitmap.
            var newSize = new SizeF(maxX - minX, maxY - minY);
            // ...and create it.
            var returnBitmap = new Bitmap((int)Math.Ceiling(newSize.Width), (int)Math.Ceiling(newSize.Height));
            // Now draw the old bitmap on it.
            using (var g = Graphics.FromImage(returnBitmap))
            {
                g.TranslateTransform(newSize.Width / 2.0f, newSize.Height / 2.0f);
                g.RotateTransform(angle);
                g.TranslateTransform(-b.Width / 2.0f, -b.Height / 2.0f);

                g.DrawImage(b, 0, 0);
            }

            return returnBitmap;
        }
    }
}