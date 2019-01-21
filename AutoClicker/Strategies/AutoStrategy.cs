using AutoClicker.Enums;
using AutoClicker.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AutoClicker.Strategies
{
    public abstract class AutoStrategy : IObserver
    {
        protected IntPtr _hWnd;
        protected CancellationTokenSource _cts;
        protected ITargetBlock<DateTimeOffset> _task;

        public void Start()
        {
            // Create the token source.
            _cts = new CancellationTokenSource();

            try
            {
                // Set the task.
                _task = CreateNeverEndingTask(now => DoWork(), _cts.Token);

                // Start the task.  Post the time.
                _task.Post(DateTimeOffset.Now);
            }
            // Task canceled.  Will not report as bug.
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Stop()
        {
            // CancellationTokenSource implements IDisposable.
            using (_cts)
            {
                // Cancel.  This will cancel the task.
                if (_cts != null)
                {
                    _cts.Cancel();
                }
            }

            // Set everything to null, since the references
            // are on the class level and keeping them around
            // is holding onto invalid state.
            _cts = null;
            _task = null;
        }

        protected void Initial(IntPtr intPtr)
        {
            _hWnd = intPtr;
        }

        private ITargetBlock<DateTimeOffset> CreateNeverEndingTask(Action<DateTimeOffset> action, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // Declare the block variable, it needs to be captured.
            ActionBlock<DateTimeOffset> block = null;

            // Create the block, it will call itself, so
            // you need to separate the declaration and
            // the assignment.
            // Async so you can wait easily when the
            // delay comes.
            block = new ActionBlock<DateTimeOffset>(async now =>
            {
                // Perform the action.
                action(now);

                // Wait.
                await Task.Delay(TimeSpan.FromSeconds(TimeOutInSecond()), cancellationToken).
                    // Doing this here because synchronization context more than
                    // likely *doesn't* need to be captured for the continuation
                    // here.  As a matter of fact, that would be downright
                    // dangerous.
                    ConfigureAwait(false);

                // Post the action back to the block.
                block.Post(DateTimeOffset.Now);
            }, new ExecutionDataflowBlockOptions
            {
                CancellationToken = cancellationToken
            });

            // Return the block.
            return block;
        }

        public abstract void LoadData(dynamic data);
        protected abstract double TimeOutInSecond();
        protected abstract void DoWork();

        // Get mainstream to handle auto if needed
        public abstract event EventHandler RequestMainStream;
        public abstract event EventHandler ReleaseMainStream;

        protected abstract void FireRequestMainStream();
        protected abstract void FireReleaseMainStream();

        protected ContextState _subject;
        public void SetSubject(ContextState subject)
        {
            _subject = subject;
        }

        protected bool _mainStream;
        protected MainStreamState _currentMainStreamState;
        public abstract MainStreamState ThisState();

        public void SubjectUpdated()
        {
            _currentMainStreamState = _subject.MainStreamState;
            if (_currentMainStreamState == ThisState())
            {
                _mainStream = true;
            }
            else
            {
                _mainStream = false;
            }
        }

        public void InitIntPtr(IntPtr intPtr)
        {
            _hWnd = intPtr;
        }
    }
}
