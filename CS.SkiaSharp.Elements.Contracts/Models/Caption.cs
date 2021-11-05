namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Caption : Element
    {
        public string Text { get; set; }

        public static Caption Create(string text)
        {
            var result = new Caption()
            {
                Text = text
            };
            result.AddCoordinates(new Primitives.Vector2D(10, 10));
            return result;
        }
    }
}
