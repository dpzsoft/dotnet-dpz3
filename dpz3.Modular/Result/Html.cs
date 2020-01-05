using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular.Result {

    /// <summary>
    /// 文本类型
    /// </summary>
    public class Html : IResult {

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 呈现内容
        /// </summary>
        /// <param name="context"></param>
        public Task Render(HttpContext context) {
            context.Response.ContentType = "text/html;charset=utf-8";
            return context.Response.WriteAsync(this.Content);
        }
    }
}
