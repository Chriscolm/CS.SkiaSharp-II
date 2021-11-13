using CS.SkiaSharpExample.Elements.Contracts.Models;
using CS.SkiaSharpExample.Renderer.Contracts;
using SkiaSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CS.SkiaSharpExample.Renderer
{
    public class Renderer : IRenderer
    {
        private double _width;
        private double _height;
        private Scene _scene;

        public void Render(object surface, double surfaceWidth, double surfaceHeight, Scene scene)
        {
            _width = surfaceWidth;
            _height = surfaceHeight;

            if (surface is SKSurface skSurface)
            {
                Render(skSurface, scene);
            }

            ColorInfo();
        }

        private void ColorInfo()
        {
            void OutFormat(SKColor c)
            {
                System.Diagnostics.Debug.WriteLine($"R: {c.Red} G: { c.Green} B: {c.Blue} A: {c.Alpha}");
            }
            //var red = SKColors.Red;            
            //var green = new SKColor(0, 255, 0, 255);
            //var blue = new SKColor(0, 0, 255);
            //SKColor.TryParse("#1000FF00", out var transparentGreen);

            //OutFormat(red);
            //OutFormat(green);
            //OutFormat(blue);
            //OutFormat(transparentGreen);

            var redHsv = SKColor.FromHsv(0, 100, 100);
            var redHsl = SKColor.FromHsl(0, 100, 50);
            redHsv.ToHsl(out var h, out var s, out var l);
            System.Diagnostics.Debug.Assert(h == 0 && s == 100 && l == 50);

            SKColor.TryParse("#7F00FF7", out var c1); // AARRGGB
            SKColor.TryParse("#7F00FF7F", out var c1a); // AARRGGBB ?
            SKColor.TryParse("#00FF7F", out var c2); // RRGGBB
            SKColor.TryParse("A0FA", out var c3); // ARGB
            SKColor.TryParse("#0FA", out var c4); // RGB
            //OutFormat(redHsv);
            
        }

        private void Render(SKSurface surface, Scene scene)
        {
            var canvas = surface.Canvas;

            Center(canvas);

            if (SKColor.TryParse(scene.Settings.BackgroundColor, out var color))
            {
                canvas.Clear(color);
            }
            if (scene.Settings.MakeNoise)
            {
                RenderPerlinNoise(canvas, scene.Settings);
            }
            RenderElements(canvas, scene);
        }

        private void RenderElements(SKCanvas canvas, Scene scene)
        {
            _scene = scene;
            foreach (var element in scene.Elements)
            {
                if(SKColor.TryParse(element.Settings.ForegroundColor, out var color))
                {
                    using var paint = CreateDrawPaint(scene, element, color);
                    RenderElement(canvas, element, scene.Settings, paint);
                }
            }
        }

        private void RenderElement(SKCanvas canvas, Element element, GeneralSettings settings, SKPaint paint)
        {
            if (element is Circle circle)
            {
                RenderRounded(canvas, circle, settings, paint);
            }
            else if (element is Caption caption)
            {
                RenderText(canvas, caption, settings);
            }
            else if (element is BitmapElement)
            {
                RenderBitmap(canvas, settings);
            }
            else
            {
                RenderAngular(canvas, element, settings, paint);
            }
        }

        private void RenderAngular(SKCanvas canvas, Element element, GeneralSettings settings, SKPaint paint)
        {
            var points = from q in element.Coordinates
                         let p = Translator.Translate(new SKPoint(q.Xf, q.Yf), _height, settings.Scale)
                         select p;
            using var path = new SKPath();
            path.AddPoly(points.ToArray(), close: true);
            canvas.DrawPath(path, paint);
        }

        private void RenderRounded(SKCanvas canvas, Circle element, GeneralSettings settings, SKPaint paint)
        {
            var point = from q in element.Coordinates
                        let p = Translator.Translate(new SKPoint(q.Xf, q.Yf), _height, settings.Scale)
                        select p;
            var v = point.Single();
            var r = (float)(element.Radius * settings.Scale);
            canvas.DrawCircle(v, r, paint);
        }

        private void RenderText(SKCanvas canvas, Caption element, GeneralSettings settings)
        {
            canvas.ResetMatrix();
            var b = SKColor.TryParse(element.Settings.ForegroundColor, out var color);
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
        }

        private void Center(SKCanvas canvas)
        {
            if (_scene != null)
            {
                var coordinates = _scene.Elements.SelectMany(p => p.Coordinates).ToArray();
                if (!coordinates.Any())
                {
                    return;
                }
                var minx = coordinates.Min(p => p.Xf);
                var maxx = coordinates.Max(p => p.Xf);
                var miny = coordinates.Min(p => p.Yf);
                var maxy = coordinates.Max(p => p.Yf);
                var xext = maxx - minx;
                var yext = maxy - miny;
                var x = (float)_width / 2f - xext / 2f;
                var y = (float)_height / 2f - yext / 2f;
                canvas.Translate(x, -y); 
            }
        }

        private SKPaint CreateDrawPaint(Scene scene, Element element, SKColor color)
        {            
            using var highlightShader = CreateRadialSpecularHighlight(element.Settings);
            using var shadowFilter = CreateShadow(element.Settings);
            using var blurFilter = CreateBlurFilter(element.Settings);
            return new SKPaint()
            {
                IsAntialias = scene.Settings.IsAntialias,                
                Color = color,                
                Shader = highlightShader,
                ImageFilter = shadowFilter,
                MaskFilter = blurFilter 
            };
        }

        private static SKPaint CreateTextPaint(SKColor color, float textSize = 20f, float strokeWidth = 2f)
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
                TextSize = textSize,
                IsDither = true,
                Color = color,
                ColorFilter = filter,
                StrokeWidth = strokeWidth,
                IsStroke = true
            };
        }
                        
        private SKImageFilter CreateShadow(ElementSettings settings)
        {
            if(settings.UseShadow && SKColor.TryParse(settings.ShadowColor, out var color))
            {
                var dx = 20f * (float)_scene.Settings.Scale;
                var dy = 15f * (float)_scene.Settings.Scale;
                var filter = SKImageFilter.CreateDropShadow(dx, dy, 8, 8, color);
                return filter;
            }
            return null;
        }

        private SKShader CreateRadialSpecularHighlight(ElementSettings settings)
        {
            if (settings.UseHighlight && SKColor.TryParse(settings.ForegroundColor, out var color))
            {                
                var scale = (float)_scene.Settings.Scale;
                var highlightColor = SKColors.AntiqueWhite;
                var r = (float)(settings.Radius * scale);
                var p = new SKPoint((float)settings.X - (float)settings.Radius / 2f, 0 - (float)settings.Radius / 2f);
                var center = Translator.Translate(p, _height, scale);
                var shader = SKShader.CreateRadialGradient(center, r, new[] { highlightColor, color }, null, SKShaderTileMode.Clamp);
                return shader;
            }
            return null;
        }                

        private SKMaskFilter CreateBlurFilter(ElementSettings settings)
        {
            if (settings.UseBlur && Enum.TryParse<SKBlurStyle>(settings.BlurStyle, out var style))
            {
                var sigma = settings.BlurSigma * (float)_scene.Settings.Scale;
                var filter = SKMaskFilter.CreateBlur(style, sigma);
                return filter;
            }
            return null;
        }

        private void RenderBitmap(SKCanvas canvas, GeneralSettings settings)
        {
            canvas.ResetMatrix();
            
            var resourceId = "CS.SkiaSharpExample.Renderer.neuwerk-quer.jpg";
            using var resource = GetType().Assembly.GetManifestResourceStream(resourceId);
            using var codec = SKCodec.Create(resource);
            using var bmp = SKBitmap.Decode(codec);

            using var grayScalePaint = CreateGrayScale(settings);

            var resized = FitToScreen(bmp);
            CenterBitmap(canvas, resized.Info.Size);
            canvas.DrawBitmap(resized, new SKPoint(0, 0), grayScalePaint);

            if (settings.IsClipped)
            {
                Clip(canvas, resized);
            }

            if (settings.HasWatermark)
            {
                AddWatermark(canvas);
            }
        }

        private static SKPaint CreateGrayScale(GeneralSettings settings)
        {
            if (!settings.IsGrayScale)
            {
                return null;
            }
            var matrix = new float[]
            {
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0,     0,     0,     1, 0
            };
            var paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(matrix)
            };
            return paint;
        }

        private static SKPaint CreateRandomScale(GeneralSettings settings)
        {
            if (!settings.IsClipped)
            {
                return null;
            }
            var random = new Random();
            var r = (float)random.NextDouble();
            var g = (float)random.NextDouble();
            var b = (float)random.NextDouble();
            var matrix = new float[]
            {
                r, r, r, 0, 0,
                g, g, g, 0, 0,
                b, b, b, 0, 0,
                0, 0, 0, 1, 0
            };
            var paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(matrix)
            };
            return paint;
        }

        private SKBitmap FitToScreen(SKBitmap bmp)
        {
            var currentWidth = (double)bmp.Width;
            var currentHeight = (double)bmp.Height;
            var scale = Math.Min(_width / currentWidth, _height / currentHeight);
            var info = new SKImageInfo()
            {
                Width = (int)(currentWidth * scale),
                Height = (int)(currentHeight * scale),
                AlphaType = bmp.Info.AlphaType,
                ColorSpace = bmp.Info.ColorSpace,
                ColorType = bmp.Info.ColorType
            };
            var fitted = new SKBitmap(info);
            return bmp.ScalePixels(fitted, SKFilterQuality.Medium) ? fitted : bmp;
        }

        private void CenterBitmap(SKCanvas canvas, SKSizeI size)
        {
            var dx = (float)(_width - size.Width) / 2f;
            var dy = (float)(_height - size.Height) / 2f;
            canvas.Translate(dx, dy);
        }

        private void AddWatermark(SKCanvas canvas)
        {
            var x = 10f;
            var y = (float)_height - 40;
            using var paint = CreateTextPaint(new SKColor(250, 250, 250, 100), 40, 5);
            canvas.DrawText("Watermark", new SKPoint(x,y), paint);
        }

        private static void Clip(SKCanvas canvas, SKBitmap bmp)
        {
            var rect = SKRect.Create(0, 0, bmp.Width / 2f, bmp.Height / 2);
            canvas.ClipRect(rect, SKClipOperation.Intersect, true);
            canvas.DrawBitmap(bmp, new SKPoint(0, 0), CreateRandomScale(new GeneralSettings() { IsClipped = true }));
        }

        private void RenderPerlinNoise(SKCanvas canvas, GeneralSettings settings)
        {
            canvas.ResetMatrix();
            using var shader =
                settings.HasPerlinFractalNoise ?
                SKShader.CreatePerlinNoiseFractalNoise(settings.PerlinBaseFrequencyX, settings.PerlinBaseFrequencyY, settings.PerlinNumOctaves, DateTime.Now.Ticks) :
                settings.HasPerlinTurbulenceNoise ?
                SKShader.CreatePerlinNoiseTurbulence(settings.PerlinBaseFrequencyX, settings.PerlinBaseFrequencyY, settings.PerlinNumOctaves, DateTime.Now.Ticks) :
                null;
            if(shader == null)
            {
                return;
            }
            using var paint = new SKPaint()
            {
                IsAntialias = true,
                Shader = shader,
            };
            canvas.DrawRect(0, 0, (float)_width, (float)_height, paint);
        }
    }
}
