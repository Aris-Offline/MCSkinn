//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Drawing;
using WPF = System.Windows;
using Forms = System.Windows.Forms;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public interface ITool
    {
        void BeginClick(Skin skin, Point p, WPF.Input.MouseButtonEventArgs e);
        void MouseMove(Skin skin, WPF.Input.MouseEventArgs e);
        bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y);
        bool RequestPreview(ColorGrabber pixels, Skin skin, int x, int y);
        bool EndClick(ColorGrabber pixels, Skin skin, WPF.Input.MouseButtonEventArgs e);
        string GetStatusLabelText();
        void SelectedBrushChanged();
    }
}