using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// Markdown 文档对象
    /// </summary>
    public class MdDocument : MdBasicBlock {

        /// <summary>
        /// 对象实例化
        /// </summary>
        public MdDocument() : base(MdTypes.Document) { }

    }
}
