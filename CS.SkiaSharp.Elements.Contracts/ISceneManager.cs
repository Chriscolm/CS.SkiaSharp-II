using CS.SkiaSharpExample.Elements.Contracts.Models;
using System.Threading.Tasks;

namespace CS.SkiaSharpExample.Elements.Contracts
{
    public interface ISceneManager
    {
        void Render(object surface, double surfaceWidth, double surfaceHeight);
        void CreateScene(Settings settings);
        void CreateTriangle(Settings settings);
        void CreateCircle(Settings settings);
        Task StartAnimationAsync(Settings settings);
        Task StopAnimationAsync();
    }
}
