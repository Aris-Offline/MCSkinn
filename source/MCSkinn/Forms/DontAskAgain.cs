//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Windows.Forms;
using Inkore.Coreworks.Localization;

namespace MCSkinn.Forms
{
    public partial class DontAskAgain : Form
    {
        public DontAskAgain()
        {
            InitializeComponent();
        }

        private void DontAskAgain_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Display a Don't Ask Again dialog, returns true if he clicked Yes.
        /// </summary>
        /// <param name="language">Language to use</param>
        /// <param name="labelValue">Label string</param>
        /// <param name="againValue">The current stored boolean and reference to the new one</param>
        /// <returns></returns>
        public static bool Show(Language language, string labelValue, ref bool dontShow)
        {
            if (dontShow)
                return true;

            using (var form = new DontAskAgain())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                try
                {
                    form.label1.Text = language.StringTable[labelValue];
                }
                catch(Exception ex) { form.label1.Text = labelValue; Program.Log(ex, false); }

                form.ShowDialog();

                dontShow = form.checkBox1.Checked;

                return form.DialogResult == DialogResult.Yes;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}