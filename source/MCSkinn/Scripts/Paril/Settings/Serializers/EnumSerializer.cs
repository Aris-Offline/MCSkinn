//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System;

namespace MCSkinn.Scripts.Paril.Settings.Serializers
{
    public class EnumSerializer<E> : ITypeSerializer
    {
        #region ITypeSerializer Members

        public string Serialize(object obj)
        {
            return ((int)obj).ToString();
        }

        public object Deserialize(string str)
        {
            return Enum.Parse(typeof(E), str);
        }

        #endregion
    }
}