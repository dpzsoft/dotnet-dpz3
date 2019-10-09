using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 有序列表
    /// </summary>
    public class MdOrderedList : MdLevelBlockBasic {

        /// <summary>
        /// 获取或设置序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 对象初始化
        /// </summary>
        public MdOrderedList() {
            this.SerialNumber = 1;
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
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnGetHtmlString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ol start=\"{0}\">", this.SerialNumber);
            foreach (var md in base.Children) {
                sb.Append(md.ToHtml());
            }
            sb.Append("</ol>");
            return sb.ToString();
        }

    }
}
