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

            // 添加加签类型
            sb.AppendFormat("$type={0}", sType);

            // 添加盐
            sb.AppendFormat("$salt={0}", salt);

            // 添加时间戳
            sb.AppendFormat("$time={0}", this.Header.Time);

            // 添加交互标识
            sb.AppendFormat("$session={0}", this.Header.SessionID);

            // 添加附加信息
            if (!attach.IsNoneOrNull()) sb.AppendFormat("$attach={0}", attach);

            // 添加加签密钥
            sb.AppendFormat("$key={0}", key);

            switch (sType) {
                case "md5": this.Header.VerifySign = sb.ToString().GetMD5(); break;
                case "sha1": this.Header.VerifySign = sb.ToString().GetSha1(); break;
                case "sha256": this.Header.VerifySign = sb.ToString().GetSha256(); break;
                case "sha512": this.Header.VerifySign = sb.ToString().GetSha512(); break;
                default: throw new Exception("不支持的加签算法");
            }

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
