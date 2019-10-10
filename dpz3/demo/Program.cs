using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo {
    class Program {
        static void Main(string[] args) {

            string md = dpz3.File.UTF8File.ReadAllText(@"X:\Temp\temp.md");
            using (var doc = dpz3.Markdown.Parser.GetDocument(md)) {
                Console.WriteLine("String:");
                Console.WriteLine(doc.ToString());
                Console.WriteLine("Markdown:");
                Console.WriteLine(doc.ToMarkdown());
                Console.WriteLine("HTML:");
                Console.WriteLine(doc.ToHtml());
            }

            Console.ReadKey();
        }
    }
}
