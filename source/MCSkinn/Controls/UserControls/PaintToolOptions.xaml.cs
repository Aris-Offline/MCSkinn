using Inkore.Coreworks.Helpers;
using Inkore.Coreworks.Localization;
using MCSkinn.Scripts;
using MCSkinn.Scripts.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
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

namespace MCSkinn.Controls.UserControls
{
    /// <summary>
    /// PaintToolOptions.xaml 的交互逻辑
    /// </summary>
    public partial class PaintToolOptions : UserControl
    {
        public PaintToolOptions()
        {
            InitializeComponent();
        }
        public void RegisterLanguage()
        {
            LanguageWpf.Register(TextBlock_BrushSize, TextBlock.TextProperty);
            LanguageWpf.Register(CheckBox_Incremental, ContentProperty);
            LanguageWpf.Register(CheckBox_CloseAfterSelect, ContentProperty);
        }

        CollectionViewSource CollectionViewSource_Brushes;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (Program.Form_Editor != null)
            {
                Program.Form_Editor.SelectedToolChanged += Editor_SelectedToolChanged;
            }

            RegisterLanguage();
            Editor_SelectedToolChanged(this, e);

            LanguageLoader.CurrentChanged += LanguageLoader_CurrentChanged;

            CollectionViewSource_Brushes = new CollectionViewSource();
            CollectionViewSource_Brushes.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            CollectionViewSource_Brushes.Source = MCSkinn.Forms.Controls.Brushes.BrushList;

            GridView_Brushes.SetBinding(Inkore.UI.WPF.Modern.Controls.GridView.ItemsSourceProperty, new Binding() { Source = CollectionViewSource_Brushes });

            GridView_Brushes.SelectedIndex = 0;
        }

        private void LanguageLoader_CurrentChanged(object? sender, EventArgs e)
        {
            Editor_SelectedToolChanged(this, e);
        }

