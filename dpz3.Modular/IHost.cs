using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Modular {

    /// <summary>
    /// 模块化宿主程序接口
    /// </summary>
    public interface IHost {

        /// <summary>
        /// 获取超文本上下文
        /// </summary>
        HttpContext Context { get; }

        /// <summary>
        /// 获取交互管理器
        /// </summary>
        ISessionManager Session { get; }

        /// <summary>
        /// 获取数据库连接管理器
        /// </summary>
        dpz3.db.Connection Connection { get; }

    }
}
