using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 文本行
    /// </summary>
    public class MdTextLine : MdBlockBasic {

        /// <summary>
        /// 是否为段落
        /// </summary>
        public bool IsSection { get; set; }

        /// <summary>
        /// 实例化对象
        /// </summary>
        public MdTextLine() {
            this.IsSection = false;
        }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in base.Children) {
                sb.Append(md.ToString());
            }
            sb.Append("\r\n");
            if (this.IsSection) sb.Append("\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            StringBuilder sb = new StringBuilder();
            if (IsSection) sb.Append("<p>");
            foreach (var md in base.Children) {
                sb.Append(md.ToHtml());
            }
            if (IsSection) {
                sb.Append("</p>");
            } else {
                sb.Append("<br />");
            }
            return sb.ToString();
        }

    }
}
