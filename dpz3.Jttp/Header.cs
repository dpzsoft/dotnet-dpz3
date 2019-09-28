using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Jttp {

    /// <summary>
    /// 头部定义
    /// </summary>
    public class Header {

        /// <summary>
        /// 获取或设置版本信息
        /// </summary>
        public string Ver { get; set; }

        /// <summary>
        /// 获取或设置交互类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置交互信息
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// 获取或设置时间戳信息
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 获取或设置状态信息
        /// </summary>
        public int Status { get; set; }

    }
}
