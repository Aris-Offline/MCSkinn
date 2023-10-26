//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System;
using System.Windows.Forms;

namespace MCSkinn.Scripts.Paril.Components.Shortcuts
{
    public interface IShortcutImplementor
    {
        string Name { get; set; }
        string SaveName { get; }
        Keys Keys { get; set; }
        Action Pressed { get; set; }

        bool CanEvaluate();
    }
}