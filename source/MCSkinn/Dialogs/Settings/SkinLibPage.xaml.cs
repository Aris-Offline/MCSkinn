using MCSkinn.Scripts.Languages;
using MCSkinn.Scripts.Setting;
using SeaSharp.Styler.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

namespace MCSkinn.Dialogs.Settings
{
    /// <summary>
    /// SkinLibPage.xaml 的交互逻辑
    /// </summary>
    public partial class SkinLibPage : Page
    {
        public bool IsLibChanged = false;
        public SkinLibPage()
        {
            InitializeComponent();
        }

        public ObservableCollection<string> Libs = new ObservableCollection<string>();

        public void RegisterLanguage()
        {
            LanguageWpf.Register(TextBlock_SkinLibMgmt, TextBlock.TextProperty);

            LanguageWpf.Register(Button_AddFolder, ContentProperty);
            LanguageWpf.Register(Button_RemoveCurrent, ContentProperty);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        public void UnegisterLanguage()
        {
            LanguageWpf.Unegister(TextBlock_SkinLibMgmt);
            LanguageWpf.Unegister(Button_AddFolder);
            LanguageWpf.Unegister(Button_RemoveCurrent);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        private void ListView_Folders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Libs.Clear();
            foreach(string folder in GlobalSettings.SkinDirectories)
            {
                Libs.Add(folder);
            }
            ListView_Folders.ItemsSource= Libs;

            RegisterLanguage();
        }

        private void Button_AddFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog d = new VistaFolderBrowserDialog();
            if (d.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                Libs.Add(d.SelectedPath);
                IsLibChanged = true;
            }
        }

        private void Button_RemoveCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Folders.SelectedItem is string)
                Libs.Remove(ListView_Folders.SelectedItem as string); IsLibChanged = true;

        }

        private void Button_MoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Folders.SelectedIndex > 0)
                Libs.Move(ListView_Folders.SelectedIndex, ListView_Folders.SelectedIndex - 1); IsLibChanged = true;

        }

        private void Button_MoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Folders.SelectedIndex < Libs.Count - 1)
                Libs.Move(ListView_Folders.SelectedIndex, ListView_Folders.SelectedIndex + 1); IsLibChanged = true;


        }
    }

    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = new System.IO.DirectoryInfo(value as string).Name;
            if (v != null && v.Replace(" ", "") != "")
                return v;
            else
                return (value as string).Replace(@"\", "").Replace("/", "").Replace(".", "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
