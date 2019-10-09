using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 表格头部数据
    /// </summary>
    public class MdTableHeader : MdTableCell {

        /// <summary>
        /// 获取或设置对齐方式
        /// </summary>
        public string Align { get; private set; }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return String.Format("| {0} ", Parser.Escape(base.Content));
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            if (this.Align.IsNoneOrNull()) {
                return String.Format("<th>{0}</th>", base.Content);
            } else {
                return String.Format("<th align=\"{0}\">{1}</th>", this.Align, base.Content);
            }

        }

    }
}
