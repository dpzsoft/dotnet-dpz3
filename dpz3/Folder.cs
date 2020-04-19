using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3 {

    /// <summary>
    /// 文件夹操作
    /// </summary>
    public static class Folder {

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void Create(string path) {
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 获取所属子文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetDirectories(string path) {
            return System.IO.Directory.GetDirectories(path);
        }

        /// <summary>
        /// 获取所属文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string pattern = null) {
            if (pattern == null) {
                return System.IO.Directory.GetFiles(path);
            } else {
                return System.IO.Directory.GetFiles(path, pattern);
            }
        }

    }
}
