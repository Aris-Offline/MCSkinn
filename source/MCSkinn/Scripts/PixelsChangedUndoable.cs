//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System;
using System.Collections.Generic;
using System.Drawing;
using MCSkinn.Scripts.Paril.Components.Undo;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts
{
    public struct ColorAlpha
    {
        public Color Color;
        public float TotalAlpha;

        public ColorAlpha(Color c, float alpha) :
            this()
        {
            Color = c;
            TotalAlpha = alpha;
        }
    }

    public class PixelsChangedUndoable : IUndoable
    {
        public Dictionary<Point, Tuple<Color, ColorAlpha>> Points = new Dictionary<Point, Tuple<Color, ColorAlpha>>();

        public PixelsChangedUndoable(string action)
        {
            Action = action + " [" + DateTime.Now.ToString("h:mm:ss") + "]";
        }

        public PixelsChangedUndoable(string action, string tool) :
            this(action + " (" + tool + ")")
        {
        }

        #region IUndoable Members

        public string Action { get; private set; }

        public void Undo(object obj)
        {
            var skin = (Skin)obj;

            using (var grabber = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                grabber.Load();

                foreach (var kvp in Points)
                {
                    Point p = kvp.Key;
                    Tuple<Color, ColorAlpha> color = kvp.Value;
                    grabber[p.X, p.Y] =
                        new ColorPixel(color.Item1.R | color.Item1.G << 8 | color.Item1.B << 16 | color.Item1.A << 24);

                    if (!Program.Editor.PaintedPixels.ContainsKey(p))
                        Program.Editor.PaintedPixels.Add(p, true);
                }

                grabber.Save();
            }

            Program.Editor.SetPartTransparencies();
        }

        public void Redo(object obj)
        {
            var skin = (Skin)obj;

            using (var grabber = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                grabber.Load();

                foreach (var kvp in Points)
                {
                    Point p = kvp.Key;
                    Tuple<Color, ColorAlpha> color = kvp.Value;
                    grabber[p.X, p.Y] =
                        new ColorPixel(color.Item2.Color.R | color.Item2.Color.G << 8 | color.Item2.Color.B << 16 |
                                       color.Item2.Color.A << 24);

                    if (!Program.Editor.PaintedPixels.ContainsKey(p))
                        Program.Editor.PaintedPixels.Add(p, true);
                }

                grabber.Save();
            }

            Program.Editor.SetPartTransparencies();
        }

        #endregion
    }
}