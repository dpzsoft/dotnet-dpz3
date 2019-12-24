using System;

namespace demo {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            using (var jttp = new dpz3.Jttp.Object()) {
                jttp.Text = "12344";
                Console.WriteLine(jttp.ToString());
            }

        }
    }
}
