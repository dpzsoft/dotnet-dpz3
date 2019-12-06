﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Json {

    /// <summary>
    /// 数值对象操作器
    /// </summary>
    public class JsonFloatOperator {

        private JsonUnit _json;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="jsonUnit"></param>
        public JsonFloatOperator(JsonUnit jsonUnit) {
            _json = jsonUnit;
        }

        /// <summary>
        /// 获取或设置数值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float this[int index] {
            get {
                var obj = _json[index];
                if (obj != null) return (float)obj.GetNumber();
                return 0;
            }
            set {
                _json.Number(index, value);
            }
        }

        /// <summary>
        /// 获取或设置数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float this[string key] {
            get {
                var obj = _json[key];
                if (obj != null) return (float)obj.GetNumber();
                return 0;
            }
            set {
                _json.Number(key, value);
            }
        }

    }
}