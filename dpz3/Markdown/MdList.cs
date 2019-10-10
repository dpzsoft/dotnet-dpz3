using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 有序列表
    /// </summary>
    public class MdList : MdBasicBlock {

        /// <summary>
        /// 获取或设置是否为有序列表
        /// </summary>
        public bool IsOrdered { get; set; }

        /// <summary>
        /// 获取或设置序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 对象初始化
        /// </summary>
        public MdList() : base(MdTypes.List, "    ") {
            this.SerialNumber = 1;
            this.IsOrdered = false;
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
