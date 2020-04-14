using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Modular {

    /// <summary>
    /// Jttp控制器基类
    /// </summary>
    public abstract class JttpControllerBase : ControllerBase {

        /// <summary>
        /// 获取请求器
        /// </summary>
        protected new dpz3.Json.JsonObject Request { get; private set; }

        /// <summary>
        /// 获取响应器
        /// </summary>
        protected new dpz3.Jttp.Object Response { get; private set; }

        /// <summary>
        /// 获取请求文本内容
        /// </summary>
        protected string RequestContent { get; private set; }

        /// <summary>
        /// 返回一个数据行
        /// </summary>
        /// <param name="row"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected Result.Jttp RowSuccess(dpz3.db.Row row, string msg = null) {
            Response.Result = 1;
            if (!msg.IsNoneOrNull()) Response.Message = msg;
            foreach (var item in row) {
                Response.Data.String(item.Key, item.Value);
            }
            return new Result.Jttp() { Content = Response };
        }

        /// <summary>
        /// 返回一个数据行集合
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected Result.Jttp RowsSuccess(dpz3.db.Rows rows, string msg = null) {
            Response.Result = 1;
            if (!msg.IsNoneOrNull()) Response.Message = msg;
            var list = Response.List;
            foreach (var row in rows) {
                var obj = list.Obj[list.Count];
                foreach (var item in row) {
                    obj.String(item.Key, item.Value);
                }
            }
            return new Result.Jttp() { Content = Response };
        }

        /// <summary>
        /// 返回一个文本内容
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected Result.Jttp Success(string msg = null) {
            Response.Result = 1;
            if (!msg.IsNoneOrNull()) Response.Message = msg;
            return new Result.Jttp() { Content = Response };
        }

        /// <summary>
        /// 返回一个文本内容
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected Result.Jttp Fail(string msg = null) {
            Response.Result = 0;
            if (!msg.IsNoneOrNull()) Response.Message = msg;
            return new Result.Jttp() { Content = Response };
        }

        /// <summary>
        /// 返回一个文本内容
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected Result.Jttp Error(string msg = null, int code = 0, string info = null) {
            Response.Result = -1;
            if (!msg.IsNoneOrNull()) Response.Message = msg;
            Response.Error.Code = 0;
            if (!info.IsNoneOrNull()) Response.Error.Info = info;
            return new Result.Jttp() { Content = Response };
        }

        /// <summary>
        /// 重载初始化事件
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        protected override string OnInitialize(IHost host) {

            // 执行基类函数并判断返回值
            string res = base.OnInitialize(host);
            if (!res.IsNoneOrNull()) return res;

            // 读取上下文内容
            host.Context.Request.EnableBuffering();
            var reader = new System.IO.StreamReader(host.Context.Request.Body);
            string content = reader.ReadToEnd();
            this.RequestContent = content;

            // 解析获取到的数据
            this.Request = (dpz3.Json.JsonObject)Json.Parser.ParseJson(content);

            // 建立Jttp应答器
            this.Response = new Jttp.Object();

            return null;
        }

    }
}
