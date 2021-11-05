using CS.SkiaSharpExample.Elements.Contracts.Primitives;
using System;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Triangle : Element
    {
        public static Triangle CreateEquilateral(double length, Vector2D start)
        {
            var a = start;
            var b = start.Add(length, 0d);

            var h = Math.Sqrt(length * length - length / 2d * length / 2d);
            var c = start.Add(length / 2d, h);

            var result = new Triangle();
            result.AddCoordinates(a, b, c);
            return result;
        }
    }
}
