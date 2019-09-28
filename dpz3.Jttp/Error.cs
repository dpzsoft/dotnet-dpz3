using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Jttp {

    /// <summary>
    /// 错误信息定义
    /// </summary>
    public class Error {

        /// <summary>
        /// 获取或设置错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        public string Info { get; set; }

    }
}
