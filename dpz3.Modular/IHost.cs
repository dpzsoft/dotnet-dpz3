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

        /// <summary>
        /// 宿主版本
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 工作目录
        /// </summary>
        string WorkFolder { get; }

        /// <summary>
        /// 文件存储目录
        /// </summary>
        string StorageFolder { get; }

        /// <summary>
        /// 包名称
        /// </summary>
        string PackageName { get; }

        /// <summary>
        /// 包版本
        /// </summary>
        string PackageVersion { get; }

        /// <summary>
        /// 包工作目录
        /// </summary>
        string PackageWorkFolder { get; }

        /// <summary>
        /// 调试输出
        /// </summary>
        /// <param name="content"></param>
        void Debug(string content);

        /// <summary>
        /// 调试输出
        /// </summary>
        /// <param name="content"></param>
        void DebugLine(string content = null);

    }
}
