using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.VDisk {

    /// <summary>
    /// 关联数据类型
    /// </summary>
    public class Types {

        /// <summary>
        /// 硬盘信息
        /// </summary>
        public struct VirtualDiskInfo {

            /// <summary>
            /// 信息标志
            /// </summary>
            public byte[] Sign;

            /// <summary>
            /// 定义版本
            /// </summary>
            public int Version;

            /// <summary>
            /// 数据区块记录
            /// </summary>
            public long DataBlocks;

        }

        /// <summary>
        /// 文件信息
        /// </summary>
        public struct VirtualPathInfo {

            /// <summary>
            /// 文件寻址
            /// </summary>
            public long Position;

            /// <summary>
            /// 下一个文件寻址
            /// </summary>
            public long NextPosition;

            /// <summary>
            /// 第一个子文件寻址
            /// </summary>
            public long FirstChildPosition;

            /// <summary>
            /// 存储类型
            /// </summary>
            public byte Type;

            /// <summary>
            /// 存储名称
            /// </summary>
            public byte[] Name;

        }

        /// <summary>
        /// 数据信息
        /// </summary>
        public struct VirtualDataInfo {

            /// <summary>
            /// 数据寻址
            /// </summary>
            public long Position;

            /// <summary>
            /// 下一个文件寻址
            /// </summary>
            public long NextPosition;

            /// <summary>
            /// 数据长度
            /// </summary>
            public int Length;

            /// <summary>
            /// 数据内容
            /// </summary>
            public byte[] Data;

        }

    }
}
