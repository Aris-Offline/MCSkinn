using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF = System.Windows;
using Drawing = System.Drawing;
using Modern = Inkore.UI.WPF.Modern;

namespace MCSkinn.Scripts
{
    public static class TypeHelper
    {
        public static void Set(this Inkore.UI.WPF.ColorPicker.Models.NotifyableColor c , WPF.Media.Color v)
        {
            c.RGB_R = v.R;
            c.RGB_G = v.G;
            c.RGB_B = v.B;
            c.A = v.A;
        }
        public static void Set(this Inkore.UI.WPF.ColorPicker.Models.NotifyableColor c, Drawing.Color v)
        {
            c.RGB_R = v.R;
            c.RGB_G = v.G;
            c.RGB_B = v.B;
            c.A = v.A;
        }

        public static Modern.ApplicationTheme? ToApplicationTheme(this Modern.ElementTheme theme)
        {
            switch (theme)
            {
                case Modern.ElementTheme.Light:
                    return Modern.ApplicationTheme.Light;
                case Modern.ElementTheme.Dark:
                    return Modern.ApplicationTheme.Dark;
                case Modern.ElementTheme.Default:
                    return null;
                default:
                    return null;
            }
        }
        public static Modern.ElementTheme ToElementTheme(this Modern.ApplicationTheme? theme)
        {
            if (theme == null)
                return Modern.ElementTheme.Default;
            else
                return theme.Value.ToElementTheme();
        }
        public static Modern.ElementTheme ToElementTheme(this Modern.ApplicationTheme theme)
        {
            switch (theme)
            {
                case Modern.ApplicationTheme.Light:
                    return Modern.ElementTheme.Light;
                case Modern.ApplicationTheme.Dark:
                    return Modern.ElementTheme.Dark;
                default:
                    return Modern.ElementTheme.Default;
            }
        }

    }
}
