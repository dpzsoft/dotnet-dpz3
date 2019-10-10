using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 区块
    /// </summary>
    public class MdBlock : MdBasicBlock {

        /// <summary>
        /// 对象实例化
        /// </summary>
        public MdBlock() : base(MdTypes.Block, "> ") { }

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
