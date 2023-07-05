//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace MCSkinn.Scripts.Paril.Windows.Dialogs
{
	public partial class ExceptionDialog : Form
	{
		private readonly Exception _exception;

		public ExceptionDialog()
		{
			InitializeComponent();
		}

		public ExceptionDialog(Exception e) :
			this()
		{
			_exception = e;
			exceptionName.Text = e.GetType().FullName;

			// general
			generalMessage.Text = e.Message;
			generalSource.Text = e.Source;
			generalTargetMethod.Text = FormatMethodBase(e.TargetSite);
			generalHelpLink.Text = e.HelpLink;

			// stack
			stackTrace.Text = e.StackTrace;

			// inner
			TreeNode curNode = null;
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				var node = new TreeNode(ex.GetType().ToString());

				if (curNode != null)
					curNode.Nodes.Add(node);
				else
					treeView1.Nodes.Add(node);

				curNode = node;

				var exceptionNode = new TreeNode(ex.Message);
				var messageNode = new TreeNode(FormatMethodBase(ex.TargetSite));

				curNode.Nodes.Add(exceptionNode);
				curNode.Nodes.Add(messageNode);
			}
		}

		public static string FormatMethodBase(MethodBase method)
		{
			if (method == null)
				return "Unknown";

			string str = "[" + method.Module.Name + "]" + " " + method;

			str += "(";
			bool started = false;
			foreach (ParameterInfo x in method.GetParameters())
			{
				if (started)
					str += ", ";
				else
					started = true;

				str += x.ParameterType + " " + x.Name;
			}
			str += ")";

			return str;
		}

		public static void Show(Exception e)
		{
			var d = new ExceptionDialog(e);
			SystemSounds.Asterisk.Play();
			d.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
			{
				sfd.RestoreDirectory = true;
				sfd.Filter = "Text files (*.txt)|*.txt";
				sfd.RestoreDirectory = true;

				if (sfd.ShowDialog() == DialogResult.OK)
					File.WriteAllText(sfd.FileName, _exception.ToString());
			}
		}

		private void ExceptionDialog_Load(object sender, EventArgs e)
		{
		}
	}
}