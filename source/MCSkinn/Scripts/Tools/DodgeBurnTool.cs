//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Drawing;
using System.Windows.Forms;
using MCSkinn.Scripts.Paril.Drawing;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class DodgeBurnTool : BrushToolBase
    {
        public bool IsInverted { get; set; } = false;

        public override bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return MouseMoveOnSkin(pixels, skin, x, y, GlobalSettings.Tool_DodgeBurn_Incremental);
        }

        public override Color BlendColor(Color l, Color r)
        {
            bool ctrlIng = (Control.ModifierKeys & Keys.Shift) != 0;
            bool switchTools = !IsInverted && ctrlIng ||
                               IsInverted && !ctrlIng;
            float mod = l.A / 255.0f;

            if (switchTools)
                return Color.FromArgb(ColorBlending.Burn(r, 1 - GlobalSettings.Tool_DodgeBurn_Exposure * mod / 10.0f).ToArgb());
            else
                return Color.FromArgb(ColorBlending.Dodge(r, GlobalSettings.Tool_DodgeBurn_Exposure * mod / 10.0f).ToArgb());
        }

        public override Color GetLeftColor()
        {
            return Color.FromArgb(255, 0, 0, 0);
        }

        public override string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_DODGEBURN");
        }
    }
}