using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.User {

    /// <summary>
    /// 密码相关
    /// </summary>
    public static class Password {

        /// <summary>
        /// MD5加密定义字符串
        /// </summary>
        public const string MD5 = "md5";

        /// <summary>
        /// sha1加密定义字符串
        /// </summary>
        public const string SHA1 = "sha1";

        /// <summary>
        /// sha256加密定义字符串
        /// </summary>
        public const string SHA256 = "sha256";


        /// <summary>
        /// sha512加密定义字符串
        /// </summary>
        public const string SHA512 = "sha512";


        /// <summary>
        /// 获取对应的加密密码
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPasswod(string tp, string pwd, string key = null) {
            switch (tp.ToLower()) {
                case MD5:
                    return GetMD5Passwod(pwd, key);
                case SHA1:
                    return GetSha1Passwod(pwd, key);
                case SHA256:
                    return GetSha256Passwod(pwd, key);
                case SHA512:
                    return GetSha512Passwod(pwd, key);
                default:
                    throw new Exception($"尚未支持\"{tp}\"加密方式");
            }
        }

        /// <summary>
        /// 获取一个MD5加密的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetMD5Passwod(string pwd, string key = null) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("${0}",MD5);
            if (!key.IsNoneOrNull()) sb.AppendFormat("${0}", key);
            if (!pwd.IsNoneOrNull()) sb.AppendFormat("${0}", pwd);
            return sb.ToString().GetMD5();
        }

        /// <summary>
        /// 获取一个MD5加密的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSha1Passwod(string pwd, string key = null) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("${0}", SHA1);
            if (!key.IsNoneOrNull()) sb.AppendFormat("${0}", key);
            if (!pwd.IsNoneOrNull()) sb.AppendFormat("${0}", pwd);
            return sb.ToString().GetSha1();
        }

        /// <summary>
        /// 获取一个MD5加密的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSha256Passwod(string pwd, string key = null) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("${0}", SHA256);
            if (!key.IsNoneOrNull()) sb.AppendFormat("${0}", key);
            if (!pwd.IsNoneOrNull()) sb.AppendFormat("${0}", pwd);
            return sb.ToString().GetSha256();
        }

        /// <summary>
        /// 获取一个MD5加密的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSha512Passwod(string pwd, string key = null) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("${0}", SHA512);
            if (!key.IsNoneOrNull()) sb.AppendFormat("${0}", key);
            if (!pwd.IsNoneOrNull()) sb.AppendFormat("${0}", pwd);
            return sb.ToString().GetSha512();
        }

    }
}
