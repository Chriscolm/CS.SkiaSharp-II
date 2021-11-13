using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class ElementSettings
    {
        public event Action Changed;

        private string _foregroundColor = "#AABBCC";
        private bool _isStroke = true;
        private float _strokeWidth = 2f;
        private bool _isJittered;
        private bool _isDashed;
        private bool _useGradient;
        private bool _clearEffects;
        private bool _useLighting;
        private double _radius;
        private bool _useShadow;
        private string _shadowColor = "#DD000000";
        private bool _useHighlight;
        private bool _useBlur;
        private string _blurStyle = "Normal";
        private float _blurSigma = 20f;

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
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Changed?.Invoke();
            }
        }
        public double X { get; set; }
        public bool UseShadow
        {
            get => _useShadow;
            set
            {
                _useShadow = value;
                Changed?.Invoke();
            }
        }
        public string ShadowColor
        {
            get => _shadowColor;
            set
            {
                _shadowColor = value;
                Changed?.Invoke();
            }
        }
        public bool UseHighlight
        {
            get => _useHighlight;
            set
            {
                _useHighlight = value;
                Changed?.Invoke();
            }
        }
        public bool UseBlur
        {
            get => _useBlur;
            set
            {
                _useBlur = value;
                Changed?.Invoke();
            }
        }
        public string BlurStyle
        {
            get => _blurStyle;
            set
            {
                _blurStyle = value;
                Changed?.Invoke();
            }
        }
        public float BlurSigma
        {
            get => _blurSigma;
            set
            {
                _blurSigma = value;
                Changed?.Invoke();
            }
        }
        public IEnumerable<string> BlurStyles
        {
            get => new[] { "Normal", "Solid", "Inner", "Outer" };
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
