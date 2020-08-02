using dpz3.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace demo {
    public class ServerEntity : dpz3.Net.TcpServerEntity {

        // 数据接收线程
        System.Threading.Thread _recieveThread;

        // 接收数据线程
        private void RecieveThread() {
            // 定义缓冲数据
            var buffer = new byte[1024 * 4];
            int res = 0;
            do {
                res = this.Receive(buffer, 0, buffer.Length);
                this.Send(buffer, 0, res);
            } while (res > 0);
        }

        public ServerEntity(TcpServer server, Socket socket) : base(server, socket) {
            base.OnDisconnect(() => {
                try {
                    if (_recieveThread != null) {
                        _recieveThread.Abort();
                    }
                } catch { }
                _recieveThread = null;
            });
            this.OnFree(() => { this.Send(new byte[] { 49 }); });
            this.FreeSecond = 10;
            // 建立一个处理接收数据的线程
            _recieveThread = new System.Threading.Thread(RecieveThread);
            _recieveThread.Start();
        }

    }
}
