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
using MCSkinn.Scripts.lemon42.Colors;

namespace MCSkinn.Forms.Controls
{
    public class SaturationSliderRenderer : SliderRenderer
    {
        public SaturationSliderRenderer(ColorSlider slider) :
            base(slider)
        {
        }

        public ColorManager CurrentColor { get; set; }

        public override void Render(Graphics g)
        {
            var colorRect = new Rectangle(0, Slider.Height / 2 - 3, Slider.Width - 6, 4);

            Color c1 = new ColorManager.HSVColor(CurrentColor.HSV.H, 0, CurrentColor.HSV.V).ToColor();
            Color c2 = new ColorManager.HSVColor(CurrentColor.HSV.H, 100, CurrentColor.HSV.V).ToColor();

            var brush = new LinearGradientBrush(colorRect, c1, c2, LinearGradientMode.Horizontal);

            //Draw color
            g.FillRectangle(brush, colorRect);
            //Draw border
            g.DrawRectangle(Pens.Black, colorRect);

            DrawThumb(g);
        }
    }
}