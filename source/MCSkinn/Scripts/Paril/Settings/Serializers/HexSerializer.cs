//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Globalization;

namespace MCSkinn.Scripts.Paril.Settings.Serializers
{
    public static class HexSerializer
    {
        public static string GetHex(string s)
        {
            string h = "";

            foreach (char c in s)
                h += string.Format("{0:X2}", (int)c);

            return h;
        }

        public static string GetString(string hex)
        {
            string s = "";

            for (int i = 0; i < hex.Length; i += 2)
                s += (char)int.Parse("" + hex[i] + hex[i + 1], NumberStyles.HexNumber);

            return s;
        }
    }
}