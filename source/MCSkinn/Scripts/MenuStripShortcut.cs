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

namespace MCSkinn.Scripts
{
    public class MenuStripShortcut : IShortcutImplementor
    {
        private readonly ToolStripMenuItem _menuItem;

        private readonly string _saveName;

        private Keys _keys;

        public MenuStripShortcut(ToolStripMenuItem item) :
            this(item, item.ShortcutKeys)
        {
            _menuItem.ShortcutKeys = 0;
        }

        public MenuStripShortcut(ToolStripMenuItem item, Keys keys)
        {
            _menuItem = item;
            Name = _saveName = _menuItem.Text.Replace("&", "");
            Keys = keys;
        }

        #region IShortcutImplementor Members

        public string Name { get; set; }

        public string SaveName
        {
            get { return _saveName; }
        }

        public Keys Keys
        {
            get { return _keys; }
            set
            {
                _keys = value;
                _menuItem.ShortcutKeyDisplayString = ShortcutEditor.KeysToString(_keys);
            }
        }

        public Action Pressed { get; set; }

        public bool CanEvaluate()
        {
            return true;
        }

        #endregion

        public override string ToString()
        {
            return Name + " [" + ShortcutEditor.KeysToString(Keys) + "]";
        }
    }
}