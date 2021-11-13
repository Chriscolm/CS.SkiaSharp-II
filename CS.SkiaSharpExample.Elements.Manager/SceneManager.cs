using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Renderer.Contracts;

namespace CS.SkiaSharpExample.Elements.Manager
{
    public class SceneManager : ISceneManager
    {
        private Scene _currentScene;
        private readonly IRenderer _renderer;

        public Scene CurrentScene => _currentScene;

        public SceneManager(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public void Render(object surface, double surfaceWidth, double surfaceHeight)
        {
            if (_currentScene == null)
            {
                CreateScene(new GeneralSettings());
            }
            _renderer.Render(surface, surfaceWidth, surfaceHeight, _currentScene);
            
        }

        public void CreateScene(GeneralSettings settings)
        {
            _currentScene = new Scene
            {
                Settings = settings
            };
        }
        
        public void CreateCircle(GeneralSettings settings, ElementSettings elementSettings)
        {
            if (_currentScene == null)
            {
                CreateScene(settings); 
            }
            var c = Circle.Create(elementSettings);
            _currentScene.Add(c);
        }

        public void CreateBitmap(GeneralSettings settings, ElementSettings elementSettings)
        {
            if(_currentScene == null)
            {
                CreateScene(settings);
            }
            var bmp = BitmapElement.Create(elementSettings);
            _currentScene.Add(bmp);
        }

        public void ClearScene()
        {
            _currentScene?.Clear();
        }
    }
}
