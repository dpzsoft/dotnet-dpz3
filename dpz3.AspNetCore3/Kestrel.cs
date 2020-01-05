using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace dpz3.AspNetCore {

    /// <summary>
    /// Kestrel操作器
    /// </summary>
    public static class Kestrel {

        private static bool CheckEnable(string config) {
            if (config.IsNoneOrNull()) return false;
            config = config.ToLower();
            return (config == "yes" || config == "true");
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="path"></param>
        /// <param name="webBuilder"></param>
        public static void DeployConfig(string path, IWebHostBuilder webBuilder) {

            // 当文件不存在时，执行初始化创建
            if (!System.IO.File.Exists(path)) {
                using (dpz3.File.ConfFile file = new dpz3.File.ConfFile(path)) {

                    // 建立服务配置
                    var serverGroup = file["Server"];
                    serverGroup["Enable"] = "no";

                    // 建立HTTP配置
                    var httpGroup = file["HTTP"];
                    httpGroup["Enable"] = "no";
                    httpGroup["Port"] = "80";

                    // 建立HTTPS配置
                    var httpsGroup = file["HTTPS"];
                    httpsGroup["Enable"] = "no";
                    httpsGroup["Port"] = "443";
                    httpsGroup["Pfx.Path"] = "/ssl/ssl.pfx";
                    httpsGroup["Pfx.Password"] = "123456";

                    //文件保存
                    file.Save();
                }
            }

            // 读取配置
            using (dpz3.File.ConfFile file = new dpz3.File.ConfFile(path)) {
                // 读取服务配置
                var serverGroup = file["Server"];
                if (CheckEnable(serverGroup["Enable"])) {
                    webBuilder.ConfigureKestrel(options => {
                        // 读取HTTP配置
                        var httpGroup = file["HTTP"];
                        if (CheckEnable(httpGroup["Enable"])) {
                            // 填入配置中的监听端口
                            options.Listen(IPAddress.Any, httpGroup["Port"].ToInteger());
                        }
                        // 读取HTTPS配置
                        var httpsGroup = file["HTTPS"];
                        if (CheckEnable(httpsGroup["Enable"])) {
                            // 填入配置中的监听端口
                            options.Listen(IPAddress.Any, httpsGroup["Port"].ToInteger(), listenOptions => {
                                // 填入配置中的pfx文件路径和指定的密码
                                listenOptions.UseHttps(httpsGroup["Pfx.Path"], httpsGroup["Pfx.Password"]);
                            });
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="webBuilder"></param>
        /// <param name="config"></param>
        public static void Deploy(IWebHostBuilder webBuilder, KestrelConfig config) {

            // 判断是否启用Kestrel服务
            if (config.Enable) {
                webBuilder.ConfigureKestrel(options => {

                    // 判断是否启用HTTP配置
                    if (config.HttpEnable) {
                        // 填入配置中的监听端口
                        options.Listen(IPAddress.Any, config.HttpPort);
                    }

                    // 判断是否启用HTTPS配置
                    if (config.HttpsEnable) {
                        // 填入配置中的监听端口
                        options.Listen(IPAddress.Any, config.HttpsPort, listenOptions => {
                            // 填入配置中的pfx文件路径和指定的密码
                            listenOptions.UseHttps(config.HttpsPfxPath, config.HttpsPfxPwd);
                        });
                    }

                });
            }
        }

    }
}
