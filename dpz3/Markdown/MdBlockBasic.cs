using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 包含内容的Markdown元素的基础块对象
    /// </summary>
    public abstract class MdBlockBasic : MdBasic {

        /// <summary>
        /// 获取子元素集合
        /// </summary>
        public List<MdBasic> Children { get; private set; }

        /// <summary>
        /// 实例化块对象
        /// </summary>
        public MdBlockBasic() {
            this.Children = new List<MdBasic>();
        }

    }
}
