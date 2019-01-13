using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            return .15;
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
        public ActivePottingStrategy(IntPtr intPtr)
        {
            Initial(intPtr);
        }

        protected override void DoWork()
        {
            using (Bitmap b = new Bitmap(ScreenCapture.CaptureWindow(_hWnd)))
            {
                foreach (CharacterAttributePosition pot in Master.Instance.PotPositions)
                {
                    Rectangle cloneRect = new Rectangle(pot.Position.Location, pot.Position.Size);
                    using (Bitmap cloneBitmap = b.Clone(cloneRect, b.PixelFormat))
                    {
                        decimal missingPixels = 0m;
                        List<Color> colors = ImageProcessing.ReadBitmap(cloneBitmap);
                        colors.ForEach(color =>
                        {
                            if (color != GetColorByAttributeType(pot.Type))
                            {
                                missingPixels++;
                            }
                        });

                        decimal value = 100 - (missingPixels / colors.Count * 100);
                        SendKeyPressIfNeeded(pot.Type, value);
                    }
                }
            }
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

        private Color GetColorByAttributeType(AttributeType type)
        {
            switch (type)
            {
                case AttributeType.HP:
                    return Color.Red;
                case AttributeType.MP:
                    return Color.Blue;
                case AttributeType.STM:
                    return Color.GreenYellow;
                default:
                    return Color.DarkGray;
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
