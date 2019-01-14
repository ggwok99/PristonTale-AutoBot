using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClicker.Strategies
{
    public abstract class AttackingStrategy : AutoStrategy
    {
        protected int TargetArea { get; set; }
        protected AttackStyleType Style { get; set; }
        protected Keys MainSkillHotkey { get; set; }

        public override void LoadData(dynamic data)
        {
            TargetArea = data.TargetArea;
            Style = data.Style;
            MainSkillHotkey = data.MainSkillHotkey;
        }

        protected override double TimeOutInSecond()
        {
            return 1.5;
        }

        public override MainStreamState ThisState()
        {
            return MainStreamState.Attack;
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

    public class ActiveAttackingStrategy : AttackingStrategy
    {
        public ActiveAttackingStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            if (!_mainStream)
            {
                return;
            }

            Point point = AutoClickHandlers.GetRandomPointFromCenter(TargetArea);
            AutoClickHandlers.SendKeyPress(_hWnd, MainSkillHotkey);
            int i;
            switch (Style)
            {
                case AttackStyleType.Attack:
                    i = AutoClickHandlers.Random(0, 4);

                    if (i == 0)
                    {
                        AutoClickHandlers.RightClickOnPoint(_hWnd, point);
                        break;
                    }

                    AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
                    AutoClickHandlers.LeftClickOnPoint(_hWnd, point);
                    AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
                    break;
                case AttackStyleType.Spell:
                    i = AutoClickHandlers.Random(0, 2);
                    // Use skills less
                    if (i == 0)
                    {
                        AutoClickHandlers.SendMouseToPoint(_hWnd, point);
                        break;
                    }

                    AutoClickHandlers.RightClickOnPoint(_hWnd, point);
                    break;
                case AttackStyleType.Training:
                    AutoClickHandlers.SendKeyDown(_hWnd, Keys.ControlKey);
                    AutoClickHandlers.RightClickOnPoint(_hWnd, Master.Instance.ScreenResolution.Center);
                    AutoClickHandlers.SendKeyUp(_hWnd, Keys.ControlKey, 50);
                    break;
                default:
                    break;
            }
        }
    }

    public class DeactiveAttackingStrategy : AttackingStrategy
    {
        public DeactiveAttackingStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ControlKey, 50);
            Stop();
        }
    }
}
