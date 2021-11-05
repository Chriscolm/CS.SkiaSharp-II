using CS.SkiaSharpExample.Elements.Contracts.Models;

namespace CS.SkiaSharpExample.Renderer.Contracts
{
    public interface IRenderer
    {
        void Render(object surface, double surfaceWidth, double surfaceHeight, Scene scene);
    }
}
