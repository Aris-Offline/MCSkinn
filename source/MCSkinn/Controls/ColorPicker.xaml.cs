using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using iNKORE.Coreworks.Windows.Helpers;

namespace MCSkinn.Controls
{
    /// <summary>
    /// ColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPicker : UserControl, INotifyPropertyChanged
    {
        public static DependencyProperty ColorProperty=DependencyProperty.Register("Color",typeof(Color),typeof(ColorPicker),new PropertyMetadata(default(Color))); 
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static RoutedEvent ColorPickedEvent = EventManager.RegisterRoutedEvent("ColorPicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));

        public event PropertyChangedEventHandler? PropertyChanged;

        public event RoutedEventHandler ColorPicked
        {
            add { AddHandler(ColorPickedEvent, value); }
            remove { RemoveHandler(ColorPickedEvent, value); }
        }
        public ColorPicker()
        {
            InitializeComponent();
        }


        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog d = new System.Windows.Forms.ColorDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Color = d.Color.ToWpfColor();
                RaiseEvent(new RoutedEventArgs(ColorPickedEvent));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
            }
        }
    }
    internal class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
                return new SolidColorBrush((Color)value);
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
                return ((SolidColorBrush)value).Color;
            else
                return Colors.Transparent;
        }
    }
}
