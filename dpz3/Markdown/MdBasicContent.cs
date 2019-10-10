using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 包含内容的Markdown元素的基础对象
    /// </summary>
    public abstract class MdBasicContent : MdBasic {

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 对象实例化
        /// <param name="mdType"></param>
        /// </summary>
        public MdBasicContent(MdTypes mdType) : base(mdType) { }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return this.Content;
        }

    }
}
