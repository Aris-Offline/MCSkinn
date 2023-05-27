using MCSkinn.Controls;
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
    /// RenderPage.xaml 的交互逻辑
    /// </summary>
    public partial class RenderPage : Page
    {
        public RenderPage()
        {
            InitializeComponent();
        }
        public void RegisterLanguage()
        {
            LanguageWpf.Register(TextBlock_BgColor, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_3D, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_Multisampling, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_2D, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_Overlay, TextBlock.TextProperty);
            LanguageWpf.Register(CheckBox_OverlayEnabled, ContentProperty);
            LanguageWpf.Register(TextBlock_LineColor, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_TextColor, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_LineSize, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_TextSize, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_Grid, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_GridColor, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_GridOpacity, TextBlock.TextProperty);
            LanguageWpf.Register(CheckBox_GridEnabled, ContentProperty);
            LanguageWpf.Register(TextBlock_BgColor_RestartWarning, TextBlock.TextProperty);
            LanguageWpf.Register(TextBlock_Multisampling_RestartWarning, TextBlock.TextProperty);
            //LanguageWpf.Register(TextBlock_RestartWarning, TextBlock.TextProperty);

            //LanguageWpf.Register(Run_CurrentLang, Run.TextProperty);
            //LanguageWpf.Register(Run_NewLang, Run.TextProperty);

            //LanguageWpf.Register(AppBarButton_Redo, AppBarButton.LabelProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
            //LanguageWpf.Register(Button_LibraryToolbox_ZoomIn, ToolTipProperty);
        }
        public void UnegisterLanguage()
        {
            LanguageWpf.Unegister(TextBlock_BgColor);
            LanguageWpf.Unegister(TextBlock_3D);
            LanguageWpf.Unegister(TextBlock_Multisampling);
            LanguageWpf.Unegister(TextBlock_2D);
            LanguageWpf.Unegister(TextBlock_Overlay);
            LanguageWpf.Unegister(CheckBox_OverlayEnabled);
            LanguageWpf.Unegister(TextBlock_LineColor);
            LanguageWpf.Unegister(TextBlock_TextColor);
            LanguageWpf.Unegister(TextBlock_LineSize);
            LanguageWpf.Unegister(TextBlock_TextSize);
            LanguageWpf.Unegister(TextBlock_Grid);
            LanguageWpf.Unegister(TextBlock_GridColor);
            LanguageWpf.Unegister(TextBlock_GridOpacity);
            LanguageWpf.Unegister(CheckBox_GridEnabled);
            LanguageWpf.Unegister(TextBlock_BgColor_RestartWarning);
            LanguageWpf.Unegister(TextBlock_Multisampling_RestartWarning);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterLanguage();

            ColorPicker_BgColor.Color = GlobalSettings.BackgroundColor.D2W();
            NumberBox_Multisampling.Value = GlobalSettings.Multisamples;

            CheckBox_OverlayEnabled.IsChecked = GlobalSettings.TextureOverlay;
            ColorPicker_LineColor.Color = GlobalSettings.DynamicOverlayLineColor.D2W();
            ColorPicker_TextColor.Color = GlobalSettings.DynamicOverlayTextColor.D2W();
            NumberBox_LineSize.Value = GlobalSettings.DynamicOverlayLineSize;
            NumberBox_TextSize.Value = GlobalSettings.DynamicOverlayTextSize;

            CheckBox_GridEnabled.IsChecked = GlobalSettings.GridEnabled;
            ColorPicker_GridColor.Color = GlobalSettings.DynamicOverlayGridColor.D2W(255);
            NumberBox_GridOpacity.Value = GlobalSettings.DynamicOverlayGridColor.A;

        }
        private void ColorPicker_BgColor_ColorPicked(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.BackgroundColor == ColorPicker_BgColor.Color.W2D())
                return;

            GlobalSettings.BackgroundColor = ColorPicker_BgColor.Color.W2D();
            TextBlock_BgColor_RestartWarning.Visibility = Visibility.Visible;
            GlobalSettings.Save();
        }


        private void CheckBox_OverlayEnabled_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.TextureOverlay == CheckBox_OverlayEnabled.IsChecked)
                return;

            GlobalSettings.TextureOverlay = CheckBox_OverlayEnabled.IsChecked.GetValueOrDefault(true);
            GlobalSettings.Save();

        }


        private void ColorPicker_LineColor_ColorPicked(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.DynamicOverlayLineColor == ColorPicker_LineColor.Color.W2D())
                return;

            GlobalSettings.DynamicOverlayLineColor = ColorPicker_LineColor.Color.W2D();
            GlobalSettings.Save();

        }

        private void ColorPicker_TextColor_ColorPicked(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.DynamicOverlayTextColor == ColorPicker_TextColor.Color.W2D())
                return;
            GlobalSettings.DynamicOverlayTextColor = ColorPicker_TextColor.Color.W2D();
            GlobalSettings.Save();

        }

        private void NumberBox_LineSize_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (GlobalSettings.DynamicOverlayLineSize == (int)NumberBox_LineSize.Value)
                return;
            if (NumberBox_LineSize.Value < 1 || double.IsNaN(NumberBox_LineSize.Value) || double.IsInfinity(NumberBox_LineSize.Value))
                NumberBox_LineSize.Value = 1;

            GlobalSettings.DynamicOverlayLineSize = (int)NumberBox_LineSize.Value;
            GlobalSettings.Save();

        }

        private void NumberBox_TextSize_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (GlobalSettings.DynamicOverlayTextSize == (int)NumberBox_TextSize.Value)
                return;
            if (NumberBox_TextSize.Value < 1 || double.IsNaN(NumberBox_TextSize.Value) || double.IsInfinity(NumberBox_TextSize.Value))
                NumberBox_TextSize.Value = 1;

            GlobalSettings.DynamicOverlayTextSize = (int)NumberBox_TextSize.Value;
            GlobalSettings.Save();

        }

        private void CheckBox_GridEnabled_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.GridEnabled = CheckBox_GridEnabled.IsChecked.GetValueOrDefault(true))
                return;
            GlobalSettings.GridEnabled = CheckBox_GridEnabled.IsChecked.GetValueOrDefault(true);
            GlobalSettings.Save();
        }

        private void ColorPicker_GridColor_ColorPicked(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.DynamicOverlayGridColor == ColorPicker_GridColor.Color.W2D((byte)Math.Min(NumberBox_GridOpacity.Value, 255)))
                return;
            if (NumberBox_GridOpacity.Value < 0 || double.IsNaN(NumberBox_GridOpacity.Value) || double.IsInfinity(NumberBox_GridOpacity.Value))
                NumberBox_GridOpacity.Value = 0;

            GlobalSettings.DynamicOverlayGridColor = ColorPicker_GridColor.Color.W2D((byte)Math.Min(NumberBox_GridOpacity.Value, 255));
            GlobalSettings.Save();

        }

        private void NumberBox_GridOpacity_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (GlobalSettings.DynamicOverlayGridColor == ColorPicker_GridColor.Color.W2D((byte)Math.Min(NumberBox_GridOpacity.Value, 255)))
                return;
            if (NumberBox_GridOpacity.Value < 0 || double.IsNaN(NumberBox_GridOpacity.Value) || double.IsInfinity(NumberBox_GridOpacity.Value))
                NumberBox_GridOpacity.Value = 0;

            GlobalSettings.DynamicOverlayGridColor = ColorPicker_GridColor.Color.W2D((byte)Math.Min(NumberBox_GridOpacity.Value, 255));
            GlobalSettings.Save();
        }

        private void NumberBox_Multisampling_ValueChanged(Inkore.UI.WPF.Modern.Controls.NumberBox sender, Inkore.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (GlobalSettings.Multisamples == (int)NumberBox_Multisampling.Value)
                return;
            if (NumberBox_Multisampling.Value < 0 || double.IsNaN(NumberBox_Multisampling.Value) || double.IsInfinity(NumberBox_Multisampling.Value))
                NumberBox_Multisampling.Value = 0;

            GlobalSettings.Multisamples = (int)NumberBox_Multisampling.Value;
            TextBlock_Multisampling_RestartWarning.Visibility = Visibility.Visible;
            GlobalSettings.Save();

        }
    }
}
