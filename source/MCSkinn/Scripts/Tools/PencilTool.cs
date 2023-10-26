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
using iNKORE.Coreworks.Windows.Helpers;

namespace MCSkinn.Scripts.Tools
{
    public class PencilTool : BrushToolBase
    {
        public override bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return MouseMoveOnSkin(pixels, skin, x, y, GlobalSettings.Tool_Pencil_Incremental);
        }

        public override Color BlendColor(Color l, Color r)
        {
            return (Color)ColorBlending.AlphaBlend(l, r);
        }

        public override Color GetLeftColor()
        {
            return
                ((Control.ModifierKeys & Keys.Shift) != 0
                     ? Program.Editor.ColorPanel.SecondaryColor
                     : Program.Editor.ColorPanel.SelectedColor).ToDrawingColor();
        }

        public override string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_PENCIL");
        }
    }
}