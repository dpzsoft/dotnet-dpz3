using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 表格
    /// </summary>
    public class MdTable : MdBasic {

        /// <summary>
        /// 获取头定义集合
        /// </summary>
        public List<MdTableRow> Headers { get; private set; }

        /// <summary>
        /// 获取头定义集合
        /// </summary>
        public List<MdTableAlign> Aligns { get; private set; }

        /// <summary>
        /// 获取数据定义集合
        /// </summary>
        public List<MdTableRow> Rows { get; private set; }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in this.Headers) {
                sb.Append(md.ToString());
            }
            foreach (var md in this.Aligns) {
                sb.Append(md.ToString());
                sb.Append("|\r\n");
            }
            foreach (var md in this.Rows) {
                sb.Append(md.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var md in this.Headers) {
                sb.Append(md.ToHtml());
            }
            sb.Append("</table>");
            return sb.ToString();
        }

    }
}
