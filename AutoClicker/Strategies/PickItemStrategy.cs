using AForge.Imaging.Filters;
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
    public abstract class PickItemStrategy : AutoStrategy
    {
        protected int TargetArea { get; set; }

        public override void LoadData(dynamic data)
        {
            TargetArea = data.TargetArea;
        }

        protected override double TimeOutInSecond()
        {
            return 15;
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

            BlobCountingObjectsProcessing motionProcessing = new BlobCountingObjectsProcessing(5, 2);
            MotionDetector detector = new MotionDetector(new TwoFramesDifferenceDetector(), motionProcessing);
            EuclideanColorFiltering colorFilter = new EuclideanColorFiltering
            {
                CenterColor = new AForge.Imaging.RGB(Color.Black),
                Radius = 25,
                FillColor = new AForge.Imaging.RGB(Color.White)
            };

            int i = 0;
            while (i < 5)
            {
                using (Bitmap backgroundFrame = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
                {
                    detector.MotionZones = new Rectangle[] { ImageProcessing.GetRectangleAreaFromCenter(backgroundFrame, 20) };

                    colorFilter.ApplyInPlace(backgroundFrame);
                    detector.ProcessFrame(backgroundFrame);

                    AutoClickHandlers.SendKeyDown(_hWnd, Keys.A);

                    Thread.Sleep(200);
                    using (Bitmap currentFrame = (Bitmap)ScreenCapture.CaptureWindow(_hWnd))
                    {
                        colorFilter.ApplyInPlace(currentFrame);

                        if (detector.ProcessFrame(currentFrame) > 0.0002 && motionProcessing.ObjectsCount >= 1)
                        {
                            RECT rect = motionProcessing.ObjectRectangles.FirstOrDefault();
                            AutoClickHandlers.LeftClickOnPoint(_hWnd, rect.Center.ScaleDownToClientPoint(_hWnd));
                            Thread.Sleep(500);
                        }
                        else
                        {
                            AutoClickHandlers.SendKeyUp(_hWnd, Keys.A);
                            FireReleaseMainStream();
                            break;
                        }
                    }

                    AutoClickHandlers.SendKeyUp(_hWnd, Keys.A);
                    Thread.Sleep(200);
                }

                i++;
            }

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
