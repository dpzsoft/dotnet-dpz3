﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 四级标题
    /// </summary>
    public class MdTitle4 : MdContentBasic {

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return String.Format("#### {0}\r\n\r\n", Parser.Escape(base.Content));
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            return String.Format("<h4>{0}</h4>", base.Content);
        }

    }
}
