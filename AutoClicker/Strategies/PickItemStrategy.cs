using AutoClicker.Enums;
using AutoClicker.Helpers;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker.Strategies
{
    public abstract class PickItemStrategy : AutoStrategy
    {
        protected int TargetArea { get; set; }

        public override void LoadData(dynamic data)
        {
            TargetArea = data.TargetArea;
        }

        protected override double TimeOutInSecond()
        {
            return 20;
        }

        public override MainStreamState ThisState()
        {
            return MainStreamState.PickItem;
        }


        public override event EventHandler RequestMainStream;
        public override event EventHandler ReleaseMainStream;

        protected override void FireRequestMainStream()
        {
            RequestMainStream?.Invoke(this, null);
        }
        protected override void FireReleaseMainStream()
        {
            ReleaseMainStream?.Invoke(this, null);
        }
    }

    public class ActivePickItemStrategy : PickItemStrategy
    {
        public ActivePickItemStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            FireRequestMainStream();
            Thread.Sleep(100);

            if (!_mainStream)
            {
                return;
            }

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.A);
            for (var i = 0; i < 15; i++)
            {
                Point point = AutoClickHandlers.GetRandomPointFromCenter(TargetArea);
                AutoClickHandlers.LeftClickOnPoint(_hWnd, point, 500);
            }
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.A);

            FireReleaseMainStream();
        }
    }

    public class DeactivePickItemStrategy : PickItemStrategy
    {
        public DeactivePickItemStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.A);
            Stop();
        }
    }
}
