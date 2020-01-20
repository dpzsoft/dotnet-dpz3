using System;

namespace dpz3.AspNetCore3.Demo {
    class Program {
        static void Main(string[] args) {
            using (dpz3.AspNetCore.KestrelConfig config = new AspNetCore.KestrelConfig(@"X:\Projects\modular\core\ModularCore\bin\Debug\netcoreapp3.1\conf\kestrel.cfg")) {
                System.Console.WriteLine(config.Enable);
            }
            System.Console.WriteLine("Hello World!");
        }
    }
}
