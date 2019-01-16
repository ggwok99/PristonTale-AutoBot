using AutoClicker.Enums;
using AutoClicker.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker.Helpers
{
    public static class AutoClickHandlers
    {
        public const int SW_BringToFront = 1;
        public const int SW_MAXIMIZED = 3;
        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x0101;
        public const int WM_COMMAND = 0x111;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_RBUTTONDBLCLK = 0x206;

        public static void RightClick(IntPtr hWnd, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            User32.SendMessage(hWnd, WM_RBUTTONDOWN, (IntPtr)0x00000001, IntPtr.Zero);
            User32.SendMessage(hWnd, WM_RBUTTONUP, (IntPtr)0x00000001, IntPtr.Zero);
        }

        public static void SendKeyPress(IntPtr hWnd, Keys key, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            SendKeyDown(hWnd, key);
            SendKeyUp(hWnd, key);
        }

        public static void SendKeyDown(IntPtr hWnd, Keys key, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            User32.SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, IntPtr.Zero);
        }

        public static void SendKeyUp(IntPtr hWnd, Keys key, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            User32.SendMessage(hWnd, WM_KEYUP, (IntPtr)key, IntPtr.Zero);
        }

        public static void LeftClickOnPoint(IntPtr hWnd, Point point, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            int w = (point.Y << 16) | point.X;
            SendMouseToPoint(hWnd, point);
            User32.SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)0x00000001, (IntPtr)w);
            User32.SendMessage(hWnd, WM_LBUTTONUP, (IntPtr)0x00000001, (IntPtr)w);
        }

        public static void RightClickOnPoint(IntPtr hWnd, Point point, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            int w = (point.Y << 16) | point.X;
            SendMouseToPoint(hWnd, point);
            User32.SendMessage(hWnd, WM_RBUTTONDOWN, (IntPtr)0x00000001, (IntPtr)w);
            User32.SendMessage(hWnd, WM_RBUTTONUP, (IntPtr)0x00000001, (IntPtr)w);
        }

        public static void SendMouseToPoint(IntPtr hWnd, Point point, int delayInMinisecond = 0)
        {
            if (delayInMinisecond > 0)
            {
                Thread.Sleep(delayInMinisecond);
            }

            int w = (point.Y << 16) | point.X;
            User32.SendMessage(hWnd, WM_MOUSEMOVE, (IntPtr)0x00000001, (IntPtr)w);
        }

        public static Point GetRandomPointFromCenter(int targetArea)
        {
            Point centerPoint = Master.Instance.ScreenResolution.Center;
            decimal maxMultiplier = 1 + targetArea / 100m;
            decimal minMultiplier = 1 - targetArea / 100m;
            int xPosition = Random(centerPoint.X * minMultiplier, centerPoint.X * maxMultiplier);
            int yPosition = Random(centerPoint.Y * minMultiplier, centerPoint.Y * maxMultiplier);
            return new Point(xPosition, yPosition);
        }

        public static int Random(decimal min, decimal max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(Convert.ToInt32(min), Convert.ToInt32(max));
        }

        internal static void LeftClickOnPoint(IntPtr hWnd, object p)
        {
            throw new NotImplementedException();
        }

        public static Point ScaleDownToClientPoint(this Point point, IntPtr hWnd)
        {
            RECT windowRect = new RECT();
            User32.GetWindowRect(hWnd, ref windowRect);
            decimal xMultiplier = (decimal)Master.Instance.ScreenResolution.X / windowRect.Width;
            decimal yMultiplier = (decimal)Master.Instance.ScreenResolution.Y / windowRect.Height;
            return new Point((int)(point.X * xMultiplier), (int)(point.Y * yMultiplier));
        }
    }
}
