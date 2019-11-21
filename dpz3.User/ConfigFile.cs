using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.User {

    /// <summary>
    /// 配置文件
    /// </summary>
    public class ConfigFile : dpz3.File.ConfFile {

        private dpz3.File.Conf.SettingGroup _def;
        private dpz3.File.Conf.SettingGroup _pwd;

        /// <summary>
        /// 获取或设置属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new string this[string key] {
            get { return _def[key]; }
            set { _def[key] = value; }
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="path"></param>
        public ConfigFile(string path) : base(path) {
            _def = base["default"];
            _pwd = base["password"];
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="pwd"></param>
        public bool CheckPassword(string pwd) {
            // 读取配置中的密码
            string tp = _pwd["type"];
            if (tp.IsNoneOrNull()) return false;
            string key = _pwd["key"];
            string passwd = _pwd[tp];
            if (passwd.IsNoneOrNull()) return false;
            // 计算当前密码的验证串
            string passwd2 = Password.GetPasswod(tp, pwd, key);
            return passwd.Equals(passwd2);
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="pwd"></param>
        public void SetPassword(string tp, string pwd) {
            string key = Guid.NewGuid().ToString();
            string passwd = Password.GetPasswod(tp, pwd, key);
            _pwd["type"] = tp;
            _pwd["key"] = key;
            _pwd[tp] = passwd;
        }

        /// <summary>
        /// 设置MD5密码
        /// </summary>
        /// <param name="pwd"></param>
        public void SetMD5Password(string pwd) {
            SetPassword(Password.MD5, pwd);
        }

        /// <summary>
        /// 设置sha1密码
        /// </summary>
        /// <param name="pwd"></param>
        public void SetSha1Password(string pwd) {
            SetPassword(Password.SHA1, pwd);
        }

        /// <summary>
        /// 设置sha256密码
        /// </summary>
        /// <param name="pwd"></param>
        public void SetSha256Password(string pwd) {
            SetPassword(Password.SHA256, pwd);
        }

        /// <summary>
        /// 设置sha512密码
        /// </summary>
        /// <param name="pwd"></param>
        public void SetSha512Password(string pwd) {
            SetPassword(Password.SHA512, pwd);
        }

    }
}
