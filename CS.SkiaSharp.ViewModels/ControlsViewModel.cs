using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Eventbus.Contracts;
using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using CS.SkiaSharpExample.UI.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS.SkiaSharpExample.ViewModels
{
    public class ControlsViewModel : ViewModel
    {
        private readonly ISceneManager _sceneManager;
        private readonly Settings _settings;
        private ICommand _createSceneCommand;
        private ICommand _createTriangleCommand;
        private ICommand _createCircleCommand;
        private ICommand _startAnimationCommand;
        private ICommand _stopAnimationCommand;

        public Settings Settings => _settings;
        public ICommand CreateSceneCommand => _createSceneCommand ??= new RelayCommand(p => CreateScene());
        public ICommand CreateTriangleCommand => _createTriangleCommand ??= new RelayCommand(p => CreateTriangle());
        public ICommand CreateCircleCommand => _createCircleCommand ??= new RelayCommand(p => CreateCircle());
        public ICommand StartAnimationCommand => _startAnimationCommand ??= new RelayCommand(p => StartAnimation());
        public ICommand StopAnimationCommand => _stopAnimationCommand ??= new RelayCommand(p => StopAnimation());

        public ControlsViewModel(IMessageBroker messageBroker, ISceneManager sceneManager) : base(messageBroker)
        {
            _sceneManager = sceneManager;
            _settings = new Settings();
            _settings.Changed += () =>
            {
                SendMessage(_settings, new RenderingRequestedEventArgs());
            };
        }

        private void CreateScene()
        {
            _sceneManager.CreateScene(_settings);
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void CreateTriangle()
        {
            _sceneManager.CreateTriangle(_settings);
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void CreateCircle()
        {
            _sceneManager.CreateCircle(_settings);
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void StartAnimation()
        {
            Task.Run(async () => await _sceneManager.StartAnimationAsync(_settings));
        }

        private void StopAnimation()
        {
            Task.Run(async () => await _sceneManager.StopAnimationAsync());
        }
    }
}
