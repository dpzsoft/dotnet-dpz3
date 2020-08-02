using System;

namespace dpz3.Linux.Gtk3 {
    [Flags]
    public enum GtkWindowPosition {
        GtkWinPosNone,
        GtkWinPosCenter,
        GtkWinPosMouse,
        GtkWinPosCenterAlways,
        GtkWinPosCenterOnParent
    }
}