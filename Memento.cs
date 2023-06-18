using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Memento
    {
        public string Content { get; set; }
    }
    public interface IOriginator
    {
        object GetMemento();
        void SetMemento(object memento);
    }
    public class Caretaker
    {
        private object memento;
        public void SaveState(IOriginator originator)
        {
            memento = originator.GetMemento();
        }

        public void RestoreState(IOriginator originator)
        {
            originator.SetMemento(memento);
        }
    }
}