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
        void BeginClick(SkinNode skin, Point p, WPF.Input.MouseButton button);
        void MouseMove(SkinNode skin, Point p);
        bool MouseMoveOnSkin(ColorGrabber pixels, SkinNode skin, int x, int y);
        bool RequestPreview(ColorGrabber pixels, SkinNode skin, int x, int y);
        bool EndClick(ColorGrabber pixels, SkinNode skin, Point p, WPF.Input.MouseButton button);
        string GetStatusLabelText();
        void SelectedBrushChanged();
    }
}