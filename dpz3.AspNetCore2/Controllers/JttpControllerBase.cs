using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.AspNetCore.Controllers {

    /// <summary>
    /// 基于Jttp协议的基础控制器
    /// </summary>
    public abstract class JttpControllerBase : ControllerBase {

        /// <summary>
        /// 输出对象
        /// </summary>
        protected dpz3.Jttp.Object JResponse { get; private set; }

        /// <summary>
        /// 输出用的头部对象
        /// </summary>
        protected dpz3.Jttp.Header JHeader { get { return JResponse.Header; } }

        /// <summary>
        /// 输出用的数据对象
        /// </summary>
        protected dpz3.Json.JsonObject JData { get { return JResponse.Data; } }

        /// <summary>
        /// 获取客户端提交的表单信息
        /// </summary>
        protected dpz3.KeyValues<string> Form { get; private set; }

        /// <summary>
        /// 获取客户端提交的表单信息
        /// </summary>
        /// <returns></returns>
        protected dpz3.KeyValues<string> GetForm() {
            ICollection<string> keys = Request.Form.Keys;
            foreach (string key in keys) {
                this.Form[key] = Request.Form[key];
            }
            return this.Form;
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public JttpControllerBase() {

            // 建立Jttp应答器
            this.JResponse = new Jttp.Object();

            // 建立
            this.Form = new KeyValues<string>();
        }

        /// <summary>
        /// 将行数据填充到Json对象中
        /// </summary>
        /// <param name="row"></param>
        /// <param name="obj"></param>
        protected void FillRowToJsonObject(dpz3.db.Row row, dpz3.Json.JsonObject obj) {
            foreach (var item in row) {
                obj.String(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 将行集合填充到Json对象中
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="array"></param>
        protected void FillRowsToJsonArray(dpz3.db.Rows rows, dpz3.Json.JsonArray array) {
            foreach (var row in rows) {
                FillRowToJsonObject(row, array.Object(array.Count));
            }
        }

        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <returns></returns>
        protected string Success(string msg = null) {
            JHeader.Status = 1;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
            return JResponse.ToString();
        }

        /// <summary>
        /// 返回失败数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected string Fail(string msg = null) {
            JHeader.Status = 0;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
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
            JHeader.Status = -1;
            if (!msg.IsNoneOrNull()) JResponse.Message = msg;
            JResponse.Error.Code = 0;
            if (info != null) JResponse.Error.Info = info;
            return JResponse.ToString();
        }

    }
}
