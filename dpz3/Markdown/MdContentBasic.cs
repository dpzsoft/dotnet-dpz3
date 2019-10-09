using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 包含内容的Markdown元素的基础对象
    /// </summary>
    public abstract class MdContentBasic : MdBasic {

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public string Content { get; set; }

    }
}
