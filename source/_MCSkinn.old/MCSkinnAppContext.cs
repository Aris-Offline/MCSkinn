using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MCSkinn.Forms;
using MCSkinn.Scripts.Setting;

namespace MCSkinn
{
	class MCSkinnAppContext : ApplicationContext
	{
		public Form Form => Program.Form_Editor;
		public Dialogs.SplashWindow SplashForm => Program.Window_Splash;

		//public MCSkinnAppContext()
		//{
		//	Program.Context = this;
		//	Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		//	GlobalSettings.Load();

		//	try
		//	{
		//		if (Directory.Exists(GlobalSettings.GetDataURI("__updateFiles")))
		//			Directory.Delete(GlobalSettings.GetDataURI("__updateFiles"), true);
		//	}
		//	catch
		//	{
		//	}

		//	SplashForm = new Splash();

		//	Form = new Editor();
		//	Form.FormClosing += (sender, e) => GlobalSettings.Save();
		//	Form.FormClosed += (sender, e) => ExitThread();

		//	SplashForm.Show();
		//}

		public void DoneLoadingSplash()
		{
			Form.Show();
			Program.Window_Main.Show();
		}
	}
}
