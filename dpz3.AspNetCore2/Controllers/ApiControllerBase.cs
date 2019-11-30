using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.AspNetCore.Controllers {

    /// <summary>
    /// WebApi基础类
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase {

        /// <summary>
        /// 输出对象
        /// </summary>
        protected dpz3.Jttp.Object JResponse { get; private set; }

        /// <summary>
        /// 获取格式化内容后的对象
        /// </summary>
        protected dpz3.Json.JsonObject JRequest { get; private set; }

        /// <summary>
        /// 获取文本内容
        /// </summary>
        protected string JRequestText { get; private set; }

        /// <summary>
        /// 可重载的初始化时间
        /// </summary>
        /// <returns></returns>
        protected virtual string OnInit() { return null; }

        /// <summary>
        /// 可重载的呈现时间
        /// </summary>
        /// <returns></returns>
        protected virtual void OnRender() { }

        /// <summary>
        /// 操作初始化
        /// </summary>
        /// <returns></returns>
        protected string Initialize() {

            var reader = new System.IO.StreamReader(Request.Body);
            string content = reader.ReadToEnd();
            this.JRequestText = content;

            // 解析获取到的数据
            this.JRequest = (dpz3.Json.JsonObject)Json.Parser.ParseJson(content);

            // 建立Jttp应答器
            this.JResponse = new Jttp.Object();

            // 返回初始化重载事件
            return this.OnInit();
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public ApiControllerBase() { }

        /// <summary>
        /// 将行数据填充到Json对象中
        /// </summary>
        /// <param name="row"></param>
        /// <param name="obj"></param>
        protected void RenderData(dpz3.db.Row row, dpz3.Json.JsonObject obj = null) {
            if (dpz3.Object.IsNull(obj)) obj = JResponse.Data;
            foreach (var item in row) {
                obj.String(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 将行集合填充到Json对象中
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="array"></param>
        protected void RenderList(dpz3.db.Rows rows, dpz3.Json.JsonArray array = null) {
            if (dpz3.Object.IsNull(array)) array = JResponse.List;
            foreach (var row in rows) {
                RenderData(row, array.Object(array.Count));
            }
        }

        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <returns></returns>
        protected string Success(string msg = null) {
            JResponse.Result = 1;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
            // 重载
            this.OnRender();
            return JResponse.ToString();
        }

        /// <summary>
        /// 返回失败数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected string Fail(string msg = null) {
            JResponse.Result = 0;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
            // 重载
            this.OnRender();
            return JResponse.ToString();
        }

        /// <summary>
        /// 返回错误数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected string Error(int code = 0, string msg = null, string info = null) {
            JResponse.Result = -1;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
            JResponse.Error.Code = 0;
            if (info != null) JResponse.Error.Info = info;
            // 重载
            this.OnRender();
            return JResponse.ToString();
        }

    }
}
