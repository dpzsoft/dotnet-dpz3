using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// 基础对象集合
    /// </summary>
    public class MdBasicCollection : List<MdBasic> {

        /// <summary>
        /// 父区块
        /// </summary>
        public MdBasicBlock ParentBlock { get; private set; }

        /// <summary>
        /// 获取子元素集合
        /// </summary>
        public List<MdBasic> Children { get; private set; }

        /// <summary>
        /// 实例化块对象
        /// </summary>
        public MdBasicCollection(MdBasicBlock parent) {
            this.ParentBlock = parent;
            this.Children = new List<MdBasic>();
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="md"></param>
        public new void Add(MdBasic md) {
            md.ParentBlock = this.ParentBlock;
            base.Add(md);
        }

        /// <summary>
        /// 获取标准的Markdown字符串
        /// </summary>
        /// <returns></returns>
        internal string GetMarkdownString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in this.Children) {
                sb.Append(md.ToMarkdown());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取标准字符串表示
        /// </summary>
        /// <returns></returns>
        internal string GetString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in this.Children) {
                sb.Append(md.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取HTML表示形式
        /// </summary>
        /// <returns></returns>
        internal string GetHtmlString() {
            StringBuilder sb = new StringBuilder();
            foreach (var md in this.Children) {
                sb.Append(md.ToHtml());
            }
            return sb.ToString();
        }

    }
}
