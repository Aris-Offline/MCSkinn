//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Drawing;
using WinForms = System.Windows.Forms;
using WPF = System.Windows;
using Inkore.Coreworks.Windows.Helpers;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Tools
{
    public class CameraTool : ITool
    {
        private WinForms.Screen _clickedScreen;
        private Point _oldMouse;

        #region ITool Members

        public void BeginClick(Skin skin, Point p, WPF.Input.MouseButtonEventArgs e)
        {
            _oldMouse = e.GetPosition(Program.Form_Editor.Renderer).W2D();
            _clickedScreen = WinForms.Screen.FromPoint(WinForms.Cursor.Position);
        }

        public void SelectedBrushChanged()
        {
        }

        public void MouseMove(Skin skin, WPF.Input.MouseEventArgs e)
        {
            var delta = new Point((int)e.GetPosition(Program.Form_Editor.Renderer).X - _oldMouse.X, (int)e.GetPosition(Program.Form_Editor.Renderer).Y - _oldMouse.Y);
            Point position = WinForms.Cursor.Position;

            if (GlobalSettings.InfiniteMouse)
            {
                Rectangle screenBounds = _clickedScreen.Bounds;
                bool wasWrapped = false;
                Point oldMouseOnScreen = Editor.MainForm.Renderer.PointToScreen(_oldMouse.D2W()).W2D();

                if (position.X <= screenBounds.X && oldMouseOnScreen.X > screenBounds.X)
                {
                    WinForms.Cursor.Position = new Point(screenBounds.X + screenBounds.Width, position.Y);
                    wasWrapped = true;
                }
                else if (position.X >= screenBounds.X + screenBounds.Width - 1 &&
                         oldMouseOnScreen.X < screenBounds.X + screenBounds.Width - 1)
                {
                    WinForms.Cursor.Position = new Point(screenBounds.X, position.Y);
                    wasWrapped = true;
                }

                if (position.Y <= screenBounds.Y && oldMouseOnScreen.Y > screenBounds.Y)
                {
                    WinForms.Cursor.Position = new Point(position.X, screenBounds.Y + screenBounds.Height);
                    wasWrapped = true;
                }
                else if (position.Y >= screenBounds.Y + screenBounds.Height - 1 &&
                         oldMouseOnScreen.Y < screenBounds.Y + screenBounds.Height - 1)
                {
                    WinForms.Cursor.Position = new Point(position.X, screenBounds.Y);
                    wasWrapped = true;
                }

                if (wasWrapped)
                    _oldMouse = Editor.MainForm.GetRenderCursorPos();
                else
                    _oldMouse = e.GetPosition(Program.Form_Editor.Renderer).W2D();
            }
            else
                _oldMouse = e.GetPosition(Program.Form_Editor.Renderer).W2D();

            if (GetChangedButton(e) == Editor.MainForm.CameraRotate)
                Editor.MainForm.RotateView(delta, 1);
            else if (GetChangedButton(e) == Editor.MainForm.CameraZoom)
                Editor.MainForm.ScaleView(delta, 1);
            else if (GetChangedButton(e) == Editor.MainForm.CameraTranslate)
                Editor.MainForm.TranslateView(delta, 1);

        }

        public static WinForms.MouseButtons GetChangedButton(WPF.Input.MouseEventArgs e, WPF.Input.MouseButtonState state = WPF.Input.MouseButtonState.Pressed)
        {
            if (e.LeftButton == state)
                return WinForms.MouseButtons.Left;
            else if (e.MiddleButton == state)
                return WinForms.MouseButtons.Middle;
            else
                return WinForms.MouseButtons.Right;
        }

        public bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return false;
        }

        public bool RequestPreview(ColorGrabber pixels, Skin skin, int x, int y)
        {
            return false;
        }

        public bool EndClick(ColorGrabber pixels, Skin skin, WPF.Input.MouseButtonEventArgs e)
        {
            return false;
        }

        public string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_CAMERA");
        }

        #endregion
    }
}