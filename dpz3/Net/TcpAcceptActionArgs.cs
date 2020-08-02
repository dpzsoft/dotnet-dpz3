using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace dpz3.Net {

    /// <summary>
    /// TCP错误信息
    /// </summary>
    public class TcpAcceptActionArgs : dpz3.Object {

        /// <summary>
        /// 获取或设置信息
        /// </summary>
        public Socket Socket { get; set; }

    }
}
