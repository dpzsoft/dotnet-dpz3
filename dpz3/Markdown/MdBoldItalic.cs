using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 文本对象
    /// </summary>
    public class MdBoldItalic : MdContentBasic {

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return String.Format("***{0}***", Parser.Escape(base.Content));
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            return String.Format("<b><i>{0}</i></b>", base.Content);
        }

    }
}
