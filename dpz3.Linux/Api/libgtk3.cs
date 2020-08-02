using System;
using System.Runtime.InteropServices;
using System.Text;
using dpz3.Linux.Gtk3;

namespace dpz3.Linux.Api {
    /// <summary>
    /// libgtk3
    /// </summary>
    public static class libgtk3 {
        /// <summary>
        /// 库名称
        /// </summary>
        public const string LibName = "libgtk-3.so.0";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_init(int argc, string[] argv);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_window_new(GtkWindowType type);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = false)]
        public static extern void gtk_main();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_widget_get_window(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_widget_set_visual(IntPtr widget, IntPtr visual);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_widget_get_display(IntPtr window);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_widget_show_all(IntPtr window);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_window_get_size(IntPtr window, out int width, out int height);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_window_set_title(IntPtr window, string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_window_set_default_size(IntPtr window, int width, int height);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = false)]
        public static extern bool gtk_window_set_icon_from_file(IntPtr raw, string filename, out IntPtr err);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool gtk_window_set_position(IntPtr window, GtkWindowPosition position);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool gtk_window_maximize(IntPtr window);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool gtk_window_fullscreen(IntPtr window);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_main_quit();

        // MessageBox
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_message_dialog_new(IntPtr parent_window, DialogFlags flags, MessageType type,
            ButtonsType bt, string msg, IntPtr args);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gtk_dialog_run(IntPtr raw);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_widget_destroy(IntPtr widget);
    }
}