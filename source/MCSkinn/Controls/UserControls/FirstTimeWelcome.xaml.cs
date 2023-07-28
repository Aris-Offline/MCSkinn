using Inkore.Coreworks.Localization;
using Inkore.UI.WPF.Modern.Controls.Primitives;
using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using MCSkinn.Scripts;

namespace MCSkinn.Controls.UserControls
{
    /// <summary>
    /// FirstTimeWelcome.xaml 的交互逻辑
    /// </summary>
    public partial class FirstTimeWelcome : UserControl
    {
        public FirstTimeWelcome()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox_Language.ItemsSource = LanguageLoader.Languages;
            
            if(Program.CurrentLanguage != null)
                ComboBox_Language.SelectedItem = Program.CurrentLanguage;
            else
                ComboBox_Language.SelectedIndex = 0;

            if (!Inkore.Coreworks.Windows.Helpers.DirectoryHelper.HasPermission(Program.Dir_AssemblyParentCurrent, FileSystemRights.WriteAttributes))
            {
                ComboBoxItem_ConfigLocation_Application.IsEnabled = false;
            }

            ComboBox_ConfigLocation.SelectedIndex = 1;
        }

        private void ComboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBox_Language.SelectedItem is Language && (Language)ComboBox_Language.SelectedItem != Program.CurrentLanguage)
            {
                Program.CurrentLanguage = (Language)ComboBox_Language.SelectedItem;
            }

            TextBlock_Welcome.Text = Program.GetLanguageString("M_FIRSTIMEWELCOME");
            ComboBoxItem_ConfigLocation_Application.Content = Program.GetLanguageString("M_CONFIGINAPPLICATION");
            ComboBoxItem_ConfigLocation_User.Content = Program.GetLanguageString("M_CONFIGINUSER");
            ControlHelper.SetPlaceholderText(TextBox_Library, Program.GetLanguageString("M_SELECTSKINLIB"));
            Button_Library_Browse.ToolTip = Program.GetLanguageString("M_BROWSE");
        }

        System.Windows.Forms.FolderBrowserDialog dialog;

        private void Button_Library_Browse_Click(object sender, RoutedEventArgs e)
        {
            if(dialog == null)
            {
                dialog = new System.Windows.Forms.FolderBrowserDialog();
            }

            dialog.Description = Program.GetLanguageString("M_SELECTSKINLIBTIP");
            dialog.ShowNewFolderButton = true;

            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_Library.Text = dialog.SelectedPath;
            }
        }

        private void Button_FinishSetup_Click(object sender, RoutedEventArgs e)
        {
            if (!System.IO.Directory.Exists(TextBox_Library.Text))
            {
                if (MessageBox.Show(Program.GetLanguageString("M_INVALIDSKINLIBWARN"), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            }

            try
            {
                if (ComboBox_ConfigLocation.SelectedIndex == 0)
                {
                    GlobalSettings.FullPath_Config = IO.Path.Combine(Program.Dir_WorkdirCurrent, GlobalSettings.ConfigFilename);
                    
                }
                else
                {
                    if(!IO.Directory.Exists(Program.Dir_AppdataCurrent))
                        IO.Directory.CreateDirectory(Program.Dir_AppdataCurrent);             

                    GlobalSettings.FullPath_Config = IO.Path.Combine(Program.Dir_AppdataCurrent, GlobalSettings.ConfigFilename);

                    if (IO.File.Exists(IO.Path.Combine(Program.Dir_WorkdirCurrent, GlobalSettings.ConfigFilename)))
                        IO.File.Delete(IO.Path.Combine(Program.Dir_WorkdirCurrent, GlobalSettings.ConfigFilename));
                }

                GlobalSettings.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProgressRing_Finalizing.IsActive = true;
            ProgressRing_Finalizing.Visibility = Visibility.Visible;

            TextBlock_Welcome.Text = Program.GetLanguageString("M_COMINGRIGHTUP");

            (this.Resources["Storyboard_Finalizing"] as Storyboard).Begin();


            finalizeThread = new Thread(Finalizing);
            finalizeThread.Start();
        }


        private void Finalizing()
        {
            string libraryPath = null;

            Thread.Sleep(500);

            this.Dispatcher.Invoke(new Action(() =>
            {
                libraryPath = TextBox_Library.Text;
            }));

            if (IO.Directory.Exists(libraryPath))
            {
                Workfolder f = new Workfolder(libraryPath);
                f.Name = new IO.DirectoryInfo(libraryPath).Name;
                f.Color = Colors.CornflowerBlue;

                f.Initialize();

                SkinLibrary.RootFolders.Add(f.Root);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    GlobalSettings.SkinDirectories.Add(f);
                }));

            }

            GlobalSettings.Save();


            Thread.Sleep(500);

            this.Dispatcher.Invoke(Program.Context.DoneLoadingSplash);
        }

        Thread finalizeThread;
    }
}