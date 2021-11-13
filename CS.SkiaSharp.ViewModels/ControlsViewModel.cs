using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Eventbus.Contracts;
using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using CS.SkiaSharpExample.UI.Contracts;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.SkiaSharpExample.ViewModels
{
    public class ControlsViewModel : ViewModel
    {
        private readonly ISceneManager _sceneManager;
        private readonly GeneralSettings _settings;
        private readonly ElementSettings _leftCircleSettings;
        private readonly ElementSettings _rightCircleSettings;
        private ICommand _createSceneCommand;
        private ICommand _createCirclesCommand;
        private ICommand _loadBitmapCommand;
        private ICommand _bitmapToGrayscaleCommand;
        private ICommand _addWatermarkCommand;
        private ICommand _clipCommand;
        private ICommand _makePerlinNoiseCommand;

        public GeneralSettings Settings => _settings;
        public ElementSettings LeftCircleSettings => _leftCircleSettings;
        public ElementSettings RightCircleSettings => _rightCircleSettings;
        public IEnumerable<ElementSettings> ElementSettings => new[] { LeftCircleSettings, RightCircleSettings };
        public ICommand CreateSceneCommand => _createSceneCommand ??= new RelayCommand(p => CreateScene());
        public ICommand CreateCirclesCommand => _createCirclesCommand ??= new RelayCommand(p => CreateCircles());
        public ICommand LoadBitmapCommand => _loadBitmapCommand ??= new RelayCommand(LoadBitmap);
        public ICommand BitmapToGrayscaleCommand => _bitmapToGrayscaleCommand ??= new RelayCommand(BitmapToGrayscale);
        public ICommand AddWatermarkCommand => _addWatermarkCommand ??= new RelayCommand(AddWatermark);
        public ICommand ClipCommand => _clipCommand ??= new RelayCommand(Clip);
        public ICommand MakePerlinNoiseCommand => _makePerlinNoiseCommand ??= new RelayCommand(MakePerlinNoise);

        public ControlsViewModel(IMessageBroker messageBroker, ISceneManager sceneManager) : base(messageBroker)
        {
            _sceneManager = sceneManager;
            _settings = new GeneralSettings() { IsAntialias = true };
            _settings.Changed += () =>
            {
                SendMessage(_settings, new RenderingRequestedEventArgs());
            };
            _leftCircleSettings = new ElementSettings()
            {
                Radius = 50,
                X = 10d
            };
            _leftCircleSettings.Changed += () =>
            {
                CreateCircles();
            };
            _rightCircleSettings = new ElementSettings()
            {
                Radius = 50,
                X = 100d
            };
            _rightCircleSettings.Changed += () =>
            {
                CreateCircles();
            };
        }

        private void CreateScene()
        {
            _sceneManager.CreateScene(_settings);
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void CreateCircles()
        {
            _sceneManager.ClearScene();
            var arr = new[] { _leftCircleSettings, _rightCircleSettings };
            foreach (var elementSettings in arr)
            {
                _sceneManager.CreateCircle(_settings, elementSettings);
            }
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void LoadBitmap(object commandParameter)
        {
            _settings.IsGrayScale = false;
            _settings.HasWatermark = false;
            _settings.HasPerlinFractalNoise = false;
            _settings.HasPerlinTurbulenceNoise = false;
            _settings.IsClipped = false;
            _sceneManager.ClearScene();
            _sceneManager.CreateScene(_settings);
            _sceneManager.CreateBitmap(_settings, new ElementSettings());
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void BitmapToGrayscale(object commandParameter)
        {
            _settings.IsGrayScale = true;
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void AddWatermark(object commandParameter)
        {
            _settings.HasWatermark = true;
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void Clip(object commandParameter)
        {
            _settings.IsClipped = true;
            SendMessage(this, new RenderingRequestedEventArgs());
        }

        private void MakePerlinNoise(object commandParameter)
        {
            _sceneManager.ClearScene();
            _sceneManager.CreateScene(_settings);
            _settings.MakeNoise = !_settings.MakeNoise;
            SendMessage(this, new RenderingRequestedEventArgs());
        }
    }
}
