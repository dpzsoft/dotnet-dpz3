using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 表格头部数据
    /// </summary>
    public class MdTableAlign : MdContentBasic {

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
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
