using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Jttp {

    /// <summary>
    /// 错误信息定义
    /// </summary>
    public class Error {

        // 头部对象
        private dpz3.Json.JsonObject _obj;

        /// <summary>
        /// 获取或设置错误代码
        /// </summary>
        public int Code { get { return (int)_obj.Number("Code"); } set { _obj.Number("Code", value); } }

        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        public string Info { get { return _obj.String("Info"); } set { _obj.String("Info", value); } }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="obj"></param>
        public Error(dpz3.Json.JsonObject obj) {
            _obj = obj;
        }

    }
}
