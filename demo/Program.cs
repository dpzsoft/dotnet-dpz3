﻿using System;

namespace demo {
    class Program {
        static void Main(string[] args) {

            var arg = new dpz3.Console.Arguments(args);
            Console.WriteLine("path:" + arg.ContainsKey("t"));
            Console.WriteLine("path:" + arg.ContainsKey("n"));
            Console.WriteLine("path:" + arg.ContainsKey("path"));

            string xml = "<div class=\"dig-body\">";
            xml += "<div dpz-id=\"Tool\" class=\"dig-tools\">";
            xml += "<a href=\"javascript:;\" v-on:click=\"onSave\"><img v-bind:src=\"Images.Save\" />保存</a>";
            xml += "<a href=\"javascript:;\" v-on:click=\"onCancel\"><img v-bind:src=\"Images.Cancel\" />取消</a>";
            xml += "</div>";
            xml += "<div dpz-id=\"Form\" class=\"dig-form\">";
            xml += "<div class=\"dig-form-box\">";
            xml += "<xorm src=\"orm.xml\" type=\"form\"></xorm>";
            xml += "</div>";
            xml += "</div>";
            xml += "</div>";

            using (dpz3.Html.HtmlDocument doc = new dpz3.Html.HtmlDocument(xml)) {
                Console.WriteLine($"html:\r\n{doc.InnerHTML}");
            }

            using (dpz3.Xml.XmlDocument doc = new dpz3.Xml.XmlDocument(xml)) {
                Console.WriteLine($"xml:\r\n{doc.InnerXml}");
            }

            string urlCss = "https://v5.lywos.com/css/Shared/Home.css";
            string szCss = dpz3.Net.HttpClient.Get(urlCss);
            using (dpz3.Html.HtmlCss css = new dpz3.Html.HtmlCss(szCss)) {
                Console.WriteLine($"css:\r\n{css.ToString()}");
            }

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
