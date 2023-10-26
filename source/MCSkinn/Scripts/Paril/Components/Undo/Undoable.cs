//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

namespace MCSkinn.Scripts.Paril.Components.Undo
{
    public interface IUndoable
    {
        string Action { get; }

        void Undo(object obj);
        void Redo(object obj);
    }
}