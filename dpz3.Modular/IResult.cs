using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular {
    
    /// <summary>
    /// 模块化函数返回
    /// </summary>
    public interface IResult {

        /// <summary>
        /// 呈送结果
        /// </summary>
        /// <param name="context"></param>
        Task Render(HttpContext context);
    }
}
