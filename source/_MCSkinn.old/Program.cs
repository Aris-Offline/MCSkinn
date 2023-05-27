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

using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using WinForms = System.Windows.Forms;
using MCSkinn.ExceptionHandler;
using WPF = System.Windows;
using MCSkinn.Scripts.Languages;
using System.Windows.Forms;

namespace MCSkinn
{
    internal static class Program
	{
		public const string Name = "MCSkinn";
		public static Version Version;
		public static MCSkinnAppContext Context = new MCSkinnAppContext();

		public static Dialogs.SplashWindow Window_Splash;
        public static Editor? Form_Editor;
		public static App App_Main;
		public static MainWindow Window_Main;

		public const string VersionFull = "0.6.2";

		public static Stream GetResourceStream(string name)
		{
			name = "MCSkinn.Resources." + name;
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		}

		public static string GetResourceString(string name, Encoding encoding)
		{
			using (var stream = GetResourceStream(name))
			using (var br = new StreamReader(stream, encoding))
				return br.ReadToEnd();
		}

		public static string GetResourceString(string name)
		{
			return GetResourceString(name, Encoding.ASCII);
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			//AppDomain.CurrentDomain.AssemblyResolve +=
			//(sender, args) =>
			//{
			//	using (Stream stream = GetResourceStream(new AssemblyName(args.Name).Name + ".dll"))
			//	{
			//		if (stream == null)
			//			return null;

			//		var assemblyData = new Byte[stream.Length];
			//		stream.Read(assemblyData, 0, assemblyData.Length);
			//		return Assembly.Load(assemblyData);
			//	}
			//};

			MainCore();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void MainCore()
		{
			MainThread = Thread.CurrentThread;

#if !DEBUG
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif


#if !DEBUG
			//Version.Revision = Repository.Revision;
#endif

#if !DEBUG
			try
			{
#endif

#if CONVERT_MODELS
				Models.Convert.ConversionInterface.Convert();
#endif
			WinForms.Control.CheckForIllegalCrossThreadCalls = false;
			WinForms.Application.EnableVisualStyles();
			WinForms.Application.SetCompatibleTextRenderingDefault(true);
			//WinForms.Application.Run(new MCSkinnAppContext());

			App_Main = new App();
			App_Main.InitializeComponent();
			App_Main.Run();

#if !DEBUG
			}
			catch (Exception ex)
			{
				RaiseException(ex);
			}
#endif
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			RaiseException(e.ExceptionObject as Exception);
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			RaiseException(e.Exception);
		}

		public static void RaiseException(Exception ex)
		{
			if (Editor.MainForm.IsHandleCreated)
			{
				var raiseForm = (WinForms.Form)Editor.MainForm;
                if (raiseForm.InvokeRequired)
                {
                    raiseForm.Invoke((Action)delegate () { RaiseException(ex); });
                    return;
                }

            }
			else
			{
				Program.Context.SplashForm.Dispatcher.Invoke((Action)delegate () { RaiseException(ex); });
			}
   //         var raiseForm = Editor.MainForm.IsHandleCreated ? (WinForms.Form)Editor.MainForm : (WinForms.Form)Program.Context.SplashForm;

			//if (raiseForm.InvokeRequired)
			//{
			//	raiseForm.Invoke((Action)delegate () { RaiseException(ex); });
			//	return;
			//}

			var form = new ExceptionForm();
			form.Exception = ex;

			if (Editor.CurrentLanguage == null)
				Editor.CurrentLanguage = Language.Parse(new StreamReader(new MemoryStream(Properties.Resources.English)));

			SystemSounds.Asterisk.Play();

			if (Editor.MainForm.Visible)
				form.ShowDialog(Editor.MainForm);
			else
				form.ShowDialog();
		}

		public static Thread MainThread { get; set; }
	}
}