using System;

namespace demo {
    class Program {
        static void Main(string[] args) {

            // 测试http方式获取数据
            string http = dpz3.Net.HttpClient.Get("http://v3.lywos.com/");
            Console.WriteLine($"http:{http.Length}");

            // 测试https方式获取数据
            string https = dpz3.Net.HttpClient.Get("https://v5.lywos.com/");
            Console.WriteLine($"https:{https.Length}");

            int tick = Environment.TickCount;
            long lastLoad = 0;
            dpz3.Net.HttpClient.Download("https://jx3wscs-fullupdatehttps.dl.kingsoft.com/jx3v4_launcher/JX3Installer_1.0.0.522_Official.exe", @"X:\temp\JX3Installer_1.0.0.522_Official.exe", (long size, long loaded) => {
                int ts = Environment.TickCount - tick;
                tick = Environment.TickCount;

                float speed = (int)(((loaded - lastLoad) / (float)ts) * 1000 / 10.24) / (float)100;
                lastLoad = loaded;

                string speedStr = "";

                if (speed < 1024) {
                    speedStr = $"{speed}KB/s";
                } else {
                    float speedMb = (int)(speed / 10.24) / (float)100;
                    speedStr = $"{speedMb}MB/s";
                }

                Console.WriteLine($"{loaded}/{size} - {speedStr}");
            });
        }
    }
}
