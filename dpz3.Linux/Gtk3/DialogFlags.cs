using System;

namespace dpz3.Linux.Gtk3 {
    [Flags]
    public enum DialogFlags {
        Modal = 1 << 0,
        DestroyWithParent = 1 << 1,
    }
}