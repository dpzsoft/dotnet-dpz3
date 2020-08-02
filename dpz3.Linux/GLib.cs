using System;
using static dpz3.Linux.Api.libglib;

namespace dpz3.Linux {
    /// <summary>
    /// GLib
    /// </summary>
    public class GLib {
        private readonly IntPtr _list;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="list"></param>
        public GLib(IntPtr list) {
            _list = list;
        }

        public int Length {
            get { return g_list_length(_list); }
        }

        public void Free() {
            if (_list != IntPtr.Zero)
                g_list_free(_list);
        }

        public IntPtr GetItem(int nth) {
            if (_list != IntPtr.Zero)
                return g_list_nth_data(_list, (uint) nth);

            return IntPtr.Zero;
        }
    }
}