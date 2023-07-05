//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Windows.Forms;

namespace MCSkinn.Forms.Controls.Tools
{
    public class ToolOptionBase : UserControl
    {
        public ToolOptionBase()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            this.AutoScroll = true;
        }

        public virtual void BoxShown()
        {
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ToolOptionBase
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            Name = "ToolOptionBase";
            Size = new System.Drawing.Size(289, 115);
            ResumeLayout(false);
        }

        public virtual void BoxHidden()
        {
        }
    }
}