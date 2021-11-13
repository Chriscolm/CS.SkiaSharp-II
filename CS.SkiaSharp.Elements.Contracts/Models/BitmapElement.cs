using CS.SkiaSharpExample.Elements.Contracts.Primitives;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class BitmapElement: Element
    {
        public static BitmapElement Create(ElementSettings settings)
        {
            var vector = new Vector2D(0, 0);
            var result = new BitmapElement()
            {
                Settings = settings,
            };
            result.AddCoordinates(vector);
            return result;
        }
    }
}
