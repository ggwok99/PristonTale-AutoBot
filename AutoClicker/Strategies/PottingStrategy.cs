using AutoClicker.Enums;
using AutoClicker.Helpers;
using System;
using System.Windows.Forms;

namespace AutoClicker.Strategies
{
    public abstract class PottingStrategy : AutoStrategy
    {
        protected int ValueHP { get; set; }
        protected Keys KeyHP { get; set; }

        protected int ValueMP { get; set; }
        protected Keys KeyMP { get; set; }

        protected int ValueSTM { get; set; }
        protected Keys KeySTM { get; set; }

        public override void LoadData(dynamic data)
        {
            ValueHP = data.HP.Value;
            KeyHP = data.HP.Key;

            ValueMP = data.MP.Value;
            KeyMP = data.MP.Key;

            ValueSTM = data.STM.Value;
            KeySTM = data.STM.Key;
        }

        protected override double TimeOutInSecond()
        {
            return .1;
        }

        public override MainStreamState ThisState()
        {
            return MainStreamState.Pot;
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

    public class ActivePottingStrategy : PottingStrategy
    {
        private float _maxHP;
        private float _maxMP;
        private float _maxSTM;

        private const int _hpMemAddress = 0x03324D50;
        private const int _mpMemAddress = 0x03324D54;
        private const int _stmMemAddress = 0x03324D58;

        public ActivePottingStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            User32.GetWindowThreadProcessId(_hWnd, out uint processId);
            IntPtr processHandle = Kernel32.OpenProcess(Kernel32.PROCESS_VM_READ | Kernel32.PROCESS_VM_WRITE | Kernel32.PROCESS_VM_OPERATION, false, (int)processId);

            byte[] hpBuffer = new byte[4];
            Kernel32.ReadProcessMemory((int)processHandle, _hpMemAddress, hpBuffer, hpBuffer.Length, out int bytesRead);
            float currentHP = BitConverter.ToSingle(hpBuffer, 0);
            if (currentHP > _maxHP)
            {
                _maxHP = currentHP;
            }

            if (_maxHP == 0)
            {
                _maxMP = 0;
                _maxSTM = 0;
                return;
            }

            decimal percentageHP = (decimal)currentHP / (decimal)_maxHP * 100;
            SendKeyPressIfNeeded(AttributeType.HP, percentageHP);

            byte[] mpBuffer = new byte[4];
            Kernel32.ReadProcessMemory((int)processHandle, _mpMemAddress, mpBuffer, mpBuffer.Length, out bytesRead);
            float currentMP = BitConverter.ToSingle(mpBuffer, 0);
            if (currentMP > _maxMP)
            {
                _maxMP = currentMP;
            }

            if (_maxMP == 0)
            {
                _maxHP = 0;
                _maxSTM = 0;
                return;
            }

            decimal percentageMP = (decimal)currentMP / (decimal)_maxMP * 100;
            SendKeyPressIfNeeded(AttributeType.MP, percentageMP);

            byte[] stmBuffer = new byte[4];
            Kernel32.ReadProcessMemory((int)processHandle, _stmMemAddress, stmBuffer, stmBuffer.Length, out bytesRead);
            float currentSTM = BitConverter.ToSingle(stmBuffer, 0);
            if (currentSTM > _maxSTM)
            {
                _maxSTM = currentSTM;
            }

            if (_maxSTM == 0)
            {
                _maxHP = 0;
                _maxMP = 0;
                return;
            }

            decimal percentageSTM = (decimal)currentSTM / (decimal)_maxSTM * 100;
            SendKeyPressIfNeeded(AttributeType.STM, percentageSTM);
        }

        private void SendKeyPressIfNeeded(AttributeType type, decimal value)
        {
            switch (type)
            {
                case AttributeType.HP:
                    RequestMainStreamAndUsePot(value, ValueHP, KeyHP);
                    break;
                case AttributeType.MP:
                    RequestMainStreamAndUsePot(value, ValueMP, KeyMP);
                    break;
                case AttributeType.STM:
                    RequestMainStreamAndUsePot(value, ValueSTM, KeySTM);
                    break;
                default:
                    break;
            }
        }

        private void RequestMainStreamAndUsePot(decimal currentValue, int valueToUsePot, Keys key)
        {
            if (currentValue < valueToUsePot)
            {
                FireRequestMainStream();

                AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey);
                AutoClickHandlers.SendKeyPress(_hWnd, key, 50);

                FireReleaseMainStream();
            }
        }
    }

    public class DeactivePottingStrategy : PottingStrategy
    {
        public DeactivePottingStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            Stop();
        }
    }
}
