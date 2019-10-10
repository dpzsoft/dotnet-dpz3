using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 表格头部数据
    /// </summary>
    public class MdTableAlign : MdBasicContent {

        /// <summary>
        /// 对象实例化
        /// </summary>
        public MdTableAlign() : base(MdTypes.TableAlign) { }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnGetMarkdownString() {
            switch (base.Content) {
                case "left":
                    return "| :----- ";
                case "center":
                    return "| :----: ";
                case "right":
                    return "| -----: ";
                default:
                    return "| ------ ";
            }
        }

    }
}
