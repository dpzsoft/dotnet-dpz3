using System;

namespace dpz3.User {

    /// <summary>
    /// 配置文件
    /// </summary>
    public class Manager : dpz3.Object {

        // 用户字符限制
        private const string User_Keys = "0123456789qwertyuiopasdfghjklzxcvbnm_@.";

        // 文件夹
        private string _dir;
        private string _conf_path;
        private dpz3.File.ConfFile _conf;
        private dpz3.File.Conf.SettingGroup _group;
        private long idxer;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="dir"></param>
        public Manager(string dir) {
            _dir = dir.Replace("\\", "/");
            if (!_dir.EndsWith("/")) _dir += "/";
            _conf_path = $"{_dir}list.config";
            _conf = new File.ConfFile(_conf_path);
            _group = _conf["default"];
            idxer = _group["indexer"].ToLong();
        }

        /// <summary>
        /// 获取用户是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ExistUser(string user) {
            user = user.ToLower();
            string path = $"{_dir}{user}.cfg";
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// 添加一个用户配置
        /// </summary>
        /// <param name="user"></param>
        public ConfigFile AddUser(string user) {

            // 验证用户字符
            user = user.ToLower();
            for (int i = 0; i < user.Length; i++) {
                if (User_Keys.IndexOf(user[i]) < 0) {
                    throw new Exception("用户中包含限制字符");
                }
            }

            // 检验用户是否存在
            string path = $"{_dir}{user}.cfg";
            if (System.IO.File.Exists(path)) throw new Exception("用户已存在");

            // 索引器增加
            idxer++;
            long id = dpz3.Time.Now.ToTimeStamp() + idxer;

            // 配置文件
            var cfg = new ConfigFile(path);
            cfg["id"] = $"{id}";
            cfg["user"] = user;
            cfg.Save();

            return cfg;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="user"></param>
        public ConfigFile GetUser(string user) {

            // 检验用户是否存在
            user = user.ToLower();
            string path = $"{_dir}{user}.cfg";
            if (!System.IO.File.Exists(path)) return null;

            // 配置文件
            var cfg = new ConfigFile(path);
            return cfg;
        }
    }

}
