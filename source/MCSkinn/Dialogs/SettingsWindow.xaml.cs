using MCSkinn.Scripts;
using MCSkinn.Scripts.Languages;
using MCSkinn.Scripts.Setting;
using Inkore.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MCSkinn.Dialogs
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public static SettingsWindow Instance { get; private set; }
        public SettingsWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        Settings.GeneralPage Page_General = null;
        Settings.LanguagePage Page_Language = null;
        Settings.SkinLibPage Page_SkinLib = null;
        Settings.RenderPage Page_Render = null;
        Settings.AboutPage Page_About = null;

        public void RegisterLanguage()
        {
            LanguageWpf.Register(NavigationViewItem_General, ContentProperty);
            LanguageWpf.Register(NavigationViewItem_Library, ContentProperty);
            LanguageWpf.Register(NavigationViewItem_Render, ContentProperty);
            LanguageWpf.Register(NavigationViewItem_Language, ContentProperty);
            LanguageWpf.Register(NavigationViewItem_Help, ContentProperty);
            LanguageWpf.Register(NavigationViewItem_About, ContentProperty);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        public void UnegisterLanguage()
        {
            LanguageWpf.Unegister(NavigationViewControl);

            LanguageWpf.Unegister(NavigationViewItem_General);
            LanguageWpf.Unegister(NavigationViewItem_Library);
            LanguageWpf.Unegister(NavigationViewItem_Render);
            LanguageWpf.Unegister(NavigationViewItem_Language);
            LanguageWpf.Unegister(NavigationViewItem_Help);
            LanguageWpf.Unegister(NavigationViewItem_About);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterLanguage();

            NavigationViewControl.SelectedItem = NavigationViewItem_General;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnegisterLanguage();
            
            if (Page_Render != null)
            {
                Page_Render.UnegisterLanguage();
            }
            if (Page_General != null)
            {
                Page_General.UnegisterLanguage();
            }
            if(Page_Language != null)
            {
                Page_Language.UnegisterLanguage();
            }
            if (Page_SkinLib != null)
            {
                if (Page_SkinLib.IsLibChanged)
                {
                    GlobalSettings.SkinDirectories = Page_SkinLib.Libs.ToArray();
                    GlobalSettings.Save();

                    SkinLoader.LoadSkins();
                }

                Page_SkinLib.UnegisterLanguage();
            }

            Instance = null;
        }

        internal void NavigationViewControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewControl.Header = (NavigationViewControl.SelectedItem as NavigationViewItem).Content as string;

            switch((NavigationViewControl.SelectedItem as NavigationViewItem).Tag as string)
            {
                case "GENERAL":
                    if (Page_General == null) Page_General = new Settings.GeneralPage();
                    Frame_Contents.Navigate(Page_General);
                    break;
                case "SKINLIB":
                    if (Page_SkinLib == null) Page_SkinLib = new Settings.SkinLibPage();
                    Frame_Contents.Navigate(Page_SkinLib);
                    break;
                case "RENDER":
                    if (Page_Render == null) Page_Render = new Settings.RenderPage();
                    Frame_Contents.Navigate(Page_Render);
                    break;
                case "LANG":
                    if (Page_Language == null) Page_Language = new Settings.LanguagePage();
                    Frame_Contents.Navigate(Page_Language);
                    break;

                //case "HELP":
                //    if (Page_General == null) Page_General = new Settings.GeneralPage();
                //    Frame_Contents.Navigate(Page_General);
                //    break;
                case "ABOUT":
                    if (Page_About == null) Page_About = new Settings.AboutPage();
                    Frame_Contents.Navigate(Page_About);
                    break;
            }
        }

        private void Frame_Contents_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if(!(e.Content is FrameworkElement))
            {
                e.Cancel = true;
            }
        }
    }
}
