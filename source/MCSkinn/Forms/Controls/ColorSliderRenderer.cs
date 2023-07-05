//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Drawing;
using System.Drawing.Drawing2D;
using MB.Controls;

namespace MCSkinn.Forms.Controls
{
    public class ColorSliderRenderer : SliderRenderer
    {
        public ColorSliderRenderer(ColorSlider slider) :
            base(slider)
        {
        }

        public Color StartColor { get; set; }

        public Color EndColor { get; set; }

        public override void Render(Graphics g)
        {
            //TrackBarRenderer.DrawHorizontalTrack(g, new Rectangle(0, (Slider.Height / 2) - 2, Slider.Width, 4));
            var colorRect = new Rectangle(0, Slider.Height / 2 - 3, Slider.Width - 6, 4);
            var brush = new LinearGradientBrush(colorRect, StartColor, EndColor, LinearGradientMode.Horizontal);
            g.FillRectangle(brush, colorRect);
            g.DrawRectangle(Pens.Black, colorRect);

            DrawThumb(g);
        }
    }
}