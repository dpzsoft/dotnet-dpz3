using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.XOrm {

    /// <summary>
    /// 独立表定义
    /// </summary>
    public class StandaloneTable : dpz3.Object {

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取字段定义集合
        /// </summary>s
        public List<TableField> Fields { get; private set; }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="node"></param>
        public void FillData(dpz3.Xml.XmlNode node) {
            this.Name = node.Attr["name"];
            this.Title = node.Attr["title"];
            this.Version = node.Attr["version"];
            this.Description = node.Attr["description"];
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="xml"></param>
        public StandaloneTable(string xml = null) {
            this.Fields = new List<TableField>();
            if (!xml.IsNoneOrNull()) {
                using (var doc = dpz3.Xml.Parser.GetDocument(xml)) {

                    // 读取数据库设置
                    var table = doc["table"];
                    this.FillData(table);

                    // 读取平台信息
                    var fields = table.GetNodesByTagName("field", false);
                    foreach (var field in fields) {

                        // 添加平台信息
                        TableField tableField = new TableField();
                        this.Fields.Add(tableField);

                        // 设置平台信息
                        tableField.FillData(field);

                    }
                }
            }
        }

        /// <summary>
        /// 从网络加载设置
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Table LoadFromUrl(string url) {
            var xml = dpz3.Net.HttpClient.Get(url);
            return new Table(xml);
        }

        /// <summary>
        /// 从文件加载设置
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static Table LoadFromFile(string xmlPath) {
            var xml = dpz3.File.UTF8File.ReadAllText(xmlPath);
            return new Table(xml);
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="path"></param>
        public void SaveToFile(string path) {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append(this.ToString());
            dpz3.File.UTF8File.WriteAllText(path, sb.ToString());
        }

        /// <summary>
        /// 获取或快速创建一个字段配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TableField this[string name] {
            get {
                // 从已有的平台配置中查找
                for (int i = 0; i < this.Fields.Count; i++) {
                    if (this.Fields[i].Name == name) {
                        return this.Fields[i];
                    }
                }
                // 添加一个新的平台配置
                TableField field = new TableField();
                field.Name = name;
                this.Fields.Add(field);
                return field;
            }
        }

        /// <summary>
        /// 获取标准字符串表示形式
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table name=\"{0}\" version=\"{1}\" title=\"{2}\" description=\"{3}\">", this.Name, this.Version, this.Title, this.Description);
            for (int i = 0; i < this.Fields.Count; i++) {
                sb.Append(this.Fields[i].ToString());
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnDispose() {

            this.Name = null;
            this.Title = null;

            for (int i = 0; i < this.Fields.Count; i++) {
                this.Fields[i].Dispose();
            }

            this.Fields.Clear();
            this.Fields = null;

            base.OnDispose();
        }

    }
}
