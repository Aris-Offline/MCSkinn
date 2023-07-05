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
    public partial class PencilOptions : ToolOptionBase
    {
        public PencilOptions()
        {
            InitializeComponent();
        }

        private void PencilOptions_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = GlobalSettings.Tool_Pencil_Incremental;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.Tool_Pencil_Incremental = checkBox1.Checked;
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