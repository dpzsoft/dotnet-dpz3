using System;
using System.Runtime.InteropServices;

namespace dpz3.Linux.X11 {
    [StructLayout(LayoutKind.Sequential)]
    public struct XErrorEvent {
        public int type;
        public IntPtr display;
        public int resourceid;
        public int serial;
        public byte error_code;
        public byte request_code;
        public byte minor_code;
    }
}