//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Drawing;
using System.Windows.Forms;
using Devcorp.Controls.Design;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class DarkenLightenTool : BrushToolBase
    {
        public bool IsInverted { get; set; } = false;

        public override bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return MouseMoveOnSkin(pixels, skin, x, y, GlobalSettings.Tool_DarkenLighten_Incremental);
        }

        public override Color BlendColor(Color l, Color r)
        {
            bool ctrlIng = (Control.ModifierKeys & Keys.Shift) != 0;
            bool switchTools = !IsInverted && ctrlIng ||
                               IsInverted && !ctrlIng;
            HSL hsl = ColorSpaceHelper.RGBtoHSL(r);
            float mod = l.A / 255.0f;

            if (switchTools)
                hsl.Luminance -= GlobalSettings.Tool_DarkenLighten_Exposure * mod / 5.0f;
            else
                hsl.Luminance += GlobalSettings.Tool_DarkenLighten_Exposure * mod / 5.0f;

            if (hsl.Luminance < 0)
                hsl.Luminance = 0;
            if (hsl.Luminance > 1)
                hsl.Luminance = 1;

            return Color.FromArgb(r.A, ColorSpaceHelper.HSLtoColor(hsl));
        }

        public override Color GetLeftColor()
        {
            return Color.FromArgb(255, 0, 0, 0);
        }

        public override string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_DARKENLIGHTEN");
        }
    }
}