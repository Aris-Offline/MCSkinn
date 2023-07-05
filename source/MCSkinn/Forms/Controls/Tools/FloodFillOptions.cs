//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using MCSkinn.Forms.Controls.Tools;
using MCSkinn.Scripts;

namespace MCSkinn
{
    public partial class FloodFillOptions : ToolOptionBase
    {
        private bool _skipSet;

        public FloodFillOptions()
        {
            InitializeComponent();
        }

        private void DodgeBurnOptions_Load(object sender, EventArgs e)
        {
            SetThreshold(GlobalSettings.Tool_FloodFill_Threshold);
        }

        private void SetThreshold(float f)
        {
            _skipSet = true;
            numericUpDown1.Value = (decimal)(f * 100.0f);
            trackBar1.Value = (int)(f * 100.0f);
            _skipSet = false;

            GlobalSettings.Tool_FloodFill_Threshold = f;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_skipSet)
                return;

            SetThreshold((float)numericUpDown1.Value / 100.0f);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_skipSet)
                return;

            SetThreshold(trackBar1.Value / 100.0f);
        }
    }
}