//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Windows.Forms;
using Devcorp.Controls.Design;

namespace MCSkinn.Scripts.Paril.Controls.Color
{
	public partial class ColorPicker : Form
	{
		private bool _skipSet;

		public ColorPicker()
		{
			InitializeComponent();
		}

		public HSL MyHSL
		{
			get { return new HSL(colorSquare1.CurrentHue, colorSquare1.CurrentSat / 240.0f, saturationSlider1.CurrentLum / 240.0f); }
		}

		public System.Drawing.Color CurrentColor
		{
			get { return System.Drawing.Color.FromArgb((int)numericUpDown4.Value, ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor()); }
		}

		private void ColorPicker_Load(object sender, EventArgs e)
		{
			panel1.BackColor = ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor();
			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			SetColors();
		}

		private void SetColors()
		{
			_skipSet = true;
			numericUpDown5.Value = CurrentColor.R;
			numericUpDown6.Value = CurrentColor.G;
			numericUpDown7.Value = CurrentColor.B;
			_skipSet = false;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			colorSquare1.CurrentHue = (int)numericUpDown1.Value;
			SetColors();
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			colorSquare1.CurrentSat = (int)numericUpDown2.Value;
			SetColors();
		}

		private void colorSquare1_HueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			_skipSet = true;
			numericUpDown1.Value = colorSquare1.CurrentHue;

			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor();
			SetColors();
			_skipSet = false;
		}

		private void colorSquare1_SatChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			_skipSet = true;
			numericUpDown2.Value = colorSquare1.CurrentSat;

			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor();
			SetColors();
			_skipSet = false;
		}

		private void saturationSlider1_LumChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			_skipSet = true;
			numericUpDown3.Value = saturationSlider1.CurrentLum;

			panel1.BackColor = ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor();
			SetColors();
			_skipSet = false;
		}

		private void numericUpDown3_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			saturationSlider1.CurrentLum = (int)numericUpDown3.Value;
			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = ColorSpaceHelper.HSLtoRGB(MyHSL).ToColor();
		}

		private void numericUpDown5_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			System.Drawing.Color asRGB = System.Drawing.Color.FromArgb((int)numericUpDown5.Value, (int)numericUpDown6.Value,
																	   (int)numericUpDown7.Value);
			HSL hsl = ColorSpaceHelper.RGBtoHSL(asRGB);

			_skipSet = true;

			numericUpDown1.Value = (int)hsl.Hue;
			numericUpDown2.Value = (int)(hsl.Saturation * 240.0f);
			numericUpDown3.Value = (int)(hsl.Luminance * 240.0f);

			colorSquare1.CurrentHue = (int)numericUpDown1.Value;
			colorSquare1.CurrentSat = (int)numericUpDown2.Value;
			saturationSlider1.CurrentLum = (int)numericUpDown3.Value;
			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = asRGB;

			_skipSet = false;
		}

		private void numericUpDown6_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			System.Drawing.Color asRGB = System.Drawing.Color.FromArgb((int)numericUpDown5.Value, (int)numericUpDown6.Value,
																	   (int)numericUpDown7.Value);
			HSL hsl = ColorSpaceHelper.RGBtoHSL(asRGB);

			_skipSet = true;

			numericUpDown1.Value = (int)hsl.Hue;
			numericUpDown2.Value = (int)(hsl.Saturation * 240.0f);
			numericUpDown3.Value = (int)(hsl.Luminance * 240.0f);

			colorSquare1.CurrentHue = (int)numericUpDown1.Value;
			colorSquare1.CurrentSat = (int)numericUpDown2.Value;
			saturationSlider1.CurrentLum = (int)numericUpDown3.Value;
			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = asRGB;

			_skipSet = false;
		}

		private void numericUpDown7_ValueChanged(object sender, EventArgs e)
		{
			if (_skipSet)
				return;

			System.Drawing.Color asRGB = System.Drawing.Color.FromArgb((int)numericUpDown5.Value, (int)numericUpDown6.Value,
																	   (int)numericUpDown7.Value);
			HSL hsl = ColorSpaceHelper.RGBtoHSL(asRGB);

			_skipSet = true;

			numericUpDown1.Value = (int)hsl.Hue;
			numericUpDown2.Value = (int)(hsl.Saturation * 240.0f);
			numericUpDown3.Value = (int)(hsl.Luminance * 240.0f);

			colorSquare1.CurrentHue = (int)numericUpDown1.Value;
			colorSquare1.CurrentSat = (int)numericUpDown2.Value;
			saturationSlider1.CurrentLum = (int)numericUpDown3.Value;
			saturationSlider1.Color = new HSL(colorSquare1.CurrentHue, (double)colorSquare1.CurrentSat / 240.0f, 0);
			panel1.BackColor = asRGB;

			_skipSet = false;
		}

		private void numericUpDown4_ValueChanged(object sender, EventArgs e)
		{
		}
	}
}