using System;

namespace dpz3.Linux.X11 {
    public delegate short XHandleXError(IntPtr display, ref XErrorEvent error_event);
}