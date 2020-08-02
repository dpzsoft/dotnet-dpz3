using System;
using System.Runtime.InteropServices;
using System.Text;
using dpz3.Linux.GObject;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libgobject
    /// </summary>
    public static class libgobject {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libgobject-2.0.so.0";

        // Signals
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint g_signal_connect_data(IntPtr instance, string detailedSignal, IntPtr handler,
            IntPtr data, GClosureNotify destroyData, GConnectFlags connectFlags);
    }
}