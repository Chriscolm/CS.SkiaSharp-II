using CS.SkiaSharpExample.Elements.Contracts.Primitives;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Circle : Element
    {
        public double Radius { get; private set; }

        public static Circle Create(double radius)
        {
            var vector = new Vector2D(0, 0);
            var result = new Circle()
            {
                Radius = radius
            };
            result.AddCoordinates(vector);
            return result;
        }
    }
}
