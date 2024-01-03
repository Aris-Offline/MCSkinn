//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Collections.Generic;

namespace MCSkinn.Scripts.Paril.Components.Undo
{
    public class UndoBuffer
    {
        private readonly List<IUndoable> Undos = new List<IUndoable>();
        public object Object;
        public int _depth = -1;

        public UndoBuffer(object obj)
        {
            Object = obj;
        }

        public IEnumerable<IUndoable> UndoList
        {
            get
            {
                if (_depth == -1)
                {
                    foreach (IUndoable x in Undos)
                        yield return x;
                }
                else
                {
                    for (int i = 0; i < CurrentIndex; ++i)
                        yield return Undos[i];
                }
            }
        }

        public IEnumerable<IUndoable> RedoList
        {
            get
            {
                for (int i = Undos.Count - 1; i >= CurrentIndex; --i)
                    yield return Undos[i];
            }
        }

        public int CurrentIndex
        {
            get
            {
                if (_depth == -1)
                    return Undos.Count;
                return _depth;
            }

            set
            {
                _depth = value;

                if (_depth == Undos.Count)
                    _depth = -1;
            }
        }

        public bool CanUndo
        {
            get { return CurrentIndex != 0; }
        }

        public bool CanRedo
        {
            get { return Undos.Count > CurrentIndex; }
        }

        public void AddBuffer(IUndoable undoable)
        {
            if (CurrentIndex == Undos.Count)
                Undos.Add(undoable);
            else
            {
                Undos.RemoveRange(CurrentIndex, Undos.Count - CurrentIndex);
                Undos.Add(undoable);
                CurrentIndex = Undos.Count;
            }
        }

        public void Undo()
        {
            try
            {
                Undos[CurrentIndex - 1].Undo(Object);
                CurrentIndex--;
            }
            catch(Exception ex)
            {
                Program.Log(ex, "UndoBuffer.Undo()");
            }
        }

        public void Redo()
        {
            try
            {
                Undos[CurrentIndex].Redo(Object);
                CurrentIndex++;
            }
            catch (Exception ex)
            {
                Program.Log(ex, "UndoBuffer.Undo()");
            }

        }

        public void Clear()
        {
            Undos.Clear();
            _depth = -1;
        }
    }
}