using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Elements.Contracts.Primitives;
using CS.SkiaSharpExample.Eventbus.Contracts;
using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using CS.SkiaSharpExample.Renderer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.SkiaSharpExample.Elements.Manager
{
    public class SceneManager : ISceneManager
    {
        private Scene _currentScene;
        private readonly IRenderer _renderer;
        private readonly IMessageBroker _messageBroker;
        private bool _stopFlag;

        public Scene CurrentScene => _currentScene;

        public SceneManager(IRenderer renderer, IMessageBroker messageBroker)
        {
            _renderer = renderer;
            _messageBroker = messageBroker;
        }

        public void Render(object surface, double surfaceWidth, double surfaceHeight)
        {
            if (_currentScene == null)
            {
                CreateScene(new Settings());
            }
            _renderer.Render(surface, surfaceWidth, surfaceHeight, _currentScene);
        }

        public void CreateScene(Settings settings)
        {
            _currentScene = new Scene
            {
                Settings = settings
            };
        }

        public void CreateTriangle(Settings settings)
        {
            CreateScene(settings);
            var count = Math.Max(1, settings.Count);
            for (int i = 0; i < count; i++)
            {
                var xy = 10d + i * 10;
                var start = new Vector2D(xy, xy);
                var triangle = Triangle.CreateEquilateral(60d, start);
                _currentScene.Add(triangle);
            }
        }

        public void CreateCircle(Settings settings)
        {
            CreateScene(settings);
            var r = settings.Radius;
            var c = Circle.Create(r);
            _currentScene.Add(c);
        }

        public async Task StartAnimationAsync(Settings settings)
        {
            CreateScene(settings);
            var colorToRestore = settings.ForegroundColor;
            _stopFlag = false;
            settings.IsCentered = true;
            settings.Scale = 1.0;
            var reds = new List<int>();
            var greens = new List<int>();
            var blues = new List<int>();

            var radseq = Enumerable.Range(0, 127);
            var cnt = radseq.Count();
            var i = 0;
            while (!_stopFlag)
            {
                settings.ForegroundColor = ShiftForgroundColor(settings.ForegroundColor, reds, greens, blues);
                var f = 1d;
                var r = 10d + radseq.ElementAt(i++) * f;
                var c = Circle.Create(r);
                _currentScene.Replace(c);
                var text = $"Current Foreground {settings.ForegroundColor}";
                _currentScene.Replace(Caption.Create(text));
                if (i == cnt)
                {
                    i = 0;
                    radseq = radseq.Reverse().ToArray();
                    f *= -1d;
                }
                _messageBroker.Send(this, new RenderingRequestedEventArgs());
                await Task.Delay(10);
            }
            settings.ForegroundColor = colorToRestore;            
        }

        public async Task StopAnimationAsync()
        {
            _stopFlag = true;            
            await Task.CompletedTask;
        }

        private static string ShiftForgroundColor(string color, List<int> reds, List<int> greens, List<int> blues)
        {
            byte next(List<int> collection, byte val)
            {
                if (!collection.Any())
                {
                    collection.AddRange(Enumerable.Range(0, 256));
                }
                var idx = collection.IndexOf(val);
                if (idx < collection.Count - 1)
                {
                    return (byte)collection[idx + 1];
                }
                else
                {
                    collection.Reverse();
                    return (byte)(val - 1);
                }
            }
            string shift(int val)
            {
                var r = (byte)(val >> 16);
                var g = (byte)(val >> 8);
                var b = (byte)val;

                var arr = new[]
                {
                    next(blues, b),
                    next(greens, g),
                    next(reds, r),
                    byte.MinValue
                };
                var res = BitConverter.ToInt32(arr);
                return res.ToString("X");
            }
            var clr = color.Trim('#');
            if (int.TryParse(clr, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out var c))
            {
                var res = shift(c);
                return res;
            }            
            return color;
        }
    }
}
