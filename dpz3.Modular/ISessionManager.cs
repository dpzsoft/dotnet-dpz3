﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Modular {

    /// <summary>
    /// 交互信息管理器接口
    /// </summary>
    public interface ISessionManager {

        /// <summary>
        /// 获取存储值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 设置存储值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, string value);

    }
}
