using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBookManager manager = new PhoneBookManager();
            while (true)
            {
                int choice = -1;
                manager.ShowMenu();
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }catch(Exception e)
                {
                    Console.WriteLine("입력이 잘못되었습니다.");
                    choice = -1;
                }

                switch (choice) 
                {
                    case 1: manager.InputData(); break;
                    case 2: manager.ListData(); break;
                    case 3: manager.SearchData(); break;
                    case 4: manager.DeleteData(); break;
                    case 5: Console.Clear();return;
                }

                Console.Clear();
            }
        }
    }
}
