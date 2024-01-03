//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Drawing;
using WinForms = System.Windows.Forms;
using WPF = System.Windows;
using Devcorp.Controls.Design;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class NoiseTool : BrushToolBase
    {
        private Random _noise, _noise2;
        private int _seed;

        public NoiseTool()
        {
            _noise = new Random();
            _seed = _noise.Next();
            _noise = _noise2 = new Random(_seed);
        }

        public override Color BlendColor(Color l, Color r)
        {
            Color c = r;
            HSB hsv = ColorSpaceHelper.RGBtoHSB(c);
            hsv.Brightness += ((IsPreview ? _noise : _noise2).NextDouble() - 0.5f) * 2 * GlobalSettings.Tool_Noise_Saturation;

            if (hsv.Brightness < 0)
                hsv.Brightness = 0;
            if (hsv.Brightness > 1)
                hsv.Brightness = 1;

            return Color.FromArgb(r.A, ColorSpaceHelper.HSBtoColor(hsv));
        }

        public override void BeginClick(SkinNode skin, Point p, WPF.Input.MouseButton button)
        {
            _noise2 = new Random(_seed);
            base.BeginClick(skin, p, button);
        }

        public override Color GetLeftColor()
        {
            return Color.White;
        }

        public override bool MouseMoveOnSkin(ColorGrabber pixels, SkinNode skin, int x, int y)
        {
            return MouseMoveOnSkin(pixels, skin, x, y, false);
        }

        public override string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_NOISE");
        }

        public override bool RequestPreview(ColorGrabber pixels, SkinNode skin, int x, int y)
        {
            _noise = new Random(_seed);
            return base.RequestPreview(pixels, skin, x, y);
        }

        public override bool EndClick(ColorGrabber pixels, SkinNode skin, Point p, WPF.Input.MouseButton button)
        {
            base.EndClick(pixels, skin, p, button);
            _seed = _noise.Next();
            _noise = _noise2 = new Random(_seed);

            return false;
        }
    }
}