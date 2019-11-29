using System;
using dpz3.ssr;
using System.Collections.Generic;

namespace dpz3.ssr {

    /// <summary>
    /// 交互信息管理器
    /// </summary>
    public class SessionClient : IDisposable {

        /// <summary>
        /// 专用的事件宿主
        /// </summary>
        private class Host : IHost {

            // 设置结果标志
            private string _sign = null;

            public void OnRecieve(HostRecieveEventArgs e) {
                // 此demo进行了一次简单的定长数据获取示例
                ClientHostRecieveEventArgs args = (ClientHostRecieveEventArgs)e;
                if (args.Client.DataMode) {
                    // 数据模式
                    args.ResultData = _sign + e.Content;
                    args.Result = HostEventResults.Finished;
                } else {
                    // 命令模式
                    // 此处以+开头定义成功的数据
                    // 此处以-开头定义失败的数据
                    if (e.Content.StartsWith("+") || e.Content.StartsWith("-")) {
                        _sign = e.Content.Substring(0, 1);

                        // 获取内容长度
                        int len = int.Parse(e.Content.Substring(1));

                        // 当长度为0时直接返回，否则设置数据模式
                        if (len <= 0) {
                            args.ResultData = _sign;
                            args.Result = HostEventResults.Finished;
                        } else {
                            args.Client.SetDataMode(len);
                        }

                    }
                }
            }
        }

        // 通讯核心
        private Client _client;

        // 本地缓存
        private Dictionary<string, string> _cache;

        /// <summary>
        /// 获取交互标识
        /// </summary>
        public string SessionID { get; private set; }

        /// <summary>
        /// 实例化一个交互连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="pwd"></param>
        public SessionClient(string ip, int port, string pwd) {
            _client = new Client(new Host(), ip, port);
            _cache = new Dictionary<string, string>();

            // 执行登录
            _client.Sendln("PWD");
            _client.Sendln($"${pwd.Length}");
            _client.Send(pwd, (string data) => {
                // 判断是否成功
                if (data.StartsWith("-")) {
                    throw new Exception(data.Substring(1));
                }
            });
        }

        /// <summary>
        /// 设置交互标识
        /// </summary>
        /// <param name="sid"></param>
        public bool SetSessionID(string sid) {
            bool res = false;

            // 发送设置指令
            _client.Sendln("SID");
            _client.Sendln($"@{sid.Length}");
            _client.Send(sid, (string data) => {
                // 判断是否成功
                if (data.StartsWith("+")) {
                    res = true;
                    this.SessionID = data.Substring(1);
                }
            });
            return res;
        }

        /// <summary>
        /// 创建一个新的交互标识
        /// </summary>
        public void CreateNewSessionID() {

            // 发送创建指令
            _client.Sendln("SID");
            _client.Sendln("@0", (string data) => {
                // 判断是否成功
                if (data.StartsWith("+")) {
                    this.SessionID = data.Substring(1);
                } else {
                    // 失败时抛出一个错误
                    throw new Exception(data.Substring(1));
                }
            });
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            _cache.Clear();
            _client.Dispose();
            _client = null;
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key] {
            get {

                // 如果缓存中包含当前键，则直接返回缓存
                if (_cache.ContainsKey(key)) return _cache[key];

                string res = "";

                // 发送设置指令
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(key);
                _client.Sendln("GET");
                _client.Sendln($"${bs.Length}");
                _client.Send(bs, (string data) => {
                    // 判断是否成功
                    if (data.StartsWith("+")) {
                        res = data.Substring(1);
                    } else {
                        // 抛出错误
                        throw new Exception(data.Substring(1));
                    }
                });

                // 进行内容缓存
                _cache.Add(key, res);

                return res;
            }
            set {

                // 发送设置指令
                byte[] bsKey = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] bsValue = System.Text.Encoding.UTF8.GetBytes(value);
                _client.Sendln("SET");
                _client.Sendln($"${bsKey.Length}");
                _client.Send(bsKey);
                _client.Sendln($"&{bsValue.Length}");
                _client.Send(bsValue, (string data) => {
                    // 判断是否成功
                    if (data.StartsWith("+")) {
                        // 处理缓存
                        if (_cache.ContainsKey(key)) {
                            // 修改缓存
                            _cache[key] = value;
                        } else {
                            // 添加缓存
                            _cache.Add(key, value);
                        }
                    } else {
                        // 抛出错误
                        throw new Exception(data.Substring(1));
                    }
                });

            }
        }

    }
}
