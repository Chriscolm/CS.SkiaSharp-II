namespace CS.SkiaSharpExample.Elements.Contracts.Primitives
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public float Xf { get; private set; }
        public float Yf { get; private set; }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
            Xf = (float)X;
            Yf = (float)Y;
        }

        public Vector2D Add(double x, double y)
        {
            var result = new Vector2D(X + x, Y + y);
            return result;
        }
    }
}
