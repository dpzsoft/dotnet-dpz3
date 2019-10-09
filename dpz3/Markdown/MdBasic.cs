using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// Markdown的基础对象
    /// </summary>
    public abstract class MdBasic : dpz3.Object {

        /// <summary>
        /// 获取HTML字符串
        /// </summary>
        /// <returns></returns>
        protected virtual string OnGetHtmlString() { return null; }

        /// <summary>
        /// 转化为HTML代码
        /// </summary>
        /// <returns></returns>
        public string ToHtml() {
            return this.OnGetHtmlString();
        }

    }
}
