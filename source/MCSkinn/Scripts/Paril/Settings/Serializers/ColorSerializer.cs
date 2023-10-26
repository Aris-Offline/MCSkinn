//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using iNKORE.Coreworks.Windows.Helpers;
using System.Drawing;

namespace MCSkinn.Scripts.Paril.Settings.Serializers
{
    // I honestly thought that I wouldn't need to do this, but since structs
    // can't be readonly..
    public class ColorSerializer : ITypeSerializer
    {
        #region ITypeSerializer Members

        public string Serialize(object obj)
        {
            if (obj == null)
                return "null";

            var c = (Color)obj;

            return c.ColorToHex();
        }

        public object Deserialize(string str)
        {
            try
            {
                return iNKORE.Coreworks.Windows.Helpers.TypeHelper.HexToDrawingColor(str);
            }
            catch
            {
                if (str == "null") return null;

                string[] split = str.Split();

                return Color.FromArgb(byte.Parse(split[3]), byte.Parse(split[0]), byte.Parse(split[1]), byte.Parse(split[2]));
            }
        }

        #endregion
    }
}