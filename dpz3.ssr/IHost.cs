using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.ssr {

    /// <summary>
    /// 事件宿主接口
    /// </summary>
    public interface IHost {

        void OnRecieve(HostRecieveEventArgs e);

    }
}
