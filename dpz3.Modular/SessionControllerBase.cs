using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular {

    /// <summary>
    /// 带交互管理的Jttp管理器
    /// </summary>
    public abstract class SessionControllerBase : ControllerBase {

        /// <summary>
        /// 交互标识名称定义
        /// </summary>
        public const string Session_Id = "Mdr-Session-Id";

        /// <summary>
        /// 获取交互信息管理器
        /// </summary>
        public ISessionManager Session { get { return base.Host.Session; } }

        /// <summary>
        /// 获取交互信息管理器
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// 重载初始化事件
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        protected override string OnInitialize(IHost host) {

            // 执行基类函数并判断返回值
            string res = base.OnInitialize(host);
            if (!res.IsNoneOrNull()) return res;

            // 读取交互标识
            string sid = "";
            var cookies = Context.Request.Cookies;
            // 从Cookie中读取交互标识
            if (cookies.ContainsKey(Session_Id)) {
                sid = cookies[Session_Id];
            }
            this.SessionID = sid;

            return null;
        }

        /// <summary>
        /// 重载呈现事件
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override Task OnRender(IResult result) {
            // 向头部中添加交互标识
            base.SetResponseCookie(Session_Id, this.SessionID);
            return base.OnRender(result);
        }

    }
}
