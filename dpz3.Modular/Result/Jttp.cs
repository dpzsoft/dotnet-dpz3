using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular.Result {

    /// <summary>
    /// Jttp类型
    /// </summary>
    public class Jttp : IResult {

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public dpz3.Jttp.Object Content { get; set; }

        /// <summary>
        /// 呈现内容
        /// </summary>
        /// <param name="context"></param>
        public Task Render(HttpContext context) {
            return context.Response.WriteAsync(this.Content.ToString());
        }
    }
}
