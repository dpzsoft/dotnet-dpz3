using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace dpz3.Linux.Api {

    /// <summary>
    /// 系统函数
    /// </summary>
    public static class libc {

        public const int O_RDONLY = 0x0000;
        public const int O_WRONLY = 0x0001;
        public const int O_RDWR = 0x0002;
        public const int O_ACCMODE = 0x0003;

        public const int O_CREAT = 0x0100; /* second byte, away from DOS bits */
        public const int O_EXCL = 0x0200;
        public const int O_NOCTTY = 0x0400;
        public const int O_TRUNC = 0x0800;
        public const int O_APPEND = 0x1000;
        public const int O_NONBLOCK = 0x2000;
        public static readonly ulong TIOCSWINSZ = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? 0x80087467 : 0x5414;

        public const int _SC_OPEN_MAX = 5;

        public const int EAGAIN = 11;  /* Try again */

        public const int EINTR = 4; /* Interrupted system call */

        public const int ENOENT = 2;

        public static readonly ulong TIOCSCTTY = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? (ulong)0x20007484 : 0x540E;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct winsize {
            public ushort ws_row;   /* rows, in characters */
            public ushort ws_col;   /* columns, in characters */
            public ushort ws_xpixel;    /* horizontal size, pixels */
            public ushort ws_ypixel;    /* vertical size, pixels */
        };

        public static IntPtr StructToPtr(object obj) {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
            Marshal.StructureToPtr(obj, ptr, false);
            return ptr;
        }

        /// <summary>
        /// The `execv` POSIX syscall we use to exec /bin/sh.
        /// </summary>
        /// <param name="path">The path to the executable to exec.</param>
        /// <param name="args">
        /// The arguments to send through to the executable.
        /// Array must have its final element be null.
        /// </param>
        /// <returns>
        /// An exit code if exec failed, but if successful the calling process will be overwritten.
        /// </returns>
        [DllImport("libc",
            EntryPoint = "execv",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int execv(string path, string[] args);

        [DllImport("libc",
            EntryPoint = "dup",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int dup(int fd);


        [DllImport("libc",
            EntryPoint = "dup2",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int dup2(int oldfd, int newfd);

        /// <summary>
        /// The `readlink` POSIX syscall we use to read the symlink from /proc/self/exe
        /// to get the executable path of pwsh on Linux.
        /// </summary>
        /// <param name="pathname">The path to the symlink to read.</param>
        /// <param name="buf">Pointer to a buffer to fill with the result.</param>
        /// <param name="size">The size of the buffer we have supplied.</param>
        /// <returns>The number of bytes placed in the buffer.</returns>
        [DllImport("libc",
            EntryPoint = "readlink",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern IntPtr readlink(string pathname, IntPtr buf, UIntPtr size);

        /// <summary>
        /// The `getpid` POSIX syscall we use to quickly get the current process PID on macOS.
        /// </summary>
        /// <returns>The pid of the current process.</returns>
        [DllImport("libc",
            EntryPoint = "getpid",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int getpid();

        /// <summary>
        /// The `setenv` POSIX syscall used to set an environment variable in the process.
        /// </summary>
        /// <param name="name">The name of the environment variable.</param>
        /// <param name="value">The value of the environment variable.</param>
        /// <param name="overwrite">If true, will overwrite an existing environment variable of the same name.</param>
        /// <returns>0 if successful, -1 on error. errno indicates the reason for failure.</returns>
        [DllImport("libc",
            EntryPoint = "setenv",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int setenv(string name, string value, bool overwrite);

        /// <summary>
        /// The `sysctl` BSD sycall used to get system information on macOS.
        /// </summary>
        /// <param name="mib">The Management Information Base name, used to query information.</param>
        /// <param name="mibLength">The length of the MIB name.</param>
        /// <param name="oldp">The object passed out of sysctl (may be null)</param>
        /// <param name="oldlenp">The size of the object passed out of sysctl.</param>
        /// <param name="newp">The object passed in to sysctl.</param>
        /// <param name="newlenp">The length of the object passed in to sysctl.</param>
        /// <returns></returns>
        [DllImport("libc",
            EntryPoint = "sysctl",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int sysctl(int* mib, int mibLength, void* oldp, int* oldlenp, IntPtr newp, int newlenp);

        [DllImport("libc",
            EntryPoint = "open",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int open(string file, int oflag);

        [DllImport("libc",
            EntryPoint = "grantpt",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int grantpt(int mfd);

        [DllImport("libc",
            EntryPoint = "unlockpt",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int unlockpt(int mfd);

        [DllImport("libc",
            EntryPoint = "ptsname",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern IntPtr ptsname(int mfd);

        [DllImport("libc",
            EntryPoint = "fork",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int fork();

        [DllImport("libc",
            EntryPoint = "read",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int read(int fid, byte[] buffer, int flag);

        [DllImport("libc",
            EntryPoint = "write",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int write(int fid, byte[] buffer, int flag);

        [DllImport("libc",
            EntryPoint = "write",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static unsafe extern int write(int fid, IntPtr buffer, int flag);

        [DllImport("libc",
            EntryPoint = "ioctl",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int ioctl(int fd, ulong request, IntPtr type);

        [DllImport("libc",
            EntryPoint = "ioctl",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int ioctl(int fd, ulong request, int type);

        [DllImport("libc",
            EntryPoint = "ioctl",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int ioctl(int fd, ulong request, string type);

        [DllImport("libc",
            EntryPoint = "setsid",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int setsid();

        [DllImport("libc",
            EntryPoint = "posix_spawn_file_actions_adddup2",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int posix_spawn_file_actions_adddup2(IntPtr file_actions, int fildes, int newfildes);

        [DllImport("libc",
            EntryPoint = "posix_spawn_file_actions_addclose",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int posix_spawn_file_actions_addclose(IntPtr file_actions, int fildes);

        [DllImport("libc",
            EntryPoint = "posix_spawn_file_actions_init",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int posix_spawn_file_actions_init(IntPtr file_actions);

        [DllImport("libc",
            EntryPoint = "posix_spawnattr_init",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int posix_spawnattr_init(IntPtr attributes);

        [DllImport("libc",
            EntryPoint = "posix_spawnp",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int posix_spawnp(out IntPtr pid, string path, IntPtr fileActions, IntPtr attrib, string[] argv, string[] envp);

        [DllImport("libc",
            EntryPoint = "chdir",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern int chdir(string path);

        [DllImport("libc",
            EntryPoint = "execve",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern void execve([MarshalAs(UnmanagedType.LPStr)]string path, [MarshalAs(UnmanagedType.LPArray)]string[] argv, [MarshalAs(UnmanagedType.LPArray)]string[] envp);

    }
}
