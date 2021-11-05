using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Renderer.Contracts;
using SkiaSharp;
using System.Linq;
using System.Threading.Tasks;

namespace CS.SkiaSharpExample.Renderer
{
    public class Renderer : IRenderer
    {
        private double _width;
        private double _height;

        public void Render(object surface, double surfaceWidth, double surfaceHeight, Scene scene)
        {
            _width = surfaceWidth;
            _height = surfaceHeight;

            if (surface is SKSurface skSurface)
            {
                Render(skSurface, scene);
            }
        }

        private void Render(SKSurface surface, Scene scene)
        {
            var canvas = surface.Canvas;

            if (scene.Settings.IsCentered)
            {
                Center(canvas);
            }

            if (SKColor.TryParse(scene.Settings.BackgroundColor, out var color))
            {
                canvas.Clear(color);
            }

            RenderElements(canvas, scene);
        }

        private void RenderElements(SKCanvas canvas, Scene scene)
        {
            var b = SKColor.TryParse(scene.Settings.ForegroundColor, out var color);
            if (!b)
            {
                return;
            }
            using var paint = CreateDrawPaint(scene, color);

            if (scene.Settings.Parallel)
            {
                Parallel.ForEach(scene.Elements, e =>
                {
                    RenderElement(canvas, e, scene.Settings, paint);
                });
            }
            else
            {
                foreach (var element in scene.Elements)
                {
                    RenderElement(canvas, element, scene.Settings, paint);
                }
            }
        }

        private void RenderElement(SKCanvas canvas, Element element, Settings settings, SKPaint paint)
        {
            if (element is Circle circle)
            {
                RenderRounded(canvas, circle, settings, paint);
            }
            else if (element is Caption caption)
            {
                RenderText(canvas, caption, settings);
            }
            else
            {
                RenderAngular(canvas, element, settings, paint);
            }
        }

        private void RenderAngular(SKCanvas canvas, Element element, Settings settings, SKPaint paint)
        {
            var points = from q in element.Coordinates
                         let p = Translator.Translate(new SKPoint(q.Xf, q.Yf), _height, settings.Scale)
                         select p;
            using var path = new SKPath();
            path.AddPoly(points.ToArray(), close: true);
            canvas.DrawPath(path, paint);
        }

        private void RenderRounded(SKCanvas canvas, Circle element, Settings settings, SKPaint paint)
        {
            var point = from q in element.Coordinates
                        let p = Translator.Translate(new SKPoint(q.Xf, q.Yf), _height, settings.Scale)
                        select p;
            var v = point.Single();
            var r = (float)(element.Radius * settings.Scale);
            canvas.DrawCircle(v, r, paint);
        }

        private void RenderText(SKCanvas canvas, Caption element, Settings settings)
        {
            canvas.ResetMatrix();
            var b = SKColor.TryParse(settings.ForegroundColor, out var color);
            if (!b)
            {
                return;
            }
            using var paint = CreateTextPaint(color);
            var point = from q in element.Coordinates
                        let p = Translator.Translate(new SKPoint(q.Xf, q.Yf), _height, settings.Scale)
                        select p;
            var v = point.Single();
            canvas.DrawText(element.Text, v, paint);
            if (settings.IsCentered)
            {
                Center(canvas);
            }            
        }

        private void Center(SKCanvas canvas)
        {
            var x = (float)_width / 2f;
            var y = (float)_height / 2f;
            canvas.Translate(x, -y);
        }

        private SKPaint CreateDrawPaint(Scene scene, SKColor color)
        {
            using var pathEffect = CreatePathEffect(scene.Settings);
            using var shader = CreateShader(scene.Settings);
            using var colorFilter = CreateColorFilter(scene.Settings);
            return new SKPaint()
            {
                IsAntialias = scene.Settings.IsAntialias,
                IsStroke = scene.Settings.IsStroke,
                StrokeWidth = scene.Settings.StrokeWidth,
                IsDither = true,
                Color = color,
                PathEffect = pathEffect,
                Shader = shader,
                ColorFilter = colorFilter
            };
        }

        private static SKPaint CreateTextPaint(SKColor color)
        {
            var contrastConfig = new SKHighContrastConfig()
            {
                Contrast = 1.0f,
                Grayscale = false,
                InvertStyle = SKHighContrastConfigInvertStyle.NoInvert
            };
            var filter = SKColorFilter.CreateHighContrast(contrastConfig);
            return new SKPaint()
            {
                TextSize = 20f,
                IsDither = true,
                Color = color,
                ColorFilter = filter
            };
        }

        private static SKPathEffect CreatePathEffect(Settings settings)
        {
            SKPathEffect effect = null;
            if (settings.IsJittered)
            {
                effect = SKPathEffect.CreateDiscrete(1, 5f);
            }
            else if (settings.IsDashed)
            {
                effect = SKPathEffect.CreateDash(new[] { 1f, 2f, 3f, 2f }, 2f);
            }

            return effect;
        }

        private SKShader CreateShader(Settings settings)
        {
            SKShader shader = null;
            if (settings.UseGradient)
            {
                var p = new SKPoint()
                {
                    X = (float)_width / 2f,
                    Y = (float)_height / 2f
                };
                var arr = new[] { SKColors.Green, SKColors.Red, SKColors.Blue, SKColors.Yellow };
                shader = SKShader.CreateRadialGradient(p, (float)settings.Radius, arr, SKShaderTileMode.Mirror);
            }
            return shader;
        }

        private static SKColorFilter CreateColorFilter(Settings settings)
        {
            SKColorFilter colorFilter = null;
            if (settings.UseLighting && SKColor.TryParse(settings.ForegroundColor, out var color))
            {
                colorFilter = SKColorFilter.CreateLighting(color, SKColors.NavajoWhite);
            }
            return colorFilter;
        }
    }
}
