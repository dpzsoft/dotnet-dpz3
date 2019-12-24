using System;

namespace dpz3.Jttp {

    /// <summary>
    /// Jttp对象
    /// </summary>
    public class Object : dpz3.Object {

        // 内置对象
        private dpz3.Json.JsonUnit _obj;
        private Header _header = null;
        private Error _err = null;
        private dpz3.Json.JsonObject _data = null;
        private dpz3.Json.JsonArray _list = null;

        /// <summary>
        /// 获取关联头部数据对象
        /// </summary>
        public Header Header {
            get {
                if (_header == null) _header = new Header(_obj.Object("Header"));
                return _header;
            }
        }

        /// <summary>
        /// 获取关联数据对象
        /// </summary>
        public dpz3.Json.JsonObject Data {
            get {
                if (_data == null) _data = _obj.Object("Data");
                return _data;
            }
        }

        /// <summary>
        /// 获取关联列表对象
        /// </summary>
        public dpz3.Json.JsonArray List {
            get {
                if (_list == null) _list = _obj.Array("List");
                return _list;
            }
        }

        /// <summary>
        /// 获取关联错误对象
        /// </summary>
        public Error Error {
            get {
                if (_err == null) _err = new Error(_obj.Object("Error"));
                return _err;
            }
        }

        /// <summary>
        /// 获取或设置消息信息
        /// </summary>
        public string Message {
            get {
                return _obj.String("Message");
            }
            set {
                _obj.String("Message", value);
            }
        }

        /// <summary>
        /// 获取或设置文本信息
        /// </summary>
        public string Text {
            get {
                return _obj.String("Text");
            }
            set {
                _obj.String("Text", value);
            }
        }

        /// <summary>
        /// 获取或设置返回状态
        /// </summary>
        public int Result {
            get {
                return (int)_obj.Number("Result");
            }
            set {
                _obj.Number("Result", value);
            }
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="json"></param>
        public Object(string json = null) {
            if (json.IsNoneOrNull()) {
                _obj = new dpz3.Json.JsonObject();
            } else {
                _obj = (dpz3.Json.JsonObject)dpz3.Json.Parser.ParseJson(json);
            }
        }

        /// <summary>
        /// 加签运算
        /// </summary>
        public void SignUp(string key, string attach = null) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // 生成全新的验证盐
            string salt = Guid.NewGuid().ToString().Replace("-", "");
            this.Header.VerifySalt = salt;

            // 获取加签方式,默认为md5
            string sType = this.Header.VerifyType.ToLower();
            if (sType.IsNoneOrNull()) {
                sType = "md5";
                this.Header.VerifyType = "md5";
            }

            // 计算签名
            this.Header.VerifySign = Signer.SignUp(sType, this.Header.SessionID, this.Header.Time, salt, key, attach);
        }

        /// <summary>
        /// 获取字符串表示形势
        /// </summary>
        /// <returns></returns>
        protected override string OnParseString() {
            return _obj.ToJsonString();
        }

    }
}
