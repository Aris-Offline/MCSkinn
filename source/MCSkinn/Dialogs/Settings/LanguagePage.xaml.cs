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
    /// LanguagePage.xaml 的交互逻辑
    /// </summary>
    public partial class LanguagePage : Page
    {
        public LanguagePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterLanguage();
            ListView_Languages.ItemsSource = LanguageLoader.Languages;

            ListView_Languages.SelectedItem = Program.CurrentLanguage;

            Run_CurrentLanguage.Text = Program.CurrentLanguage.Name;
            Run_NewLang.Foreground = null;
        }
        public void RegisterLanguage()
        {
            LanguageWpf.Register(TextBlock_SelectLang, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_RestartWarning, TextBlock.TextProperty);

            LanguageWpf.Register(Run_CurrentLang, Run.TextProperty);
            LanguageWpf.Register(Run_NewLang, Run.TextProperty);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        public void UnegisterLanguage()
        {
            LanguageWpf.Unegister(TextBlock_SelectLang);
            LanguageWpf.Unegister(TextBlock_RestartWarning);

            LanguageWpf.Unegister(Run_CurrentLang);
            LanguageWpf.Unegister(Run_NewLang);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }

        private void ListView_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListView_Languages.SelectedItem != null && ListView_Languages.SelectedItem != Program.CurrentLanguage && ListView_Languages.SelectedItem is Language)
            {
                Run_NewLang.ClearValue(Run.ForegroundProperty);
                Program.CurrentLanguage = ListView_Languages.SelectedItem as Language;
                GlobalSettings.LanguageFile = System.IO.Path.GetFileName(Program.CurrentLanguage.FileName);
                GlobalSettings.Save();
                Run_NewLanguage.Text = Program.CurrentLanguage.Name;

                SettingsWindow.Instance?.NavigationViewControl_SelectionChanged(null, null);
            }
        }
    }
}
