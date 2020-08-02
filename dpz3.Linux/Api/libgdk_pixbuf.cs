using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libgdk_pixbuf
    /// </summary>
    public static class libgdk_pixbuf {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libgdk_pixbuf-2.0.so.0";

        #region Icon work

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_pixbuf_new_from_file_utf8(IntPtr filename, out IntPtr error);

        #endregion
    }
}