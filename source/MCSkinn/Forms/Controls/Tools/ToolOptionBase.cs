//
//    MCSkinn, a 3d skin management studio for Minecraft
//    Copyright (C) 2013 Altered Softworks & MCSkinn Team
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
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