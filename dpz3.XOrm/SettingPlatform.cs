using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.XOrm {

    /// <summary>
    /// 平台设置项
    /// </summary>
    public class SettingPlatform : dpz3.Object {

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取表格定义集合
        /// </summary>
        public List<SettingTable> Tables { get; private set; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public SettingPlatform() {
            this.Tables = new List<SettingTable>();
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="node"></param>
        public void FillData(dpz3.Xml.XmlNode node) {
            this.Name = node.Attr["name"];
            this.Title = node.Attr["title"];
        }

        /// <summary>
        /// 获取标准字符串表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<platform name=\"{0}\" title=\"{1}\">", this.Name, this.Title);
            for (int i = 0; i < this.Tables.Count; i++) {
                sb.Append(this.Tables[i].ToString());
            }
            sb.Append("</platform>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取或快速创建一个表配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SettingTable this[string name] {
            get {
                // 从已有的平台配置中查找
                for (int i = 0; i < this.Tables.Count; i++) {
                    if (this.Tables[i].Name == name) {
                        return this.Tables[i];
                    }
                }
                // 添加一个新的平台配置
                SettingTable table = new SettingTable();
                table.Name = name;
                this.Tables.Add(table);
                return table;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnDispose() {

            this.Name = null;
            this.Title = null;

            for (int i = 0; i < this.Tables.Count; i++) {
                this.Tables[i].Dispose();
            }

            this.Tables.Clear();
            this.Tables = null;

            base.OnDispose();
        }

    }
}
