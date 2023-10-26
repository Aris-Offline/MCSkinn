//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Drawing;
using WinForms = System.Windows.Forms;
using WPF = System.Windows;
using MCSkinn.Scripts.lemon42.Colors;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class DropperTool : ITool
    {
        #region ITool Members

        public void BeginClick(Skin skin, Point p, WPF.Input.MouseButton e)
        {
        }

        public void MouseMove(Skin skin, Point p)
        {
        }

        public void SelectedBrushChanged()
        {
        }

        public bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            ColorPixel c = pixels[x, y];
            ColorManager oldColor = ColorManager.FromRGBA(c.Red, c.Green, c.Blue, c.Alpha);

            if ((WinForms.Control.ModifierKeys & WinForms.Keys.Shift) != 0)
                Program.Editor.ColorPanel.SecondaryColor = oldColor.W();
            else
                Program.Editor.ColorPanel.SelectedColor = oldColor.W();
            return false;
        }

        public bool RequestPreview(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return false;
        }

        public bool EndClick(ColorGrabber pixels, Skin skin, Point p, WPF.Input.MouseButton button)
        {
            return false;
        }

        public string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_DROPPER");
        }

        #endregion
    }
}