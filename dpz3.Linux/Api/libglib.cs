using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libglib
    /// </summary>
    public static class libglib {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libglib-2.0.so.0";
        
        #region DLLIMPORTS GlibLib

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int g_list_length(IntPtr l);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void g_list_free(IntPtr l);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr g_list_nth_data(IntPtr l, uint n);

        #endregion
    }
}