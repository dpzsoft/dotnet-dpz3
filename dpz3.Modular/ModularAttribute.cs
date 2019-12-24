using System;

namespace dpz3.Modular {

    /// <summary>
    /// 模块化定义类型
    /// </summary>
    public enum ModularTypes {
        /// <summary>
        /// 简单类
        /// </summary>
        Api = 0x01,
        /// <summary>
        /// 带Session类的API类
        /// </summary>
        SessionApi = 0x02,
        /// <summary>
        /// Get方式访问的函数
        /// </summary>
        Get = 0x11,
        /// <summary>
        /// Post方式访问的函数
        /// </summary>
        Post = 0x012,
    }

    /// <summary>
    /// 模块化定义
    /// </summary>
    public class ModularAttribute : Attribute {

        /// <summary>
        /// 获取模块类型
        /// </summary>
        public ModularTypes ModularType { get; private set; }

        /// <summary>
        /// 获取路由地址
        /// </summary>
        public string Route { get; private set; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="modularType"></param>
        /// <param name="route"></param>
        public ModularAttribute(ModularTypes modularType, string route) {
            this.ModularType = modularType;
            this.Route = route;
        }


    }
}
