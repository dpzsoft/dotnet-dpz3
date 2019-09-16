using System;

namespace demo {
    class Program {
        static void Main(string[] args) {

            string xml = "<div class=\"pg-nav\"><div class=\"pg-nav-box\"><div class=\"pg-nav-title\">{{title}}</div></div></div>\r\n<div class=\"pg-tools\"><i><a href=\"javascript:;\" v-on:click=\"onAdd\">����ֲ�ͼ</a></i><s><a href=\"javascript:;\" v-on:click=\"onRefresh\">ˢ���б�</a></s><s>|</s><s v-if=\"key===''\"><input type=\"text\" placeholder=\"���������ؼ���\" v-model=\"keyInput\" v-on:keyup.13=\"onSearch\" /></s><i v-if=\"key===''\"><a href=\"javascript:;\" v-on:click=\"onSearch\">����</a></i><p v-if=\"key!==''\">��ǰ��ʾΪ����\"<span>{{key}}</span>\"�Ľ����<a href=\"javascript:;\" v-on:click=\"onSearchClear\">�������</a></p></div>\r\n<div class=\"pg-tools\"><p>Ϊ��֤��ͬ�ֻ��ͺŵļ����ԣ������ֲ�ͼ��Ҫ����5��</p></div>\r\n<div class=\"pg-list-page\">\r\n    <div class=\"pg-list\">\r\n        <xorm src=\"orm.xml\" type=\"list\"></xorm>\r\n        <div class=\"pg-pages\">\r\n            <dl>\r\n                <dd><i>��{{rowCount}}����ÿҳ��ʾ{{pageSize}}������ǰ��{{page}}/{{pageCount}}ҳ</i></dd>\r\n                <dd v-if=\"page>2\"><a href=\"javascript:;\" v-on:click=\"onChangePage(1)\">��ҳ</a></dd>\r\n                <dd v-if=\"page>1\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-1)\">��һҳ</a></dd>\r\n                <dd v-if=\"page>5\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-5)\">{{page-5}}</a></dd>\r\n                <dd v-if=\"page>4\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-4)\">{{page-4}}</a></dd>\r\n                <dd v-if=\"page>3\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-3)\">{{page-3}}</a></dd>\r\n                <dd v-if=\"page>2\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-2)\">{{page-2}}</a></dd>\r\n                <dd v-if=\"page>1\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page-1)\">{{page-1}}</a></dd>\r\n                <dd><span>{{page}}</span></dd>\r\n                <dd v-if=\"page<pageCount\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+1)\">{{page+1}}</a></dd>\r\n                <dd v-if=\"page<pageCount-1\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+2)\">{{page+2}}</a></dd>\r\n                <dd v-if=\"page<pageCount-2\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+3)\">{{page+3}}</a></dd>\r\n                <dd v-if=\"page<pageCount-3\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+4)\">{{page+4}}</a></dd>\r\n                <dd v-if=\"page<pageCount-4\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+5)\">{{page+5}}</a></dd>\r\n                <dd v-if=\"page<pageCount\"><a href=\"javascript:;\" v-on:click=\"onChangePage(page+1)\">��һҳ</a></dd>\r\n                <dd v-if=\"page<pageCount-1\"><a href=\"javascript:;\" v-on:click=\"onChangePage(pageCount)\">ĩҳ</a></dd>\r\n                <dt></dt>\r\n            </dl>\r\n        </div>\r\n    </div>\r\n</div>";

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
