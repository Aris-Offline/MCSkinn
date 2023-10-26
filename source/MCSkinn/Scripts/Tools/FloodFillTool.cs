//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System;
using System.Collections;
using System.Drawing;
using WinForms = System.Windows.Forms;
using WPF = System.Windows;
using MCSkinn.Scripts.lemon42.Colors;
using MCSkinn.Scripts.Paril.Drawing;
using MCSkinn.Scripts.Paril.OpenGL;
using OpenTK;
using iNKORE.Coreworks.Windows.Helpers;

namespace MCSkinn.Scripts.Tools
{
    //Added threshold [Xylem] 09/11/2011
    public class FloodFillTool : ITool
    {
        private Rectangle _boundBox;
        private bool _done;
        private byte _threshold;
        private PixelsChangedUndoable _undo;

        public float Threshold //[0-1]
        {
            get { return GlobalSettings.Tool_FloodFill_Threshold; }
        }

        #region ITool Members

        public void SelectedBrushChanged()
        {
        }

        public void BeginClick(Skin skin, Point p, WPF.Input.MouseButton e)
        {
            _undo = new PixelsChangedUndoable(Editor.GetLanguageString("U_PIXELSCHANGED"),
                                              Program.Editor.SelectedTool.MenuItem.Text);
            _boundBox = new Rectangle(0, 0, skin.Width, skin.Height);

            if ((WinForms.Control.ModifierKeys & WinForms.Keys.Control) != 0)
                _boundBox = Editor.CurrentModel.GetTextureFaceBounds(new Point(p.X, p.Y), skin);

            _done = false;
        }

        public void MouseMove(Skin skin, Point p)
        {
        }

        public bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
        {
            if (_done)
                return false;

            var curve = new BezierCurveQuadric(new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 2));
            _threshold = (byte)((1 - curve.CalculatePoint(Threshold).X) * 255);
            //(byte)((1 - Math.Sin((1 - Threshold) * (Math.PI / 2))) * 255);

            ColorPixel c = pixels[x, y];
            Color oldColor = Color.FromArgb(c.Alpha, c.Red, c.Green, c.Blue);
            ColorManager newColor = new ColorManager(new ColorManager.RGBColor((WinForms.Control.ModifierKeys & WinForms.Keys.Shift) != 0
                                        ? Program.Editor.ColorPanel.SecondaryColor
                                        : Program.Editor.ColorPanel.SelectedColor));

            FloodFill(x, y, oldColor, newColor, pixels);
            _done = true;
            return true;
        }

        public bool RequestPreview(ColorGrabber pixels, Skin skin, int x, int y)
        {
            if (x == -1)
                return false;

            var highlightPoint = new Point(x, y);
            bool doHighlight = (WinForms.Control.ModifierKeys & WinForms.Keys.Control) != 0;

            Color newColor;
            if (doHighlight)
            {
                Rectangle part = Editor.CurrentModel.GetTextureFaceBounds(highlightPoint, skin);

                for (int ry = part.Y; ry < part.Y + part.Height; ++ry)
                {
                    for (int rx = part.X; rx < part.X + part.Width; ++rx)
                    {
                        ColorPixel px = pixels[rx, ry];
                        Color c = Color.FromArgb(px.Alpha, px.Red, px.Green, px.Blue);
                        Color blendMe = Color.FromArgb(64, Color.Green);
                        newColor = (Color)ColorBlending.AlphaBlend(blendMe, c);

                        pixels[rx, ry] = new ColorPixel(newColor.R << 0 | newColor.G << 8 | newColor.B << 16 | newColor.A << 24);
                    }
                }
            }

            newColor =
                ((WinForms.Control.ModifierKeys & WinForms.Keys.Shift) != 0
                     ? Program.Editor.ColorPanel.SecondaryColor
                     : Program.Editor.ColorPanel.SelectedColor).ToDrawingColor();
            pixels[x, y] = new ColorPixel(newColor.R | newColor.G << 8 | newColor.B << 16 | newColor.A << 24);
            return true;
        }

        public bool EndClick(ColorGrabber pixels, Skin skin, Point p, WPF.Input.MouseButton button)
        {
            if (_undo == null)
                return false;

            _done = false;
            if (_undo?.Points.Count > 0)
                skin.Undo.AddBuffer(_undo);
            _undo = null;

            Program.Editor.CheckUndo();
            return false;
        }

        public string GetStatusLabelText()
        {
            return Editor.GetLanguageString("T_FILL");
        }

        #endregion

        private static bool SimilarColor(Color color1, Color color2, byte threshold)
        {
            return
                Math.Abs(color1.R - color2.R) <= threshold &&
                Math.Abs(color1.G - color2.G) <= threshold &&
                Math.Abs(color1.B - color2.B) <= threshold &&
                Math.Abs(color1.A - color2.A) <= threshold
                ;
        }

        //Same as similarColor, but avoids some calculations if it can; use this if threshold may be 255 or 0
        private static bool SimilarColor2(Color color1, Color color2, byte threshold)
        {
            if (threshold == 255)
                return true;
            else if (threshold == 0)
            {
                return
                    color1.R == color2.R &&
                    color1.G == color2.G &&
                    color1.B == color2.B &&
                    color1.A == color2.A
                    ;
            }
            else
                return SimilarColor(color1, color2, threshold);
        }

        private void FloodFill(int x, int y, Color oldColor, ColorManager newColor, ColorGrabber pixels)
        {
            Queue q = new Queue();

            q.Enqueue(new Point(x, y));

            while (q.Count != 0)
            {
                Point pop = (Point)q.Dequeue();

                ColorPixel c = pixels[pop.X, pop.Y];
                Color real = Color.FromArgb(c.Alpha, c.Red, c.Green, c.Blue);

                if (!SimilarColor2(oldColor, real, _threshold))
                    continue;

                if (!_undo.Points.ContainsKey(pop))
                {
                    _undo.Points.Add(pop, Tuple.Create(real, new ColorAlpha(newColor.RGB, 0)));

                    pixels[pop.X, pop.Y] =
                        new ColorPixel(newColor.RGB.R | newColor.RGB.G << 8 | newColor.RGB.B << 16 | newColor.RGB.A << 24);

                    if (_boundBox.Contains(pop.X - 1, pop.Y))
                        q.Enqueue(new Point(pop.X - 1, pop.Y));
                    if (_boundBox.Contains(pop.X + 1, pop.Y))
                        q.Enqueue(new Point(pop.X + 1, pop.Y));
                    if (_boundBox.Contains(pop.X, pop.Y - 1))
                        q.Enqueue(new Point(pop.X, pop.Y - 1));
                    if (_boundBox.Contains(pop.X, pop.Y + 1))
                        q.Enqueue(new Point(pop.X, pop.Y + 1));
                }
            }
        }
    }
}