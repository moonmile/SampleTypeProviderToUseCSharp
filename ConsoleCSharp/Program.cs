using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // refer SampleType.tomoaki.dll 
            var a = new FSharpLib.AA();
            Console.WriteLine("Typeprovider test in C#");
            Console.WriteLine("{0}", a.Name);
            Console.WriteLine("{0}", a.Version);

            System.Console.ReadKey();
        }
    }
}
