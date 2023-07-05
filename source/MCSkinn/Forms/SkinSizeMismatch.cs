//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Windows.Forms;

namespace MCSkinn.Forms
{
	public partial class SkinSizeMismatch : Form
	{
		public SkinSizeMismatch()
		{
			InitializeComponent();
		}

		public ResizeType Result
		{
			get;
			set;
		}

		/// <summary>
		/// Display a Don't Ask Again dialog, returns true if he clicked Yes.
		/// </summary>
		/// <param name="language">Language to use</param>
		/// <param name="labelValue">Label string</param>
		/// <param name="againValue">The current stored boolean and reference to the new one</param>
		/// <returns></returns>
		public static ResizeType Show(string labelValue)
		{
			using (var form = new SkinSizeMismatch())
			{
				form.StartPosition = FormStartPosition.CenterParent;
				form.label1.Text = labelValue;

				form.ShowDialog();

				return form.Result;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Result = ResizeType.Scale;
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Result = ResizeType.None;
			Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Result = ResizeType.Crop;
			Close();
		}
	}

	public enum ResizeType
	{
		None,
		Crop,
		Scale
	}
}