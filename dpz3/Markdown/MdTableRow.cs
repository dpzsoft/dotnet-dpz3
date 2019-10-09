using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 表格行
    /// </summary>
    public class MdTableRow : MdBasic {

        /// <summary>
        /// 获取头定义集合
        /// </summary>
        public List<MdTableCell> Children { get; private set; }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in this.Children) {
                sb.Append(md.ToString());
                sb.Append("|\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr>");
            foreach (var md in this.Children) {
                sb.Append(md.ToHtml());
            }
            sb.Append("</tr>");
            return sb.ToString();
        }

    }
}
