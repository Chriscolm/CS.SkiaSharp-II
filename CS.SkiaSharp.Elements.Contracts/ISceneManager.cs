using CS.SkiaSharpExample.Elements.Contracts.Models;

namespace CS.SkiaSharpExample.Elements.Contracts
{
    public interface ISceneManager
    {
        void Render(object surface, double surfaceWidth, double surfaceHeight);
        void CreateScene(GeneralSettings settings);
        void CreateCircle(GeneralSettings settings, ElementSettings elementSettings);
        void CreateBitmap(GeneralSettings settings, ElementSettings elementSettings);
        void ClearScene();
    }
}
