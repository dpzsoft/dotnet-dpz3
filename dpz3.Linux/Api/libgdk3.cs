using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libgdk3
    /// </summary>
    public static class libgdk3 {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libgdk-3.so.0";
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gdk_set_allowed_backends(string backend);
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_window_get_xid(IntPtr raw);
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_display_get_xdisplay(IntPtr gdkDisplay);
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gdk_window_resize(IntPtr window, int width, int height);
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gdk_window_move_resize(IntPtr window, int x, int y, int width, int height);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_visual_get_xvisual(IntPtr handle);
        
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_screen_get_default();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_screen_lookup_visual(IntPtr screen, IntPtr xvisualid);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_screen_list_visuals(IntPtr raw);
    }
}