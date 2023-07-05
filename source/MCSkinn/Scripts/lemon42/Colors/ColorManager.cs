//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Drawing;

namespace MCSkinn.Scripts.lemon42.Colors
{
    //ColorManager class that will help provide simple and efficient
    //color translations.

    public class ColorManager
    {
        #region Delegates

        public delegate void ColorChangedEventHandler();

        #endregion

        #region ColorSpace enum

        public enum ColorSpace
        {
            RGB = 0,
            HSV = 1
        }

        #endregion

        private ColorSpace _colorspace;
        private HSVColor _hsv;
        private RGBColor _rgb;

        public ColorManager()
        {
            _colorspace = ColorSpace.RGB;
            _rgb = new RGBColor(0, 0, 0, 255);
            _hsv = _rgb.ToHSVColor();
        }

        public ColorManager(RGBColor rgb)
        {
            _colorspace = ColorSpace.RGB;
            _rgb = rgb;
            _hsv = _rgb.ToHSVColor();
        }

        public ColorManager(HSVColor hsv)
        {
            _colorspace = ColorSpace.HSV;
            _hsv = hsv;
            _rgb = _hsv.ToRGBColor();
        }

        public ColorSpace CurrentSpace
        {
            get { return _colorspace; }
            set { _colorspace = value; }
        }

        public RGBColor RGB
        {
            get { return _rgb; }
            set
            {
                _rgb = value;
                _hsv = value.ToHSVColor();
                _colorspace = ColorSpace.RGB;
                if (ColorChanged != null)
                    ColorChanged();
            }
        }
        public System.Windows.Media.Color W()
        {
            return System.Windows.Media.Color.FromArgb(RGB.A, RGB.R, RGB.G, RGB.B);
        }
        public System.Drawing.Color D()
        {
            return System.Drawing.Color.FromArgb(RGB.A, RGB.R, RGB.G, RGB.B);
        }
        public HSVColor HSV
        {
            get { return _hsv; }
            set
            {
                _hsv = value;
                _rgb = value.ToRGBColor();
                _colorspace = ColorSpace.HSV;
                if (ColorChanged != null)
                    ColorChanged();
            }
        }

        public static HSVColor RGBtoHSV(Color c)
        {
            return RGBtoHSV(c.R, c.G, c.B, c.A);
        }

        public static HSVColor RGBtoHSV(byte r, byte g, byte b, byte a)
        {
            //init variables
            double h = 0;
            double s = 0;
            double v = 0;

            // R,G,B ∈ [0, 1]
            double red = r / 255.0f;
            double green = g / 255.0f;
            double blue = b / 255.0f;

            //other variables
            double max = Math.Max(Math.Max(red, green), blue);
            double min = Math.Min(Math.Min(red, green), blue);
            double chroma = max - min;

            //CALCULATION OF HUE
            if (max == min)
                h = 0;
            else if (max == red)
                h = (60 * (green - blue) / chroma + 360) % 360;
            else if (max == green)
                h = 60 * (blue - red) / chroma + 120;
            else if (max == blue)
                h = 60 * (red - green) / chroma + 240;

            //CALCULATION OF SATURATION
            s = max == 0 ? 0 : 1 - min / max;
            //CALCULATION OF VALUE
            v = max;

            //V,S is a %
            v = Math.Round(v * 100);
            s = Math.Round(s * 100);
            h = Math.Round(h);
            return new HSVColor((short)h, (byte)s, (byte)v, a);
        }

        public static Color HSVtoRGB(HSVColor c)
        {
            return HSVtoRGB(c.H, c.S, c.V, c.A);
        }

