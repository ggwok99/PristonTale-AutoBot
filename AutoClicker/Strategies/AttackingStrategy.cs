using AForge.Vision.Motion;
using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            Style = data.Style;
            MainSkillHotkey = data.MainSkillHotkey;
        }

        protected override double TimeOutInSecond()
        {
            return .75;
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

            BlobCountingObjectsProcessing motionProcessing = new BlobCountingObjectsProcessing(20, 50);
            MotionDetector detector = new MotionDetector(new TwoFramesDifferenceDetector(), motionProcessing);
            using (Bitmap screenCapture = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
            {
                detector.MotionZones = new Rectangle[] { ImageProcessing.GetRectangleAreaFromCenter(screenCapture, 85) };
            }

            bool found = false;
            AutoClickHandlers.SendKeyPress(_hWnd, MainSkillHotkey);
            while (!found)
            {
                using (Bitmap screenCapture = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
                {
                    if (detector.ProcessFrame(screenCapture) > 0.005 && motionProcessing.ObjectsCount > 0)
                    {
                        switch (Style)
                        {
                            case AttackStyleType.Melee_Attack:
                                {
                                    int i = AutoClickHandlers.Random(0, 20);
                                    if (i == 0)
                                    {
                                        found = ProcessSpellWithTarget(motionProcessing);
                                        break;
                                    }

                                    Point center = new Point(screenCapture.Width / 2, screenCapture.Height / 2);
                                    found = ProcessMeleeAttack(motionProcessing, center);
                                    break;
                                }

                            case AttackStyleType.Range_Attack:
                                {
                                    int i = AutoClickHandlers.Random(0, 20);
                                    if (i == 0)
                                    {
                                        found = ProcessSpellWithTarget(motionProcessing);
                                        break;
                                    }

                                    found = ProcessRangeAttack(motionProcessing);
                                    break;
                                }

                            case AttackStyleType.Spell_With_Target:
                                {
                                    found = ProcessSpellWithTarget(motionProcessing);
                                    break;
                                }

                            case AttackStyleType.Spell_AOE:
                                {
                                    detector.MotionZones = new Rectangle[] { ImageProcessing.GetRectangleAreaFromCenter(screenCapture, 25) };
                                    if (motionProcessing.ObjectsCount < 5)
                                    {
                                        continue;
                                    }

                                    AutoClickHandlers.RightClick(_hWnd);
                                    found = true;
                                    break;
                                }

                            default:
                                break;
                        }
                    }
                }
            }
        }

        private bool ProcessSpellWithTarget(BlobCountingObjectsProcessing motionProcessing)
        {
            Point point = GetRandomTargetPoint(motionProcessing);
            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.RightClickOnPoint(_hWnd, point.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
        }

        private bool ProcessRangeAttack(BlobCountingObjectsProcessing motionProcessing)
        {
            Point point = GetRandomTargetPoint(motionProcessing);
            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.LeftClickOnPoint(_hWnd, point.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
        }

        private static Point GetRandomTargetPoint(BlobCountingObjectsProcessing motionProcessing)
        {
            int count = motionProcessing.ObjectsCount;
            int index = 0;
            if (count > 1)
            {
                index = AutoClickHandlers.Random(0, count - 1);
            }

            RECT rectangle = motionProcessing.ObjectRectangles[index];
            return rectangle.Center;
        }

        private bool ProcessMeleeAttack(BlobCountingObjectsProcessing motionProcessing, Point point)
        {
            List<double> distances = new List<double>();
            double minDistance = 0;
            Point targetPoint = point;
            motionProcessing.ObjectRectangles.ToList().ForEach(x =>
            {
                RECT rect = x;
                Point center = Master.Instance.ScreenResolution.Center;
                double temp = (rect.Center.X - center.X) ^ 2 + (rect.Center.Y - center.Y) ^ 2;
                double distance = Math.Sqrt(temp);
                distances.Add(distance);
                if (distance <= distances.Min() && distance > 2)
                {
                    minDistance = distance;
                    targetPoint = rect.Center;
                }
            });

            if (targetPoint.X == point.X && targetPoint.Y == point.Y)
            {
                return false;
            }

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.LeftClickOnPoint(_hWnd, targetPoint.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
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
