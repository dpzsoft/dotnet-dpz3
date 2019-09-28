using System;

namespace dpz3.Jttp {

    /// <summary>
    /// Jttp对象
    /// </summary>
    public class Object : dpz3.Object {

        // 内置对象
        private dpz3.Json.JsonUnit jObj;
        private dpz3.Json.JsonObject jData = null;
        private Error jError = null;

        /// <summary>
        /// 获取关联头部数据对象
        /// </summary>
        public Header Header { get; private set; }

        /// <summary>
        /// 获取关联数据对象
        /// </summary>
        public dpz3.Json.JsonObject Data {
            get {
                if (jData == null) {
                    jData = jObj.Object("Data");
                }
                return jData;
            }
        }

        /// <summary>
        /// 获取关联错误对象
        /// </summary>
        public Error Error {
            get {
                if (jError == null) {
                    jError = new Error();
                    var err = jObj.Object("Error");
                    jError.Code = (int)err.Number("Code").Value;
                    jError.Info = err.String("Info").Value;
                }
                return jError;
            }
        }

        /// <summary>
        /// 获取或设置消息信息
        /// </summary>
        public string Message {
            get {
                if (jObj["Message"] != null) {
                    return jObj["Message"].GetString();
                }
                return "";
            }
            set {
                jObj.String("Message").Value = value;
            }
        }

        /// <summary>
        /// 获取或设置文本信息
        /// </summary>
        public string Text {
            get {
                if (jObj["Text"] != null) {
                    return jObj["Text"].GetString();
                }
                return "";
            }
            set {
                jObj.String("Text").Value = value;
            }
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="json"></param>
        public Object(string json = null) {
            if (json.IsNoneOrNull()) {
                jObj = new dpz3.Json.JsonObject();
            } else {
                jObj = (dpz3.Json.JsonObject)dpz3.Json.Parser.ParseJson(json);
            }

            // 读取头部信息
            var header = jObj.Object("Header");
            this.Header = new Header();
            this.Header.Ver = header.String("Ver").Value;
            this.Header.Type = header.String("Type").Value;
            this.Header.SessionID = header.String("SessionID").Value;
            this.Header.Time = (long)header.Number("Time").Value;
            this.Header.Status = (int)header.Number("Status").Value;

            if (this.Header.Ver.IsNull()) this.Header.Ver = "1.0";
            if (this.Header.Type.IsNull()) this.Header.Type = "";
            if (this.Header.SessionID.IsNull()) this.Header.SessionID = "";

        }

        /// <summary>
        /// 获取字符串表示形势
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {

            // 设置头部信息
            var header = jObj.Object("Header");
            header["Ver"].SetValue(this.Header.Ver);
            header["Type"].SetValue(this.Header.Type);
            header["SessionID"].SetValue(this.Header.SessionID);
            header["Time"].SetValue(this.Header.Time);
            header["Status"].SetValue(this.Header.Status);

            // 设置错误信息
            if (jError != null) {
                var err = jObj.Object("Error");
                err["Code"].SetValue(jError.Code);
                err["Info"].SetValue(jError.Info);
            }

            return jObj.ToJsonString();
        }

    }
}
