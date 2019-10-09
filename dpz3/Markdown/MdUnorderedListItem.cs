using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 无序列表项
    /// </summary>
    public class MdUnorderedListItem : MdLevelContentBasic {


        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < base.Level; i++) {
                sb.Append(base.LevelString);
            }
            sb.Append("+ ");
            sb.Append(Parser.Escape(base.Content));
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            return String.Format("<li>{0}</li>", base.Content);
        }

    }
}
