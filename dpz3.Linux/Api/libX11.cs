using System;
using System.Runtime.InteropServices;
using System.Text;
using dpz3.Linux.X11;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libX11
    /// </summary>
    public static class libX11 {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libX11.so.6";

        [DllImport(LibName)]
        public static extern int XMoveWindow(IntPtr display, IntPtr w, int x, int y);

        [DllImport(LibName)]
        public static extern int XResizeWindow(IntPtr display, IntPtr w, int width, int height);

        [DllImport(LibName)]
        public static extern int XMoveResizeWindow(IntPtr display, IntPtr w, int x, int y, int width, int height);

        [DllImport(LibName)]
        public static extern IntPtr XOpenDisplay(IntPtr display);

        [DllImport(LibName)]
        public static extern int XCloseDisplay(IntPtr display);

        [DllImport(LibName)]
        public static extern int XDefaultScreen(IntPtr display);

        [DllImport(LibName)]
        public static extern IntPtr XDefaultVisual(IntPtr display, int screen);

        [DllImport(LibName)]
        public static extern IntPtr XVisualIDFromVisual(IntPtr visual);

        [DllImport(LibName)]
        public static extern short XSetErrorHandler(XHandleXError err);

        [DllImport(LibName)]
        public static extern short XSetIOErrorHandler(XHandleXIOError err);

        [DllImport(LibName)]
        public extern static IntPtr XGetErrorText(IntPtr display, byte code, StringBuilder buffer, int length);
    }
}