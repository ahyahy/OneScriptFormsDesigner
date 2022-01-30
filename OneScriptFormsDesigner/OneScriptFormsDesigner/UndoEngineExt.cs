using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System;

namespace osfDesigner
{
    public class UndoEngineExt : UndoEngine
    {
        private string _Name_ = "UndoEngineExt";
        private Stack<UndoEngine.UndoUnit> undoStack = new Stack<UndoEngine.UndoUnit>();
        private Stack<UndoEngine.UndoUnit> redoStack = new Stack<UndoEngine.UndoUnit>();

        public UndoEngineExt (IServiceProvider provider) : base (provider)
        {
        }

        public bool EnableUndo
        {
            get { return undoStack.Count > 0; }
        }

        public bool EnableRedo
        {
            get { return redoStack.Count > 0; }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                try
                {
                    UndoEngine.UndoUnit unit = undoStack.Pop();
                    unit.Undo();
                    redoStack.Push (unit);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(_Name_ + ex.Message);
                }
            }
            else { }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                try
                {
                    UndoEngine.UndoUnit unit = redoStack.Pop();
                    unit.Undo();
                    undoStack.Push (unit);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(_Name_ + ex.Message);
                }
            }
            else { }
        }

        protected override void AddUndoUnit (UndoEngine.UndoUnit unit)
        {
            // Тут надо бы что нибудь понадежней придумать! и учесть языковые настройки.
            if (!unit.Name.Contains("Form"))
            {
                undoStack.Push(unit);
            }
        }
    }
}
