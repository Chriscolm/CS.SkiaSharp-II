using CS.SkiaSharpExample.Elements.Contracts.Primitives;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Circle : Element
    {
        public double Radius { get; private set; }

        public static Circle Create(ElementSettings elementSettings)
        {
            var vector = new Vector2D(elementSettings.X, 0);
            var result = new Circle()
            {
                Settings = elementSettings,
                Radius = elementSettings.Radius
            };
            result.AddCoordinates(vector);
            return result;
        }

        public override string ToString()
        {
            return $"{string.Join(" | ", Coordinates)} r = {Radius}";
        }
    }
}
