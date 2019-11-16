using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dpz3;

namespace demo {
    class Program {
        static void Main(string[] args) {

            dpz3.Debug.WriteLine("123456");

            string a = "123456";
            int n = a.ToInteger();
            string md5 = a.GetMD5();

            Console.WriteLine($"n={n}");
            Console.WriteLine($"md5={md5}");

            string md = dpz3.File.UTF8File.ReadAllText(@"X:\Temp\temp.md");
            using (var doc = dpz3.Markdown.Parser.GetDocument(md)) {
                Console.WriteLine("String:");
                Console.WriteLine(doc.ToString());
                Console.WriteLine("Markdown:");
                Console.WriteLine(doc.ToMarkdown());
                Console.WriteLine("HTML:");
                Console.WriteLine(doc.ToHtml());

                // 输出到HTML文件
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<head>");
                sb.Append("<title>Markdown测试</title>");
                sb.Append("</head>");
                sb.Append("<body>");
                sb.Append(doc.ToHtml());
                sb.Append("</body>");
                sb.Append("</html>");
                dpz3.File.UTF8File.WriteAllText(@"X:\Temp\temp.html", sb.ToString());
            }

            Console.ReadKey();
        }
    }
}
