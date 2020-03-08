using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular.Result {

    /// <summary>
    /// 文本类型
    /// </summary>
    public class File : IResult {

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 呈现内容
        /// </summary>
        /// <param name="context"></param>
        public Task Render(HttpContext context) {
            return context.Response.SendFileAsync(Path);
        }
    }
}
