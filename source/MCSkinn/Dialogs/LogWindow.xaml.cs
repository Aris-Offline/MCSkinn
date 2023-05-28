using Inkore.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            DataGrid_Logs.ItemsSource = Program.LogInstance.Logs;
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
    }
}
