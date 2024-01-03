using iNKORE.UI.WPF.Modern.Common.Converters;
using iNKORE.UI.WPF.Modern.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MCSkinn.Scripts
{
    public static class StaticHolder
    {
        public static ColorToBrushConverter Converter_ColorToSolidColorBrushConverter = new ColorToBrushConverter();
        public static BooleanToVisibilityConverter Converter_BooleanToVisibilityConverter = new BooleanToVisibilityConverter();

        public static ColorConverter Converter_ColorConverter = new ColorConverter();

        public static System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
    }
}
