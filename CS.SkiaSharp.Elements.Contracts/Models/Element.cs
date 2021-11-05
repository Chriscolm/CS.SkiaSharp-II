using CS.SkiaSharpExample.Elements.Contracts.Primitives;
using System.Collections.Generic;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public abstract class Element
    {
        public IEnumerable<Vector2D> Coordinates { get; protected set; }

        protected virtual void AddCoordinates(params Vector2D[] vectors)
        {
            Coordinates = vectors;
        }
    }
}
