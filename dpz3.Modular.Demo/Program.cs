using System;

namespace dpz3.Modular.Demo {
    class Program {
        static void Main(string[] args) {
            System.Console.WriteLine("Hello World!");
            string html = dpz3.File.UTF8File.ReadAllText(@"X:\Projects\modular\simple-home\pages\index.aspx");
            string res = dpz3.Modular.Parser.ParseAspxToCode(html, "GetTest");
            System.Console.WriteLine(res);
        }
    }
}