        bool isLoading = false;
        private void Editor_SelectedToolChanged(object? sender, EventArgs e)
        {
            bool _brushSize = false;
            bool _exposure = false;
            bool _twoRadioSelection = false;
            bool _incremental = false;
            bool _tooltip = false;
            string _exposureText = "O_EXPOSURE";
            string _radioOption1Text = null;
            string _radioOption2Text = null;


            isLoading = true;

            if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Camera)
            {
                _brushSize = false;
                _exposure = false;
                _twoRadioSelection = false;
                _incremental = false;
                _tooltip = true;
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Pencil)
            {
                _brushSize = true;
                _exposure = false;
                _twoRadioSelection = false;
                _incremental = true;
                _tooltip = false;

                CheckBox_Incremental.IsChecked = GlobalSettings.Tool_Pencil_Incremental;
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Eraser)
            {
                _brushSize = true;
                _exposure = false;
                _twoRadioSelection = false;
                _incremental = false;
                _tooltip = false;
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Dropper)
            {
                _brushSize = false;
                _exposure = false;
                _twoRadioSelection = false;
                _incremental = false;
                _tooltip = true;
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Dodge)
            {
                _brushSize = true;
                _exposure = true;
                _twoRadioSelection = true;
                _radioOption1Text = "O_DODGE";
                _radioOption2Text = "O_BURN";
                _incremental = true;
                _tooltip = false;

                CheckBox_Incremental.IsChecked = GlobalSettings.Tool_DodgeBurn_Incremental;
                SetExposureSlider(GlobalSettings.Tool_DodgeBurn_Exposure);
                RadioButton_Option1.IsChecked = (Program.Form_Editor.Tool_Dodge.Tool as DodgeBurnTool).IsInverted;
                RadioButton_Option0.IsChecked = !RadioButton_Option1.IsChecked.GetValueOrDefault(false);
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Darken)
            {
                _brushSize = true;
                _exposure = true;
                _twoRadioSelection = true;
                _radioOption1Text = "O_DARKEN";
                _radioOption2Text = "O_LIGHTEN";
                _incremental = true;
                _tooltip = false;

                CheckBox_Incremental.IsChecked = GlobalSettings.Tool_DarkenLighten_Incremental;
                SetExposureSlider(GlobalSettings.Tool_DarkenLighten_Exposure);
                RadioButton_Option1.IsChecked = (Program.Form_Editor.Tool_Darken.Tool as DarkenLightenTool).IsInverted;
                RadioButton_Option0.IsChecked = !RadioButton_Option1.IsChecked.GetValueOrDefault(false);
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Fill)
            {
                _brushSize = false;
                _exposure = true;
                _exposureText = "Q_THRESHOLD";
                _twoRadioSelection = false;
                _incremental = false;
                _tooltip = false;

                SetExposureSlider(GlobalSettings.Tool_FloodFill_Threshold);

            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Noise)
            {
                _brushSize = true;
                _exposure = true;
                _exposureText = "O_HOLDNESS";
                _twoRadioSelection = false;
                _incremental = false;
                _tooltip = false;

                SetExposureSlider(GlobalSettings.Tool_Noise_Saturation);
            }
            else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Stamp)
            {
                _brushSize = true;
                _exposure = false;
                _twoRadioSelection = false;
                _incremental = true;
                _tooltip = false;

                CheckBox_Incremental.IsChecked = GlobalSettings.Tool_Pencil_Incremental;
            }

            RowDefinition_Option0.Height = GetGridLength(_brushSize);
            RowDefinition_Option1.Height = GetGridLength(_exposure);
            RowDefinition_Option3.Height = GetGridLength(_tooltip);

            TextBlock_Exposure.Text = LanguageLoader.GET(_exposureText);
            RadioButton_Option0.Content = LanguageLoader.GET(_radioOption1Text); ;
            RadioButton_Option1.Content = LanguageLoader.GET(_radioOption2Text); ;

            TextBlock_Exposure.Text = LanguageLoader.GET(_exposureText);
            RadioButton_Option0.Visibility = RadioButton_Option1.Visibility = _twoRadioSelection ? Visibility.Visible : Visibility.Collapsed;
            CheckBox_Incremental.Visibility = _incremental ? Visibility.Visible : Visibility.Collapsed;

            TextBlock_Tooltip.Text = Program.Form_Editor.SelectedTool.Tool.GetStatusLabelText();

            isLoading = false;
        }

        private GridLength GetGridLength(bool isEnabled)
        {
            return isEnabled ? GridLength.Auto : new GridLength(0);
        }

        private void CheckBox_Incremental_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoading)
            {
                if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Pencil)
                {
                    GlobalSettings.Tool_Pencil_Incremental = CheckBox_Incremental.IsChecked.GetValueOrDefault(false);
                }
                else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Dodge)
                {
                    GlobalSettings.Tool_DodgeBurn_Incremental = CheckBox_Incremental.IsChecked.GetValueOrDefault(false);
                }
                else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Darken)
                {
                    GlobalSettings.Tool_DarkenLighten_Incremental = CheckBox_Incremental.IsChecked.GetValueOrDefault(false);
                }
                else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Fill)
                {
                    GlobalSettings.Tool_Pencil_Incremental = CheckBox_Incremental.IsChecked.GetValueOrDefault(false);
                }
                else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Stamp)
                {
                    GlobalSettings.Tool_Pencil_Incremental = CheckBox_Incremental.IsChecked.GetValueOrDefault(false);
                }
            }

        }

        private void SetExposureSlider(float value)
        {
            TextBox_Exposure.Text = (value * 100).ToInt32().ToString();

            if (value * 100 > Slider_Exposure.Maximum)
                Slider_Exposure.Value = Slider_Exposure.Maximum;
            else
                Slider_Exposure.Value = value * 100;
        }

        private void RadioButton_Option0_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoading && Program.Form_Editor != null)
            {
                if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Dodge)
                {
                    (Program.Form_Editor.Tool_Dodge.Tool as DodgeBurnTool).IsInverted = RadioButton_Option1.IsChecked.GetValueOrDefault(false);
                }
                else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Darken)
                {
                    (Program.Form_Editor.Tool_Darken.Tool as DarkenLightenTool).IsInverted = RadioButton_Option1.IsChecked.GetValueOrDefault(false);
                }
            }

        }

        private void Slider_Exposure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isLoading)
            {
                if(TextBox_Exposure.Text.ToDouble(0) != Slider_Exposure.Value)
                {
                    if(TextBox_Exposure.Text.ToDouble(0) > Slider_Exposure.Maximum && Slider_Exposure.Value == Slider_Exposure.Minimum)
                    {
                        return;
                    }
                    else
                    {
                        TextBox_Exposure.Text = Slider_Exposure.Value.ToInt32().ToString();
                        TextBox_Exposure_LostFocus(sender, e);
                    }
                }
            }
        }

        private void TextBox_Exposure_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!isLoading)
            {
                float value = (float)TextBox_Exposure.Text.ToInt32(-1) / 100;
                if(value >= 0)
                {
                    if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Dodge)
                    {
                        GlobalSettings.Tool_DodgeBurn_Exposure = value;
                    }
                    else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Darken)
                    {
                        GlobalSettings.Tool_DarkenLighten_Exposure = value;
                    }
                    else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Fill)
                    {
                        GlobalSettings.Tool_FloodFill_Threshold = value;
                    }
                    else if (Program.Form_Editor.SelectedTool == Program.Form_Editor.Tool_Noise)
                    {
                        GlobalSettings.Tool_Noise_Saturation = value;
                    }

                    if (value * 100 > Slider_Exposure.Maximum)
                        Slider_Exposure.Value = Slider_Exposure.Maximum;
                    else
                        Slider_Exposure.Value = value * 100;
                }
                else
                {
                    TextBox_Exposure.Text = Convert.ToInt32(Slider_Exposure.Value.PureValue(0d)).ToString();
                    TextBox_Exposure_LostFocus(sender, e);
                }
                
            }

        }

        private void TextBox_Exposure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TextBox_Exposure.IsFocused)
                    Slider_Exposure.Focus();
                else
                    TextBox_Exposure_LostFocus(sender, e);

            }
        }

        private void GridView_Brushes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bruh = GridView_Brushes.SelectedValue as MCSkinn.Forms.Controls.Brush;

            if(bruh != null)
            {
                MCSkinn.Forms.Controls.Brushes.SelectedBrush = bruh;
                if (Editor.MainForm.SelectedTool != null)
                    Editor.MainForm.SelectedTool.Tool.SelectedBrushChanged();

                TextBlock_BrushName.Text = bruh.Name;
                Image_BrushPresenter.Source = bruh.ImageSource;

                if (CheckBox_CloseAfterSelect.IsChecked == true && DropDownButton_Brush.Flyout.IsOpen)
                    DropDownButton_Brush.Flyout.Hide();

            }
        }

        private void Border_BrushItemRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_BrushNamePreview.Text = ((sender as FrameworkElement)?.DataContext as MCSkinn.Forms.Controls.Brush)?.Name;
        }

        private void Border_BrushItemRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            if (TextBlock_BrushNamePreview.Text == ((sender as FrameworkElement)?.DataContext as MCSkinn.Forms.Controls.Brush)?.Name)
            {
                TextBlock_BrushNamePreview.Text = "";
            }
        }
    }
}
