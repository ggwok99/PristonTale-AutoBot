using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker.Strategies
{
    public abstract class RefillPotStrategy : AutoStrategy
    {
        protected PotPosition PotPosition { get; set; }
        protected Keys KeyHP { get; set; }
        protected Keys KeyMP { get; set; }
        protected Keys KeySTM { get; set; }
        protected int TimeOut { get; set; }

        public override void LoadData(dynamic data)
        {
            if (data == null)
            {
                return;
            }

            if (data.PotPosition.HP is Point)
            {
                PotPosition = new PotPosition(data.PotPosition.HP, data.PotPosition.MP, data.PotPosition.STM);
            }
            else
            {
                string[] coordsHP = data.PotPosition.HP.Value.ToString().Split(',');
                Point hp = new Point(int.Parse(coordsHP[0]), int.Parse(coordsHP[1]));
                string[] coordsMP = data.PotPosition.MP.Value.ToString().Split(',');
                Point mp = new Point(int.Parse(coordsMP[0]), int.Parse(coordsMP[1]));
                string[] coordsSTM = data.PotPosition.STM.Value.ToString().Split(',');
                Point stm = new Point(int.Parse(coordsSTM[0]), int.Parse(coordsSTM[1]));
                PotPosition = new PotPosition(hp, mp, stm);
            }

            TimeOut = (int)data.TimeOut * 60;
            KeyHP = (Keys)data.KeyHP;
            KeyMP = (Keys)data.KeyMP;
            KeySTM = (Keys)data.KeySTM;
        }

        protected override double TimeOutInSecond()
        {
            return TimeOut;
        }

        public override MainStreamState ThisState()
        {
            return MainStreamState.RefillPot;
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

    public class ActiveRefillPotStrategy : RefillPotStrategy
    {
        public ActiveRefillPotStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            FireRequestMainStream();
            
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey);

            // Close all open tabs
            AutoClickHandlers.SendKeyPress(_hWnd, Keys.Space, 300);
            AutoClickHandlers.SendKeyPress(_hWnd, Keys.Space, 100);

            // Open inventory
            AutoClickHandlers.SendKeyPress(_hWnd, Keys.V, 300);

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);

            // HP
            AutoClickHandlers.SendMouseToPoint(_hWnd, PotPosition.HP, 200);
            AutoClickHandlers.SendKeyPress(_hWnd, KeyHP, 200);

            // MP
            AutoClickHandlers.SendMouseToPoint(_hWnd, PotPosition.MP, 200);
            AutoClickHandlers.SendKeyPress(_hWnd, KeyMP, 200);

            // STM
            AutoClickHandlers.SendMouseToPoint(_hWnd, PotPosition.STM, 200);
            AutoClickHandlers.SendKeyPress(_hWnd, KeySTM, 200);

            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey);

            // Close inventory
            AutoClickHandlers.SendKeyPress(_hWnd, Keys.Space, 300);
            AutoClickHandlers.SendKeyPress(_hWnd, Keys.Space, 100);

            FireReleaseMainStream();
        }
    }

    public class DeactiveRefillPotStrategy : RefillPotStrategy
    {
        public DeactiveRefillPotStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            Stop();
        }
    }
}
