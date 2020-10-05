using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class Class1
    {
        static void Main()
        {
            HashSet<PhoneInfo> phones = new HashSet<PhoneInfo>();
            phones.Add(new PhoneInfo("용칼님", "01088385429"));
            phones.Add(new PhoneInfo("용할님", "01088385429"));

            HashSet<string> test = new HashSet<string>();
            test.Add("10");
            test.Add("10");


            foreach (PhoneInfo temp in phones)
            {
                Console.WriteLine(temp.ToString());
            }
        }

    }
}
