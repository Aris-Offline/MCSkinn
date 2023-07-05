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
using MCSkinn.Scripts;

namespace MCSkinn
{
    public partial class DarkenLightenOptions : ToolOptionBase
    {
        private bool _skipSet;

        public DarkenLightenOptions()
        {
            InitializeComponent();
        }

        public bool Inverted
        {
            get { return radioButton2.Checked; }
        }

        private void DodgeBurnOptions_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = GlobalSettings.Tool_DarkenLighten_Incremental;
            SetExposure(GlobalSettings.Tool_DarkenLighten_Exposure);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.Tool_DarkenLighten_Incremental = checkBox1.Checked;
        }

        private void SetExposure(float f)
        {
            _skipSet = true;
            numericUpDown1.Value = (decimal)(f * 100.0f);
            trackBar1.Value = (int)(f * 100.0f);
            _skipSet = false;

            GlobalSettings.Tool_DarkenLighten_Exposure = f;
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
    }
}