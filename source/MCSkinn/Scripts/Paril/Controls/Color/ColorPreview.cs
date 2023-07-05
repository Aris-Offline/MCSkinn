//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MCSkinn.Scripts.Paril.Controls.Color
{
    public class ColorPreview : Control
    {
        public ColorPreview()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
        }

        public override System.Drawing.Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var brush = new HatchBrush(HatchStyle.LargeCheckerBoard, System.Drawing.Color.Gray, System.Drawing.Color.LightGray);

            e.Graphics.FillRectangle(brush, ClientRectangle);

            e.Graphics.FillPolygon(new SolidBrush(System.Drawing.Color.FromArgb(255, ForeColor)), new[]
                                                                                                  {
                                                                                                      new Point(Width, 0),
                                                                                                      new Point(Width, Height),
                                                                                                      new Point(0, Height)
                                                                                                  });

            e.Graphics.FillPolygon(new SolidBrush(ForeColor), new[]
                                                              {
                                                                  new Point(0, 0),
                                                                  new Point(Width, 0),
                                                                  new Point(0, Height)
                                                              });

            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Etched);

            base.OnPaint(e);
        }
    }
}