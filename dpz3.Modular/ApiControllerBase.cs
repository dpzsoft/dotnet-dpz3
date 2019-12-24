using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular {

    /// <summary>
    /// API控制器基类
    /// </summary>
    public abstract class ApiControllerBase {

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        protected virtual string OnInitialize(IHost host) {
            this.Context = host.Context;
            this.Request = host.Context.Request;
            this.Response = host.Context.Response;
            this.Host = host;
            return null;
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        protected virtual string OnInited() { return null; }

        /// <summary>
        /// 可重载的呈现时间
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnRender(IResult result) { return null; }

        /// <summary>
        /// 获取模块化宿主
        /// </summary>
        protected IHost Host { get; set; }

        /// <summary>
        /// 获取超文本上下文
        /// </summary>
        protected HttpContext Context { get; set; }

        /// <summary>
        /// 获取请求器
        /// </summary>
        protected HttpRequest Request { get; private set; }

        /// <summary>
        /// 获取响应器
        /// </summary>
        protected HttpResponse Response { get; private set; }

        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void SetResponseHeader(string key, string value) {
            if (Response.Headers.ContainsKey(key)) {
                Response.Headers[key] = value;
            } else {
                Response.Headers.Add(key, value);
            }
        }

        /// <summary>
        /// 返回一个文本内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected Result.Text Text(string content) {
            return new Result.Text() { Content = content };
        }

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <returns></returns>
        public string Initialize(IHost host) {
            string res = this.OnInitialize(host);
            if (!res.IsNoneOrNull()) return res;
            return this.OnInited();
        }

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <returns></returns>
        public Task Render(IResult result) {
            Task task = this.OnRender(result);
            if (task != null) return task;
            return result.Render(this.Context);
        }

    }
}
