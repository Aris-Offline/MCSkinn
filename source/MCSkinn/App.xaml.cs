using MCSkinn.Forms;
using MCSkinn.Scripts.Setting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace MCSkinn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            GlobalSettings.Load();

            try
            {
                if (Directory.Exists(GlobalSettings.GetDataURI("__updateFiles")))
                    Directory.Delete(GlobalSettings.GetDataURI("__updateFiles"), true);
            }
            catch
            {
            }

            Program.Window_Splash = new Dialogs.SplashWindow();

            Program.Form_Editor = new Editor();
            //Program.Form_Editor.FormClosing += (sender, e) => GlobalSettings.Save();
            //Program.Form_Editor.FormClosed += (sender, e) => ExitThread();

            Program.Window_Main = new MainWindow();
            this.MainWindow = Program.Window_Main;

            Program.Window_Splash.Show();



        }
    }
}
