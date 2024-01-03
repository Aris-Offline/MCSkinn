using iNKORE.Coreworks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using iNKORE.Coreworks.Windows.Helpers;
using System.Diagnostics;
using MCSkinn.Scripts;
using System;
using iNKORE.UI.WPF.Helpers;

namespace MCSkinn.Dialogs
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
            ListView_Logs.ItemsSource = Program.LogInstance.Logs;

            Program.LogInstance.Logs.CollectionChanged += Logs_CollectionChanged;
            Logs_CollectionChanged(null, null);
        }

        private void Logs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if(ToggleButton_AutoScroll.IsChecked == true)
                {
                    ScrollViewer sv = ControlHelper.FindVisualChild<ScrollViewer>(ListView_Logs);

                    if(sv != null)
                    {
                        sv.ScrollToEnd();
                    }
                }
            }));

        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog d = new System.Windows.Forms.SaveFileDialog();
            d.Filter = "Text Document (*.log)|*.log|All files|*.*";

            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(d.FileName, Program.LogInstance.ToString());
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(sender is ListViewItem && (sender as ListViewItem).DataContext is Log)
            {
                Log l = (sender as ListViewItem).DataContext as Log;
                MessageBoxImage i = MessageBoxImage.Information;
                switch (l.Type)
                {
                    case LogType.Info:
                        i = MessageBoxImage.Information;
                        break;
                    case LogType.Error:
                        i = MessageBoxImage.Error;
                        break;
                    case LogType.Warning:
                        i = MessageBoxImage.Warning;
                        break;
                    case LogType.Fatal:
                        i=MessageBoxImage.Stop; 
                        break;
                    case LogType.Load:
                        i = MessageBoxImage.Asterisk;
                        break;

                }
                MessageBox.Show(l.Summary + "\r\n\r\n" + l.Details, l.Time.ToString("F"), MessageBoxButton.OK, i);
            }
        }

        private void Button_OpenConsole_Click(object sender, RoutedEventArgs e)
        {
            if (Program.IsConsoleOpen)
            {
                ConsoleHelper.FreeConsole();
                Program.IsConsoleOpen = false;

                Program.Log(LogType.Info, "Console closed by user", "LogWindow.Button_OpenConsole_Click()");
            }
            else
            {
                ConsoleHelper.AllocConsole();
                Program.IsConsoleOpen = true;

                Program.Log(LogType.Info, "Console opened by user", "LogWindow.Button_OpenConsole_Click()");
            }
        }

        private void MenuItem_ThrowException_Click(object sender, RoutedEventArgs e)
        {
            throw new System.Exception("Test Exception thrown by user.");
        }

        private void Button_OpenConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer", "/select,\"" + GlobalSettings.FullPath_Config + "\"");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.Log(ex, "LogWindow.Button_OpenConfig_Click()");
            }
        }

        private void Button_OpenConfig_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start("notepad", GlobalSettings.FullPath_Config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.Log(ex, "LogWindow.Button_OpenConfig_Click()");
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Program.LogInstance.Logs.CollectionChanged -= Logs_CollectionChanged;
            }
            catch(Exception ex) { Program.Log(ex); }
        }
    }
}
