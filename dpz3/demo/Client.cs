using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo {
    public class Client : dpz3.Net.TcpClient {

        // 数据接收线程
        System.Threading.Thread _recieveThread;

        // 接收数据线程
        private void RecieveThread() {
            // 定义缓冲数据
            var buffer = new byte[1024 * 4];
            int res = 0;
            do {
                res = this.Receive(buffer, 0, buffer.Length);
                string txt = System.Text.Encoding.UTF8.GetString(buffer, 0, res);
                Console.WriteLine($"[{dpz3.Time.Now.ToTimeString()}] Client Recieve : {txt}");
            } while (res > 0);
        }

        public Client(string host, int port) : base(host, port) {
            this.OnConnect(() => {
                // 建立一个处理接收数据的线程
                _recieveThread = new System.Threading.Thread(RecieveThread);
                _recieveThread.Start();
            });
            this.OnDisconnect(() => {
                try { if (_recieveThread != null) _recieveThread.Abort(); } catch { }
                _recieveThread = null;
            });
        }

    }
}
