using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Net {

    /// <summary>
    /// TCP错误信息
    /// </summary>
    public class TcpErrorActionArgs : dpz3.Object {

        /// <summary>
        /// 获取或设置错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 获取或设置信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置详情
        /// </summary>
        public string Detail { get; set; }

    }
}
