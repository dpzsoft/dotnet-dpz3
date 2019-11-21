using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace dpz3.AspNetCore3 {

    /// <summary>
    /// Kestrel操作器
    /// </summary>
    public static class Kestrel {

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        public static void DeployConfig(string path, KestrelServerOptions options) {

            // 当文件不存在时，执行初始化创建
            if (!System.IO.File.Exists(path)) {
                using (dpz3.File.ConfFile file = new dpz3.File.ConfFile(path)) {

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

                // 读取HTTP配置
                var httpGroup = file["HTTP"];
                if (httpGroup["Enable"] == "yes") {
                    // 填入配置中的监听端口
                    
                    options.Listen(IPAddress.Any, httpGroup["Port"].ToInteger());
                }

                // 读取HTTPS配置
                var httpsGroup = file["HTTPS"];
                if (httpsGroup["Enable"] == "yes") {
                    // 填入配置中的监听端口
                    options.Listen(IPAddress.Any, httpsGroup["Port"].ToInteger(), listenOptions => {
                        // 填入配置中的pfx文件路径和指定的密码
                        listenOptions.UseHttps(httpsGroup["Pfx.Path"], httpsGroup["Pfx.Password"]);
                    });
                }
            }
        }

    }
}
