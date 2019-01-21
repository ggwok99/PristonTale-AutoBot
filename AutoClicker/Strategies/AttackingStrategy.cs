using AForge.Vision.Motion;
using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
                detector.MotionZones = new Rectangle[] { ImageProcessing.GetRectangleAreaFromCenter(screenCapture, 75) };
            }

            bool found = false;
            AutoClickHandlers.SendKeyPress(_hWnd, MainSkillHotkey);
            int trial = 0;
            while (!found && _mainStream)
            {
                using (Bitmap screenCapture = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
                {
                    if (detector.ProcessFrame(screenCapture.FilterForAutoAttacking()) > 0.002 && motionProcessing.ObjectsCount > 0)
                    {
                        switch (Style)
                        {
                            case AttackStyleType.Melee_Attack:
                                {
                                    int i = AutoClickHandlers.Random(0, 5);
                                    if (i == 0)
                                    {
                                        RECT rect = motionProcessing.ObjectRectangles.FirstOrDefault();

                                        found = ProcessSpellWithTarget(rect);
                                        break;
                                    }

                                    Point center = new Point(screenCapture.Width / 2, screenCapture.Height / 2);
                                    found = ProcessMeleeAttack(motionProcessing, center);
                                    break;
                                }

                            case AttackStyleType.Range_Attack:
                                {
                                    RECT rect = motionProcessing.ObjectRectangles.FirstOrDefault();

                                    int i = AutoClickHandlers.Random(0, 5);
                                    if (i == 0)
                                    {
                                        found = ProcessSpellWithTarget(rect);
                                        break;
                                    }

                                    found = ProcessRangeAttack(rect);
                                    break;
                                }

                            case AttackStyleType.Spell_With_Target:
                                {
                                    RECT rect = motionProcessing.ObjectRectangles.FirstOrDefault();

                                    found = ProcessSpellWithTarget(rect);
                                    break;
                                }

                            case AttackStyleType.Spell_AOE:
                                {
                                    detector.MotionZones = new Rectangle[] { ImageProcessing.GetRectangleAreaFromCenter(screenCapture, 25) };
                                    if (motionProcessing.ObjectsCount < 5)
                                    {
                                        break;
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

                trial++;
                if (trial > 10)
                {
                    break;
                }
            }
        }

        private bool ProcessSpellWithTarget(RECT rect)
        {
            if (!VerifyTarget(rect))
            {
                return false;
            }

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.RightClickOnPoint(_hWnd, rect.Center.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
        }

        private bool ProcessRangeAttack(RECT rect)
        {
            if (!VerifyTarget(rect))
            {
                return false;
            }

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.LeftClickOnPoint(_hWnd, rect.Center.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
        }

        private bool ProcessMeleeAttack(BlobCountingObjectsProcessing motionProcessing, Point center)
        {
            List<double> distances = new List<double>();
            double minDistance = 0;
            Point targetPoint = center;
            RECT minDistanceRectangle = new RECT();
            motionProcessing.ObjectRectangles.ToList().ForEach(x =>
            {
                RECT rect = x;
                double temp = (rect.Center.X - center.X) ^ 2 + (rect.Center.Y - center.Y) ^ 2;
                double distance = Math.Sqrt(temp);
                distances.Add(distance);
                if (distance <= distances.Min() && distance > .75)
                {
                    minDistance = distance;
                    targetPoint = rect.Center;
                    minDistanceRectangle = rect;
                }
            });

            if (targetPoint.X == center.X && targetPoint.Y == center.Y || !VerifyTarget(minDistanceRectangle))
            {
                return false;
            }

            AutoClickHandlers.SendKeyDown(_hWnd, Keys.ShiftKey);
            AutoClickHandlers.LeftClickOnPoint(_hWnd, targetPoint.ScaleDownToClientPoint(_hWnd));
            AutoClickHandlers.SendKeyUp(_hWnd, Keys.ShiftKey, 50);
            return true;
        }

        private bool VerifyTarget(RECT minDistanceRectangle)
        {
            BlobCountingObjectsProcessing tagFilterProcessing = new BlobCountingObjectsProcessing(5, 2);
            MotionDetector tagDetector = new MotionDetector(new TwoFramesDifferenceDetector(), tagFilterProcessing)
            {
                MotionZones = new Rectangle[] { new RECT(minDistanceRectangle.Left, 0, minDistanceRectangle.Right, minDistanceRectangle.Bottom) }
            };

            using (Bitmap backgroundFrame = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
            {
                tagDetector.ProcessFrame(backgroundFrame.FilterForBlackTags());
                AutoClickHandlers.SendMouseToPoint(_hWnd, minDistanceRectangle.Center.ScaleDownToClientPoint(_hWnd));
                Thread.Sleep(500);
                using (Bitmap currentFrame = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
                {
                    if (tagDetector.ProcessFrame(currentFrame.FilterForBlackTags()) > 0.0002 && tagFilterProcessing.ObjectsCount >= 1)
                    {
                        return true;
                    }
                }
            }

            return false;
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
