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
            PhoneBookManager manager;
            manager = PhoneBookManager.Create();
            while (true)
            {
                int choice = -1;
                manager.ShowMenu();
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }catch(Exception)
                {
                    Console.WriteLine("입력이 잘못되었습니다.");
                    choice = -1;
                }
                try
                {
                    switch (choice)
                    {
                        case 1: manager.InputData(); break;
                        case 2: manager.ListData(); break;
                        case 3: manager.SearchData(); break;
                        case 4: manager.Compar(); break;
                        case 5: manager.DeleteData(); break;
                        case 6: Console.Clear(); manager.END(); return;
                    }
                }
                catch (Exception)
                {
                    manager.END();
                }
                Console.Clear();
            }
        }
    }
}
