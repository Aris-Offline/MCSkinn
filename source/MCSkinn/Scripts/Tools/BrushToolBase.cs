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
using MCSkinn.Scripts.lemon42.Colors;
using MCSkinn.Scripts.Paril.OpenGL;
using Brush = MCSkinn.Forms.Controls.Brush;
using Brushes = MCSkinn.Forms.Controls.Brushes;

namespace MCSkinn.Scripts.Tools
{
    /// <summary>
    /// A simple base class to make brush tools with.
    /// </summary>
    public abstract class BrushToolBase : ITool
    {
        private Point _oldPixel = new Point(-1, -1);
        private PixelsChangedUndoable _undo;
        public bool IsPreview { get; private set; }

        #region ITool Members

        public virtual void BeginClick(Skin skin, Point p, WPF.Input.MouseButton button)
        {
            _undo = new PixelsChangedUndoable(Editor.GetLanguageString("U_PIXELSCHANGED"),
                                              Editor.MainForm.SelectedTool.MenuItem.Text);
        }

        public virtual void MouseMove(Skin skin, Point p)
        {
        }

        public void SelectedBrushChanged()
        {
        }

        public virtual bool RequestPreview(ColorGrabber pixels, Skin skin, int x, int y)
        {
            if (x == -1)
                return false;

            Brush brush = Brushes.SelectedBrush;
            int startX = x - brush.Width / 2;
            int startY = y - brush.Height / 2;
            IsPreview = true;

            for (int ry = 0; ry < brush.Height; ++ry)
            {
                for (int rx = 0; rx < brush.Width; ++rx)
                {
                    int xx = startX + rx;
                    int yy = startY + ry;

                    if (xx < 0 || xx >= skin.Width ||
                        yy < 0 || yy >= skin.Height)
                        continue;

                    if (brush[rx, ry] == 0.0f)
                        continue;

                    ColorPixel c = pixels[xx, yy];
                    Color oldColor = Color.FromArgb(c.Alpha, c.Red, c.Green, c.Blue);
                    Color color = GetLeftColor();
                    color = Color.FromArgb((byte)(brush[rx, ry] * 255 * (color.A / 255.0f)), color);

                    Color newColor = BlendColor(color, oldColor);
                    pixels[xx, yy] = new ColorPixel(newColor.R | newColor.G << 8 | newColor.B << 16 | newColor.A << 24);
                }
            }

            return true;
        }

        public virtual bool EndClick(ColorGrabber pixels, Skin skin, Point p, WPF.Input.MouseButton button)
        {
            if (_undo?.Points.Count > 0)
            {
                skin.Undo.AddBuffer(_undo);
                Editor.MainForm.CheckUndo();
                _oldPixel = new Point(-1, -1);
            }

            _undo = null;

            return false;
        }

        public abstract bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y);
        public abstract string GetStatusLabelText();

        #endregion

        public bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y, bool incremental)
        {
            if (x == _oldPixel.X && y == _oldPixel.Y)
                return false;
            IsPreview = false;

            Brush brush = Brushes.SelectedBrush;
            int startX = x - brush.Width / 2;
            int startY = y - brush.Height / 2;

            for (int ry = 0; ry < brush.Height; ++ry)
            {
                for (int rx = 0; rx < brush.Width; ++rx)
                {
                    int xx = startX + rx;
                    int yy = startY + ry;

                    if (xx < 0 || xx >= skin.Width ||
                        yy < 0 || yy >= skin.Height)
                        continue;

                    if (brush[rx, ry] == 0.0f)
                        continue;

                    ColorPixel c = pixels[xx, yy];
                    Color oldColor = Color.FromArgb(c.Alpha, c.Red, c.Green, c.Blue);
                    ColorManager.RGBColor color =new ColorManager.RGBColor
                        ((WinForms.Control.ModifierKeys & WinForms.Keys.Shift) != 0
                             ? Editor.MainForm.ColorPanel.SecondaryColor
                             : Editor.MainForm.ColorPanel.SelectedColor);

                    byte maxAlpha = color.A;
                    var alphaToAdd = (float)(byte)(brush[rx, ry] * 255 * (Editor.MainForm.ColorPanel.SelectedColor.A / 255.0f));

                    if (!incremental && _undo.Points.ContainsKey(new Point(xx, yy)) &&
                        _undo.Points[new Point(xx, yy)].Item2.TotalAlpha >= maxAlpha)
                        continue;

                    if (!incremental && _undo.Points.ContainsKey(new Point(xx, yy)) &&
                        _undo.Points[new Point(xx, yy)].Item2.TotalAlpha + alphaToAdd >= maxAlpha)
                        alphaToAdd = maxAlpha - _undo.Points[new Point(xx, yy)].Item2.TotalAlpha;

                    color = Color.FromArgb((byte)alphaToAdd, color);

                    Color newColor = BlendColor(color, oldColor);

                    if (oldColor == newColor)
                        continue;

                    if (_undo.Points.ContainsKey(new Point(xx, yy)))
                    {
                        Tuple<Color, ColorAlpha> tupl = _undo.Points[new Point(xx, yy)];
                        _undo.Points[new Point(xx, yy)] = Tuple.Create(tupl.Item1, new ColorAlpha(newColor, tupl.Item2.TotalAlpha + alphaToAdd));
                    }
                    else
                        _undo.Points.Add(new Point(xx, yy), Tuple.Create(oldColor, new ColorAlpha(newColor, alphaToAdd)));

                    pixels[xx, yy] = new ColorPixel(newColor.R | newColor.G << 8 | newColor.B << 16 | newColor.A << 24);
                }
            }

            _oldPixel = new Point(x, y);

            return true;
        }

        public abstract Color BlendColor(Color l, Color r);
        public abstract Color GetLeftColor();
    }
}