﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.File {

    /// <summary>
    /// Linux通用配置文件
    /// </summary>
    public class ConfFile : dpz3.Object {

        private string path = "";

        /// <summary>
        /// 获取全部
        /// </summary>
        public List<Conf.SettingGroup> Groups { get; private set; }

        /// <summary>
        /// Loads a configuration file.
        /// </summary>
        /// <param name="file">The filename where the configuration file can be found.</param>
        public ConfFile(string file = "") {
            this.Groups = new List<Conf.SettingGroup>();
            path = file;
            if (path != "") LoadFile();
        }

        //加载文件内容
        private void LoadFile() {
            string sz = dpz3.File.UTF8File.ReadAllText(path, false);
            string[] lines = sz.Replace("\r", "").Split('\n');
            Conf.SettingGroup group = null;

            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i].Trim();
                if (line == "") {
                    if (group == null) {
                        group = new Conf.SettingGroup() { Name = "" };
                        this.Groups.Add(group);
                    }
                    //去掉末尾空行
                    if (i != lines.Length - 1) group.AddEmptyLine();
                } else if (line.StartsWith("[") && line.EndsWith("]")) {
                    group = new Conf.SettingGroup() { Name = line.Substring(1, line.Length - 2) };
                    this.Groups.Add(group);
                } else {
                    if (group == null) {
                        group = new Conf.SettingGroup() { Name = "" };
                        this.Groups.Add(group);
                    }
                    if (line.StartsWith("#")) {
                        group.AddNote(line.Substring(1));
                    } else {
                        int idx = line.IndexOf("=");
                        if (idx < 0) {
                            group.Set(line);
                        } else {
                            group.Set(line.Substring(0, idx).Trim(), line.Substring(idx + 1).Trim());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取一个设置组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Conf.SettingGroup this[string name] {
            get {
                for (int i = 0; i < this.Groups.Count; i++) {
                    Conf.SettingGroup group = this.Groups[i];
                    if (group.Name == name) return group;
                }
                Conf.SettingGroup newGroup = new Conf.SettingGroup() { Name = name };
                this.Groups.Add(newGroup);
                return newGroup;
            }
        }

        /// <summary>
        /// Saves the configuration to a file
        /// </summary>
        /// <param name="filename">The filename for the saved configuration file.</param>
        public void Save(string filename = null) {
            if (filename == null) filename = path;
            if (filename == "") throw new Exception("未找到存储地址");

            string sz = "";
            foreach (var g in this.Groups) {
                sz += g.ToString();
            }
            dpz3.File.UTF8File.WriteAllText(filename, sz);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnDispose() {

            foreach (var g in this.Groups) {
                g.Dispose();
            }

            this.Groups.Clear();
            this.Groups = null;

            base.OnDispose();
        }
    }
}