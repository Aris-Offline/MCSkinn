//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Drawing;
using MCSkinn.Forms.Controls;
using MCSkinn.Forms.Controls.Tools;
using Brushes = MCSkinn.Forms.Controls.Brushes;
using Brush = MCSkinn.Forms.Controls.Brush;
using MCSkinn.Scripts;

namespace MCSkinn
{
    public partial class NoiseOptions : ToolOptionBase
    {
        private bool _skipSet;

        public NoiseOptions()
        {
            InitializeComponent();
        }

        public override void BoxShown()
        {
            BrushPanel.Controls.Add(Brushes.BrushBox);
            Brushes.BrushBox.Location = new Point(0, 0);
            Brushes.BrushBox.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public override void BoxHidden()
        {
            BrushPanel.Controls.Remove(Brushes.BrushBox);
        }
        private void EraserOptions_Load(object sender, EventArgs e)
        {
            SetExposure(GlobalSettings.Tool_Noise_Saturation);
        }

        private void SetExposure(float f)
        {
            _skipSet = true;
            numericUpDown1.Value = (decimal)(f * 100.0f);
            trackBar1.Value = (int)(f * 100.0f);
            _skipSet = false;

            GlobalSettings.Tool_Noise_Saturation = f;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_skipSet)
                return;

            SetExposure((float)numericUpDown1.Value / 100.0f);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_skipSet)
                return;

            SetExposure(trackBar1.Value / 100.0f);
        }
    }
}