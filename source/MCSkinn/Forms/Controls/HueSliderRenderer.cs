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
    public class HueSliderRenderer : SliderRenderer
    {
        public HueSliderRenderer(ColorSlider slider) :
            base(slider)
        {
        }

        public ColorManager CurrentColor { get; set; }

        public override void Render(Graphics g)
        {
            //theCode, love theVariableNames :D [Xylem]
            //Set the hue shades with the correct saturation and luminance
            Color[] theColors =
                {
                    new ColorManager.HSVColor(0, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(60, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(120, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(180, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(240, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(300, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                    new ColorManager.HSVColor(360, CurrentColor.HSV.S, CurrentColor.HSV.V).ToColor(),
                };

            //Calculate positions
            float percent = 1.0f / 6;
            float[] thePositions = { 0.0f, percent, percent * 2, percent * 3, percent * 4, percent * 5, 1.0f };
            //Set blend
            var theBlend = new ColorBlend();
            theBlend.Colors = theColors;
            theBlend.Positions = thePositions;
            //Get rectangle
            var colorRect = new Rectangle(0, Slider.Height / 2 - 3, Slider.Width - 6, 4);
            //Make the linear brush and assign the custom blend to it
            var theBrush = new LinearGradientBrush(colorRect,
                                                   Color.Red,
                                                   Color.Red, 0, false);
            theBrush.InterpolationColors = theBlend;
            //Draw rectangle
            g.FillRectangle(theBrush, colorRect);
            //Draw border and trackbar
            g.DrawRectangle(Pens.Black, new Rectangle(0, Slider.Height / 2 - 3, Slider.Width - 6, 4));
            DrawThumb(g);
        }
    }
}