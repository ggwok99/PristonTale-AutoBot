using AutoClicker.Interfaces;
using System.Windows.Forms;

namespace AutoClicker.Controls
{
    public class LabelObserver : Label, IObserver
    {
        private UControlContext _subject;
        private ContextState _observerState;

        public LabelObserver(UControlContext subject)
        {
            _subject = subject;
        }

        public LabelObserver() { }

        public void SetSubject(UControlContext subject)
        {
            _subject = subject;
        }

        public void SubjectUpdated()
        {
            if (_subject == null)
            {
                return;
            }

            _observerState = _subject.State;
            Text = $"Status: {_observerState.ToString()}";
        }
    }
}
