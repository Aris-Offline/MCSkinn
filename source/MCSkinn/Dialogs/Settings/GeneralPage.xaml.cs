using MCSkinn.Scripts.Languages;
using MCSkinn.Scripts.Setting;
using System;
using System.Collections.Generic;
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
    /// GeneralPage.xaml 的交互逻辑
    /// </summary>
    public partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            InitializeComponent();
        }

        private void Button_OpenKeyEditor_Click(object sender, RoutedEventArgs e)
        {
            Editor._shortcutEditor.ShowDialog();
        }

        private void CheckBox_InfiniteMouse_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.InfiniteMouse = CheckBox_InfiniteMouse.IsChecked.GetValueOrDefault(false);
            GlobalSettings.Save();
        }

        private void CheckBox_RenderStats_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.RenderBenchmark = CheckBox_RenderStats.IsChecked.GetValueOrDefault(false);
            GlobalSettings.Save();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox_InfiniteMouse.IsChecked = GlobalSettings.InfiniteMouse;
            CheckBox_RenderStats.IsChecked = GlobalSettings.RenderBenchmark;

            RegisterLanguage();
        }
        public void RegisterLanguage()
        {
            LanguageWpf.Register(TextBlock_Hotkey, TextBlock.TextProperty);
            LanguageWpf.Register(Button_OpenKeyEditor, ContentProperty);
            LanguageWpf.Register(CheckBox_InfiniteMouse, ContentProperty);
            LanguageWpf.Register(CheckBox_RenderStats, ContentProperty);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        public void UnegisterLanguage()
        {
            LanguageWpf.Unegister(TextBlock_Hotkey);
            LanguageWpf.Unegister(Button_OpenKeyEditor);
            LanguageWpf.Unegister(CheckBox_InfiniteMouse);
            LanguageWpf.Unegister(CheckBox_RenderStats);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }

    }
}
