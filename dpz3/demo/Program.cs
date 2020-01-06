using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dpz3;

namespace demo {
    class Program {
        static void Main(string[] args) {

            string a = "abc";
            string b = "123";
            Console.WriteLine($"int a={a.ToInteger()}");
            Console.WriteLine($"int b={b.ToInteger()}");

            using (dpz3.Json.JsonObject json = new dpz3.Json.JsonObject()) {
                json["obj"].Str["a"] = "abc";
                json["obj"].Num["b"] = 123;
                Console.WriteLine(json.ToJsonString());
            }

            People p1 = new People();
            p1.Name.Value = "张三";
            p1.Age.Value = 18;
            p1.Sex.Value = "Man";

            byte[] arr = p1.ToBytes();

            for (int i = 0; i < arr.Length; i++) {
                Console.Write($"{arr[i]} ");
            }

            Console.WriteLine();

            People p2 = new People();
            p2.SetValue(arr);

            p2.Name.Value = "李四";
            Console.WriteLine(p2.Sex);

            var arr2 = p2.ToBytes();

            for (int i = 0; i < arr2.Length; i++) {
                Console.Write($"{arr2[i]} ");
            }

            Console.WriteLine();

            for (int i = 0; i < arr.Length; i++) {
                Console.Write($"{arr[i]} ");
            }
            //byte[] arr2 = new ArraySegment<byte>(arr, 0, 5).Array;

            Console.WriteLine();

            // 测试对象序列化
            dpz3.Serializable.Double db = 12;
            Console.WriteLine($"Double {db.ToString()} {db.Size} {db.ToBytes().Length}");

            dpz3.Serializable.Single sgl = 12;
            Console.WriteLine($"Single {sgl.ToString()} {sgl.Size} {sgl.ToBytes().Length}");

            dpz3.Serializable.Int8 i8 = 12;
            Console.WriteLine($"Int8 {i8.ToString()} {i8.Size} {i8.ToBytes().Length}");

            dpz3.Serializable.Int16 i16 = 12;
            Console.WriteLine($"Int16 {i16.ToString()} {i16.Size} {i16.ToBytes().Length}");

            dpz3.Serializable.Int32 i32 = 12;
            Console.WriteLine($"Int32 {i32.ToString()} {i32.Size} {i32.ToBytes().Length}");

            dpz3.Debug.WriteLine("123456");

            //string a = "123456";
            int n = a.ToInteger();
            string md5 = a.GetMD5();

            Console.WriteLine($"n={n}");
            Console.WriteLine($"md5={md5}");

            string str = dpz3.File.UTF8File.ReadAllText(@"X:\Projects\modular\core\ModularCore\bin\Debug\netcoreapp3.1\packages\simple-home\1.0.1912.4\modular.json");
            Console.WriteLine(str);
            var obj = dpz3.Json.Parser.ParseJson("{}");
            Console.WriteLine(obj.ToJsonString());

            //string md = dpz3.File.UTF8File.ReadAllText(@"X:\Temp\temp.md");
            //using (var doc = dpz3.Markdown.Parser.GetDocument(md)) {
            //    Console.WriteLine("String:");
            //    Console.WriteLine(doc.ToString());
            //    Console.WriteLine("Markdown:");
            //    Console.WriteLine(doc.ToMarkdown());
            //    Console.WriteLine("HTML:");
            //    Console.WriteLine(doc.ToHtml());

            //    // 输出到HTML文件
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("<html>");
            //    sb.Append("<head>");
            //    sb.Append("<title>Markdown测试</title>");
            //    sb.Append("</head>");
            //    sb.Append("<body>");
            //    sb.Append(doc.ToHtml());
            //    sb.Append("</body>");
            //    sb.Append("</html>");
            //    dpz3.File.UTF8File.WriteAllText(@"X:\Temp\temp.html", sb.ToString());
            //}

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
