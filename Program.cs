using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Linq;

namespace anti_idle
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) 
                throw new Exception("Timeout is required");

            var x = new Random(900);
            var y = new Random(600);
            var interval = args.SingleOrDefault(arg => arg.StartsWith("--timeout")).Split("=")[1];
            do {
                var xDelta = x.Next(1000); // x axis 
                var yDelta = y.Next(550); // y axis

                VirtualMouse.SetCursorPos(xDelta, yDelta);
                VirtualMouse.LeftClick(xDelta, yDelta);

                Thread.Sleep(Convert.ToInt32(interval));
            }
            while (true); // infinite loop
        }
    }


    public static class VirtualMouse 
    {
        [DllImport("User32.Dll")]
        public static extern void SetCursorPos(int x, int y);
        [DllImport("User32.Dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void LeftClick(int x, int y) 
        {
            mouse_event(0x0002, x, y, 0, 0);
            mouse_event(0x0004, x, y, 0, 0);
        }

    } 
}
