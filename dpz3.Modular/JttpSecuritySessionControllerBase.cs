using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dpz3.Modular {

    /// <summary>
    /// 带安全管理的Jttp管理器
    /// </summary>
    public abstract class JttpSecuritySessionControllerBase : JttpSessionControllerBase {

        /// <summary>
        /// 安全验证类型
        /// </summary>
        public const string Security_Type = "Mdr-Security-Type";

        /// <summary>
        /// 安全验证加盐
        /// </summary>
        public const string Security_Salt = "Mdr-Security-Salt";

        /// <summary>
        /// 安全验证时间戳
        /// </summary>
        public const string Security_Timestamp = "Mdr-Security-Timestamp";

        /// <summary>
        /// 安全验证签名
        /// </summary>
        public const string Security_Sign = "Mdr-Security-Sign";

        /// <summary>
        /// 获取或设置安全密钥
        /// </summary>
        protected string SecurityKey { get; set; }

        // 获取签名
        private string GetSign(string token, string salt, long timetamp, string key, string type = "md5", string attach = null) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // 添加加签类型
            sb.AppendFormat("$type={0}", type);

            // 添加盐
            sb.AppendFormat("$salt={0}", salt);

            // 添加时间戳
            sb.AppendFormat("$time={0}", timetamp);

            // 添加交互标识
            sb.AppendFormat("$token={0}", token);

            // 添加附加信息
            if (!attach.IsNoneOrNull()) sb.AppendFormat("$attach={0}", attach);

            // 添加加签密钥
            sb.AppendFormat("$key={0}", key);

            switch (type) {
                case "md5": return sb.ToString().GetMD5();
                case "sha1": return sb.ToString().GetSha1();
                case "sha256": return sb.ToString().GetSha256();
                case "sha512": return sb.ToString().GetSha512();
                default: throw new Exception("不支持的加签算法");
            }
        }

        /// <summary>
        /// 头部信息加签
        /// </summary>
        /// <param name="attach"></param>
        protected void SignUp(string attach = null) {
            // 输出头部信息
            string salt = Guid.NewGuid().ToString().Replace("-", "");
            long ts = dpz3.Time.Now.ToTimeStamp();
            base.SetResponseHeader(Security_Type, "md5");
            base.SetResponseHeader(Security_Salt, salt);
            base.SetResponseHeader(Security_Timestamp, $"{ts}");
            base.SetResponseHeader(Security_Sign, GetSign(this.SessionID, salt, ts, this.SecurityKey, "md5", attach));
        }

        /// <summary>
        /// 可重载的签名事件
        /// </summary>
        protected virtual bool OnSignUp() { return true; }

        /// <summary>
        /// 重载呈现事件
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override Task OnRender(IResult result) {
            // 呈现时进行加签
            if (this.OnSignUp()) this.SignUp();
            return base.OnRender(result);
        }

        /// <summary>
        /// 检查签名有效性
        /// </summary>
        /// <returns></returns>
        protected bool CheckSign(string attach = null) {

            // 获取加签类型
            string type = "";
            if (Context.Request.Headers.ContainsKey(Security_Type)) type = Context.Request.Headers[Security_Type].ToString();
            // 获取加签盐
            string salt = "";
            if (Context.Request.Headers.ContainsKey(Security_Salt)) salt = Context.Request.Headers[Security_Salt].ToString();
            // 获取验证时间戳
            long ts = 0;
            if (Context.Request.Headers.ContainsKey(Security_Timestamp)) ts = Context.Request.Headers[Security_Timestamp].ToString().ToLong();
            // 获取签名
            string sign = "";
            if (Context.Request.Headers.ContainsKey(Security_Sign)) sign = Context.Request.Headers[Security_Sign].ToString();

            // 计算签名
            string signNew = GetSign(this.SessionID, salt, ts, this.SecurityKey, type, attach);

            // 返回对比结果
            return signNew.Equals(sign);
        }

    }
}
