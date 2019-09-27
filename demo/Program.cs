using System;

namespace demo {
    class Program {

        static void Main(string[] args) {

            //using (dpz3.Text.Pinyin py = new dpz3.Text.Pinyin()) {
            //    var words = py.DebugOut();
            //    Console.WriteLine(words.Count);
            //    foreach (var item in words) {
            //        Console.WriteLine($"dicSpecial.Add('{item.Key}', new PinyinInfo(\"{item.Value.Pinyin}\", {item.Value.Tone}));");
            //    }
            //}


            string sz = "进入手机界面后，你会发现页面变得十分简洁，就算没登陆账号也能进行复制，手机网页的格式较为单一，你直接复制到word等其他地方，格式差异不大。";
            Console.WriteLine(sz);
            Console.WriteLine(dpz3.Text.Pinyin.GetPinyinFromString(sz));
            Console.WriteLine(dpz3.Text.Pinyin.GetPinyinToneFromString(sz));
            Console.WriteLine(dpz3.Text.Pinyin.GetInitialLettersFromString(sz));



            var arg = new dpz3.Console.Arguments(args);
            Console.WriteLine("path:" + arg.ContainsKey("t"));
            Console.WriteLine("path:" + arg.ContainsKey("n"));
            Console.WriteLine("path:" + arg.ContainsKey("path"));

            string fldHtmPath = "X:\\Projects\\beikejie\\dotnet-core-ycp-entity-bkj\\wwwroot\\pages\\Web\\WebAds\\list\\src\\field.Operate.htm";
            string htmlContent = dpz3.File.UTF8File.ReadAllText(fldHtmPath, true);
            using (var doc = new dpz3.Html.HtmlDocument()) {
                var div = doc.CreateElement("div");
                doc.Nodes.Add(div);
                div.InnerHTML = htmlContent;
                Console.WriteLine($"div:\r\n{doc.InnerHTML}");
            }

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

            //int tick = Environment.TickCount;
            //long lastLoad = 0;
            //dpz3.Net.HttpClient.Download("https://jx3wscs-fullupdatehttps.dl.kingsoft.com/jx3v4_launcher/JX3Installer_1.0.0.522_Official.exe", @"X:\temp\JX3Installer_1.0.0.522_Official.exe", (long size, long loaded) => {
            //    int ts = Environment.TickCount - tick;
            //    tick = Environment.TickCount;

            //    float speed = (int)(((loaded - lastLoad) / (float)ts) * 1000 / 10.24) / (float)100;
            //    lastLoad = loaded;

            //    string speedStr = "";

            //    if (speed < 1024) {
            //        speedStr = $"{speed}KB/s";
            //    } else {
            //        float speedMb = (int)(speed / 10.24) / (float)100;
            //        speedStr = $"{speedMb}MB/s";
            //    }

            //    Console.WriteLine($"{loaded}/{size} - {speedStr}");
            //});
        }
    }
}
