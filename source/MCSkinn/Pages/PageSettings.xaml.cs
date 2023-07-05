using Inkore.Coreworks.Localization;
using Inkore.Coreworks.Windows.Helpers;
using MCSkinn.Dialogs;
using MCSkinn.Scripts;
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

namespace MCSkinn.Pages
{
    /// <summary>
    /// PageSettings.xaml 的交互逻辑
    /// </summary>
    public partial class PageSettings : Page
    {
        #region Initialization
        public PageSettings()
        {
            InitializeComponent();
        }

        private bool isLoading = true;

        public void RegisterLanguage()
        {
            LanguageWpf.Register(this, TabItem_Options, TabItem.HeaderProperty);
            LanguageWpf.Register(this, TabItem_Hotkeys, TabItem.HeaderProperty);

            LanguageWpf.Register(this, ComboBoxItem_ThemeMode_Light, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_ThemeMode_Dark, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_ThemeMode_Default, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_Backdrop_None, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_Backdrop_Acrylic, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_Backdrop_Mica, ContentProperty);
            LanguageWpf.Register(this, ComboBoxItem_Backdrop_Tabbed, ContentProperty);
            LanguageWpf.Register(this, Button_UIScale_Apply, ContentProperty);
            LanguageWpf.Register(this, TextBlock_Language, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Language_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Language_Author, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Language_Version, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_ThemeMode, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_ThemeMode_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Backdrop, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Backdrop_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_RenderScale, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_RenderScale_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_UIScale, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_UIScale_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay_LineColor, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay_LineSize, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay_TextColor, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_TextureOverlay_TextSize, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_PixelGrid, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_PixelGrid_Description, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_PixelGrid_Color, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_RememberPrefers, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_RememberPrefers_Description, TextBlock.TextProperty);

            LanguageWpf.Register(this, TextBlock_Section_General, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Section_Appearance, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Section_Rendering, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_Section_Advanced, TextBlock.TextProperty);

            LanguageWpf.Register(this, Button_OpenKeyEditor, ContentProperty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterLanguage();

            LoadSettings();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            LanguageWpf.UnregisterContainer(this);
        }

        public void LoadSettings()
        {
            isLoading = true;


            ComboBox_Language.ItemsSource = LanguageLoader.Languages;
            ComboBox_Language.SelectedItem = LanguageLoader.Current;
            TextBlock_Language_Author_Value.Text = LanguageLoader.Current.Author;
            TextBlock_Language_Version_Value.Text = LanguageLoader.Current.LangVersion;

            ComboBox_ThemeMode.SelectedIndex = (int)GlobalSettings.RequestedTheme;
            ComboBox_Backdrop.SelectedIndex = (int)GlobalSettings.BackdropType;
            Slider_UIScale.Value = GlobalSettings.UIScale;

            Slider_RenderScale.Value = GlobalSettings.RenderScale;
         
            ToggleSwitch_TextureOverlay_Enabled.IsOn = GlobalSettings.TextureOverlay;
            PortableColorPicker_TextureOverlay_LineColor.Color.Set(GlobalSettings.DynamicOverlayLineColor);
            NumberBox_TextureOverlay_LineSize.Value = GlobalSettings.DynamicOverlayLineSize;
            PortableColorPicker_PixelGrid_Color.Color.Set(GlobalSettings.DynamicOverlayGridColor);

            ToggleSwitch_PixelGrid_Enabled.IsOn = GlobalSettings.GridEnabled;
            PortableColorPicker_TextureOverlay_TextColor.Color.Set(GlobalSettings.DynamicOverlayTextColor);
            NumberBox_TextureOverlay_TextSize.Value = GlobalSettings.DynamicOverlayTextSize;

            ToggleSwitch_RememberPrefers_Enabled.IsOn = GlobalSettings.RememberPrefers;

            isLoading = false;


        }

        #endregion


        #region UI Events
        private void ComboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoading && ComboBox_Language.SelectedItem != null && ComboBox_Language.SelectedItem != Program.CurrentLanguage && ComboBox_Language.SelectedItem is Language)
            {
                Program.CurrentLanguage = ComboBox_Language.SelectedItem as Language;
                GlobalSettings.LanguageFile = System.IO.Path.GetFileName(Program.CurrentLanguage.FileName);

                TextBlock_Language_Author_Value.Text = LanguageLoader.Current.Author;
                TextBlock_Language_Version_Value.Text = LanguageLoader.Current.LangVersion;
            }

        }

        private void ComboBox_ThemeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.RequestedTheme = (Inkore.UI.WPF.Modern.ElementTheme)ComboBox_ThemeMode.SelectedIndex;
                    Program.RefreshApperanceSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }
        }

        private void ComboBox_Backdrop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.BackdropType = (Inkore.UI.WPF.Modern.Controls.Primitives.BackdropType)ComboBox_Backdrop.SelectedIndex;
                    Program.RefreshApperanceSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void Slider_UIScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.UIScale = Slider_UIScale.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void Slider_RenderScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.RenderScale = Slider_RenderScale.Value;
                    Program.Form_Editor.Renderer.RenderScale = GlobalSettings.RenderScale;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void ToggleSwitch_TextureOverlay_Enabled_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.TextureOverlay = ToggleSwitch_TextureOverlay_Enabled.IsOn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void PortableColorPicker_TextureOverlay_LineColor_ColorChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.DynamicOverlayLineColor = PortableColorPicker_TextureOverlay_LineColor.Color.N2D();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void NumberBox_TextureOverlay_LineSize_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.DynamicOverlayLineSize = (int)NumberBox_TextureOverlay_LineSize.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void PortableColorPicker_TextureOverlay_TextColor_ColorChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.DynamicOverlayTextColor = PortableColorPicker_TextureOverlay_TextColor.Color.N2D();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void NumberBox_TextureOverlay_TextSize_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.DynamicOverlayTextSize = (int)NumberBox_TextureOverlay_TextSize.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void ToggleSwitch_PixelGrid_Enabled_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.GridEnabled = ToggleSwitch_PixelGrid_Enabled.IsOn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void PortableColorPicker_PixelGrid_Color_ColorChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.DynamicOverlayGridColor = PortableColorPicker_PixelGrid_Color.Color.N2D();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }

        private void ToggleSwitch_RememberPrefers_Enabled_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoading)
                {
                    GlobalSettings.RememberPrefers = ToggleSwitch_RememberPrefers_Enabled.IsOn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Program.Log(ex);
            }

        }
        private void Button_UIScale_Apply_Click(object sender, RoutedEventArgs e)
        {
            Program.RefreshApperanceSettings();
        }

        private void Button_OpenKeyEditor_Click(object sender, RoutedEventArgs e)
        {
            Editor._shortcutEditor.ShowDialog();
        }

        #endregion

    }
}