        public static Color HSVtoRGB(short h, byte s, byte v, byte a)
        {
            double r = 0;
            double g = 0;
            double b = 0;
            double hue = h;
            if (hue >= 360)
                hue = hue - 360;
            double sat = s / 100.0f;
            double val = v / 100.0f;
            //variables and calculation
            var tempT = (int)Math.Floor(hue / 60 % 6);
            double f = hue / 60.0f - tempT;
            double l = val * (1 - sat);
            double m = val * (1 - f * sat);
            double n = val * (1 - (1 - f) * sat);

            switch (tempT)
            {
                case 0:
                    r = val;
                    g = n;
                    b = l;
                    break;
                case 1:
                    r = m;
                    g = val;
                    b = l;
                    break;
                case 2:
                    r = l;
                    g = val;
                    b = n;
                    break;
                case 3:
                    r = l;
                    g = m;
                    b = val;
                    break;
                case 4:
                    r = n;
                    g = l;
                    b = val;
                    break;
                case 5:
                    r = val;
                    g = l;
                    b = m;
                    break;
            }

            r = Math.Round(r * 255);
            g = Math.Round(g * 255);
            b = Math.Round(b * 255);

            return Color.FromArgb(a, (byte)r, (byte)g, (byte)b);
        }

        public event ColorChangedEventHandler ColorChanged;

        public static ColorManager FromRGBA(byte r, byte g, byte b, byte a)
        {
            return new ColorManager(new RGBColor(r, g, b, a));
        }

        public static ColorManager FromHSVA(int h, byte s, byte v, byte a)
        {
            return new ColorManager(new HSVColor((short)h, s, v, a));
        }

        #region Nested type: HSVColor

        public struct HSVColor
        {
            public byte A;
            public short H;
            public byte S;
            public byte V;

            public HSVColor(short hue, byte saturation, byte value, byte alpha)
            {
                H = hue;
                S = saturation;
                V = value;
                A = alpha;
            }

            public HSVColor(short hue, byte saturation, byte value)
            {
                H = hue;
                S = saturation;
                V = value;
                A = 255;
            }

            public static explicit operator Color(HSVColor c)
            {
                return c.ToColor();
            }

            public static explicit operator RGBColor(HSVColor c)
            {
                return c.ToRGBColor();
            }

            public static explicit operator HSVColor(Color c)
            {
                return RGBtoHSV(c);
            }

            //(to)
            public Color ToColor()
            {
                return HSVtoRGB(H, S, V, A);
            }

            public RGBColor ToRGBColor()
            {
                Color c = ToColor();
                return new RGBColor(c.R, c.G, c.B, c.A);
            }

            public override string ToString()
            {
                return "HSVA(" + H + "º, " + S + "%, " + V + "%, " + A + ")";
            }
        }

        #endregion

        #region Nested type: RGBColor

        public struct RGBColor
        {
            public byte A;
            public byte B;
            public byte G;
            public byte R;

            public RGBColor(byte red, byte green, byte blue, byte alpha)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }

            public RGBColor(byte red, byte green, byte blue)
            {
                R = red;
                G = green;
                B = blue;
                A = 255;
            }
            public RGBColor(System.Windows.Media.Color c)
            {
                R = c.R;
                G = c.G;
                B = c.B;
                A = c.A;
            }
            public RGBColor(System.Drawing.Color c)
            {
                R = c.R;
                G = c.G;
                B = c.B;
                A = c.A;
            }
            public RGBColor(ColorPicker.Models.NotifyableColor c)
            {
                R = (byte)Math.Min(255, Math.Max(0, c.RGB_R));
                G = (byte)Math.Min(255, Math.Max(0, c.RGB_G));
                B = (byte)Math.Min(255, Math.Max(0, c.RGB_B));
                A = (byte)Math.Min(255, Math.Max(0, c.A));
            }

            public static implicit operator Color(RGBColor c)
            {
                return c.ToColor();
            }

            public static explicit operator HSVColor(RGBColor c)
            {
                return c.ToHSVColor();
            }

            public static implicit operator RGBColor(Color c)
            {
                return new RGBColor(c.R, c.G, c.B, c.A);
            }

            //(to)
            public Color ToColor()
            {
                return Color.FromArgb(A, R, G, B);
            }

            public HSVColor ToHSVColor()
            {
                return RGBtoHSV(R, G, B, A);
            }

            public override string ToString()
            {
                return "RGBA(" + R + ", " + G + ", " + B + ", " + A + ")";
            }
        }

        #endregion
    }
}