using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 带层级的块基础对象
    /// </summary>
    public abstract class MdLevelContentBasic : MdContentBasic {

        /// <summary>
        /// 获取或设置层次
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 获取或设置层次表示字符串
        /// </summary>
        public string LevelString { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        public MdLevelContentBasic() {
            this.Level = 0;
            this.LevelString = "    ";
        }

    }
}
