//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Windows.Forms;
using MCSkinn.Scripts.Paril.Components.Shortcuts;

namespace MCSkinn.Scripts.Paril.Components.Shortcuts
{
    public class ShortcutBase : IShortcutImplementor
    {
        private readonly string _saveName;
        private string _name;

        public ShortcutBase(string name, Keys keys)
        {
            _saveName = _name = name;
            Keys = keys;
        }

        #region IShortcutImplementor Members

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string SaveName
        {
            get { return _saveName; }
        }

        public Keys Keys { get; set; }
        public Action Pressed { get; set; }

        public bool CanEvaluate()
        {
            return true;
        }

        #endregion

        public override string ToString()
        {
            return _name + " [" + ShortcutEditor.KeysToString(Keys) + "]";
        }
    }
}