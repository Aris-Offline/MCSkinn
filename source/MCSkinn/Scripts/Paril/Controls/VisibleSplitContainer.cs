//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCSkinn.Forms.Controls;
using Brushes = System.Drawing.Brushes;

namespace MCSkinn.Scripts.Paril.Controls
{
    public class VisibleSplitContainer : SplitContainer
    {
        private int _numGripBumps = 5;

        [DefaultValue(5)]
        public int NumGripBumps
        {
            get { return _numGripBumps; }
            set
            {
                _numGripBumps = value;
                Invalidate();
            }
        }

        private void DrawBump(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Gray, x + 1, y + 1, 2, 2);
            g.FillRectangle(Brushes.White, x, y, 2, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!IsSplitterFixed)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    for (int i = 0, x = Width / 2 - _numGripBumps * 3 / 2; i < _numGripBumps; ++i, x += 3 + 1)
                        DrawBump(e.Graphics, x, SplitterDistance);
                }
                else
                {
                    for (int i = 0, y = Height / 2 - _numGripBumps * 3 / 2; i < _numGripBumps; ++i, y += 3 + 1)
                        DrawBump(e.Graphics, SplitterDistance, y);
                }
            }

            base.OnPaint(e);
        }
    }
}