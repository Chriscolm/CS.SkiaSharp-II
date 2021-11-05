using System;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Settings
    {
        public event Action Changed;

        private string _backgroundColor = "#112233";
        private string _foregroundColor = "#AABBCC";
        private double _scale = 1d;
        private bool _parallel;
        private int _count = 1;
        private bool _isAntialias;
        private bool _isCentered;
        private bool _isStroke = true;
        private double _radius = 10f;
        private float _strokeWidth = 2f;
        private bool _isJittered;
        private bool _isDashed;
        private bool _useGradient;
        private bool _clearEffects;
        private bool _useLighting;

        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Changed?.Invoke();
            }
        }
        public string ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                Changed?.Invoke();
            }
        }
        public float StrokeWidth
        {
            get => _strokeWidth;
            set
            {
                _strokeWidth = value;
                Changed?.Invoke();
            }
        }
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
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
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
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

        public bool IsCentered
        {
            get => _isCentered;
            set
            {
                _isCentered = value;
                Changed?.Invoke();
            }
        }

        public bool IsStroke
        {
            get => _isStroke;
            set
            {
                _isStroke = value;
                Changed?.Invoke();
            }
        }

        public bool IsJittered
        {
            get => _isJittered;
            set
            {
                _isJittered = value;
                Changed?.Invoke();
            }
        }

        public bool IsDashed
        {
            get => _isDashed;
            set
            {
                _isDashed = value;
                Changed?.Invoke();
            }
        }

        public bool UseGradient
        {
            get => _useGradient;
            set
            {
                _useGradient = value;
                Changed?.Invoke();
            }
        }

        public bool UseLighting
        {
            get => _useLighting;
            set
            {
                _useLighting = value;
                Changed?.Invoke();
            }
        }

        public bool ClearEffects
        {
            get => _clearEffects;
            set
            {
                _clearEffects = value;
                if (value)
                {
                    IsDashed = false;
                    IsJittered = false;
                    UseGradient = false;
                    UseLighting = false;
                }
            }
        }
    }
}
