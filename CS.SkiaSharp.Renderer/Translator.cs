using SkiaSharp;

namespace CS.SkiaSharpExample.Renderer
{
    internal class Translator
    {
        /// <summary>
        /// Setz den Koordinatenursprung in die linke untere Ecke und führt eine Skalierung durch
        /// </summary>
        /// <param name="point"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        internal static SKPoint Translate(SKPoint point, double height, double scale)
        {
            point = Scale(point, scale);
            var d = height - point.Y;
            point.Y = (float)d;
            return point;
        }

        private static SKPoint Scale(SKPoint point, double scale)
        {
            point.X *= (float)scale;
            point.Y *= (float)scale;
            return point;
        }
    }
}
