using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker.Strategies
{
    public abstract class BuffStrategy : AutoStrategy
    {
        public List<BuffItem> Buffs { get; set; }

        public override void LoadData(dynamic data)
        {
            Buffs = data.Buffs;
        }

        protected override double TimeOutInSecond()
        {
            return 1;
        }

        public override MainStreamState ThisState()
        {
            return MainStreamState.Buff;
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

    public class ActiveBuffStrategy : BuffStrategy
    {
        public ActiveBuffStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            foreach (BuffItem buff in Buffs)
            {
                FireRequestMainStream();

                Thread.Sleep(100);
                if (!_mainStream)
                {
                    return;
                }

                if (buff.DecrementTrueIfCounter0())
                {
                    DateTime timeToStop = DateTime.Now.AddSeconds(3);

                    AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey);

                    do
                    {
                        // Set skill hotkey.
                        AutoClickHandlers.SendKeyPress(_hWnd, buff.Key);
                        AutoClickHandlers.SendMouseToPoint(_hWnd, new Point(0, 0));

                        // Prevent stuck key.
                        AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey);

                        // Use skills.
                        AutoClickHandlers.RightClick(_hWnd, 500);
                    } while (timeToStop > DateTime.Now);
                }

                FireReleaseMainStream();
            }
        }
    }

    public class DeactiveBuffStrategy : BuffStrategy
    {
        public DeactiveBuffStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            Stop();
        }
    }
}
