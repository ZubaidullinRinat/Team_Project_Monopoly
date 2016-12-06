using Logic.DataProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository r = new Repository();
            Console.WriteLine(r.test());
            Console.ReadKey();
        }
    }
}
