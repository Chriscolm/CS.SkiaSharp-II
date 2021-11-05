using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Eventbus.Contracts;

namespace CS.SkiaSharpExample.ViewModels
{
    public class CanvasViewModel : ViewModel
    {
        private double _canvasWidth;
        private double _canvasHeight;
        private readonly ISceneManager _sceneManager;

        public double CanvasWidth { get => _canvasWidth; set => SetProperty(ref _canvasWidth, value); }

        public double CanvasHeight { get => _canvasHeight; set => SetProperty(ref _canvasHeight, value); }

        public CanvasViewModel(IMessageBroker messageBroker, ISceneManager sceneManager) : base(messageBroker)
        {
            _sceneManager = sceneManager;
        }

        public void Render(object surface, double surfaceWidth, double surfaceHeight)
        {
            _sceneManager.Render(surface, surfaceWidth, surfaceHeight);
        }
    }
}
