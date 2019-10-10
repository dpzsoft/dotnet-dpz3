using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 代码块
    /// </summary>
    public class MdCodeBlock : MdBasicContent {

        /// <summary>
        /// 对象实例化
        /// </summary>
        public MdCodeBlock() : base(MdTypes.CodeBlock) {}

        /// <summary>
        /// 获取或设置语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnGetMarkdownString() {
            return String.Format("```{0}\r\n{1}\r\n```\r\n", this.Language, base.Content);
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            return String.Format("<code>{0}</code>", base.Content);
        }

    }
}
