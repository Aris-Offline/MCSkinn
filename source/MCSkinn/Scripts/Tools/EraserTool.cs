//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Drawing;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class EraserTool : BrushToolBase
    {
        public override Color BlendColor(Color l, Color r)
        {
            return Color.FromArgb(0, 0, 0, 0);
        }

        public override bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return MouseMoveOnSkin(pixels, skin, x, y, false);
        }

        public override Color GetLeftColor()
        {
            return Color.FromArgb(0, 0, 0, 0);
        }

        public override string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_ERASER");
        }
    }
}