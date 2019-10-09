using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 区块
    /// </summary>
    public class MdBlock : MdLevelBlockBasic {

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in base.Children) {
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
            sb.Append("<blockquote>");
            foreach (var md in base.Children) {
                sb.Append(md.ToHtml());
            }
            sb.Append("</blockquote>");
            return sb.ToString();
        }

    }
}
