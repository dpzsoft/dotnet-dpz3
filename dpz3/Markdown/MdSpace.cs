﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 空行，可用于清除格式
    /// </summary>
    public class MdSpace : MdBasic {

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return "\r\n";
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            return "";
        }

    }
}
