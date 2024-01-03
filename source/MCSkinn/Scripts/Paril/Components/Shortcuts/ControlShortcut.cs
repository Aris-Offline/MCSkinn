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
    /// <summary>
    /// Defines a shortcut that requires its control to be focused in order to work.
    /// </summary>
    public class ControlShortcut : IShortcutImplementor
    {
        public ControlShortcut(string name, Keys keys, Control owner)
        {
            Control = owner;
            Name = SaveName = name;
            Keys = keys;
        }

        public Control Control { get; private set; }

        #region IShortcutImplementor Members

        public string Name { get; set; }
        public string SaveName { get; private set; }
        public Keys Keys { get; set; }
        public Action Pressed { get; set; }

        public bool CanEvaluate()
        {
            return Control.Focused;
        }

        #endregion

        public override string ToString()
        {
            return Name + " [" + ShortcutEditor.KeysToString(Keys) + "]";
        }
    }
}