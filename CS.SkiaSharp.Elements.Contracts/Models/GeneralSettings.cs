using System;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class GeneralSettings
    {
        public event Action Changed;

        private string _backgroundColor = "#112233";
        private double _scale = 1d;
        private bool _parallel;
        private bool _isAntialias = true;
        private bool _isGrayScale;
        private bool _hasWatermark;
        private bool _isClipped;
        private bool _hasPerlinFractalNoise = true;
        private bool _hasPerlinTurbulenceNoise;
        private float _perlinBaseFrequencyX = 0.005f;
        private float _perlinBaseFrequencyY = 0.005f;
        private int _perlinNumOctaves = 2;
        private bool _makeNoise;
        private bool _autoSave;

        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Changed?.Invoke();
            }
        }
        public double Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                Changed?.Invoke();
            }
        }
        public bool Parallel
        {
            get => _parallel;
            set
            {
                _parallel = value;
                Changed?.Invoke();
            }
        }
        public bool IsAntialias
        {
            get => _isAntialias;
            set
            {
                _isAntialias = value;
                Changed?.Invoke();
            }
        }

        public bool IsGrayScale
        {
            get => _isGrayScale;
            set
            {
                _isGrayScale = value;
                Changed?.Invoke();
            }
        }

        public bool HasWatermark
        {
            get => _hasWatermark;
            set
            {
                _hasWatermark = value;
                Changed?.Invoke();
            }
        }

        public bool IsClipped
        {
            get => _isClipped;
            set
            {
                _isClipped = value;
                Changed?.Invoke();
            }
        }

        public bool HasPerlinFractalNoise
        {
            get => _hasPerlinFractalNoise;
            set
            {
                _hasPerlinFractalNoise = value;
                if (value)
                {
                    Changed?.Invoke();
                }
            }
        }

        public bool HasPerlinTurbulenceNoise
        {
            get => _hasPerlinTurbulenceNoise;
            set
            {
                _hasPerlinTurbulenceNoise = value;
                if (value)
                {
                    Changed?.Invoke();
                }
            }
        }

        public float PerlinBaseFrequencyX
        {
            get => _perlinBaseFrequencyX;
            set
            {
                _perlinBaseFrequencyX = value;
                Changed?.Invoke();
            }
        }
        
        public float PerlinBaseFrequencyY
        {
            get => _perlinBaseFrequencyY;
            set
            {
                _perlinBaseFrequencyY = value;
                Changed?.Invoke();
            }
        }
        
        public int PerlinNumOctaves
        {
            get => _perlinNumOctaves;
            set
            {
                _perlinNumOctaves = value;
                Changed?.Invoke();
            }
        }

        public bool MakeNoise
        {
            get => _makeNoise;
            set
            {
                _makeNoise = value;
                Changed?.Invoke();
            }
        }

        public bool AutoSave
        {
            get => _autoSave;
            set
            {
                _autoSave = value;
                Changed?.Invoke();
            }
        }
    }
}
