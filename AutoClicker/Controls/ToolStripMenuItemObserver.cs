using AutoClicker.Interfaces;
using System.Windows.Forms;

namespace AutoClicker.Controls
{
    public class ToolStripMenuItemConcreteObserver : ToolStripMenuItem, IObserver
    {
        private UControlContext _subject;
        private ContextState _observerState;

        public ToolStripMenuItemConcreteObserver(UControlContext subject)
        {
            _subject = subject;
        }

        public void SubjectUpdated()
        {
            _observerState = _subject.State;
            Checked = _observerState is ContextRunningState;
            CheckState = _observerState is ContextRunningState ? CheckState.Checked : CheckState.Unchecked;
        }
    }
}
