using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class Menu
    {
        public string Title { get; set; }
        public List<string> Options { get; set; }
        public int CurrentOption;

        public Menu(string title, List<string> options) { Title = title; Options = options; CurrentOption = 0; }

        public int MenuProccess (string unclearbleInfo) {
            bool flag = true;

            while (flag)
            {
                Console.WriteLine(unclearbleInfo);
                Console.WriteLine(Title);
                for (int i = 0; i < Options.Count; i++)
                {
                    if (i == CurrentOption)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(Options[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (CurrentOption == 0)
                        CurrentOption = Options.Count - 1;
                    else
                        CurrentOption--;
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (CurrentOption == Options.Count - 1)
                        CurrentOption = 0;
                    else
                        CurrentOption++;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    flag = false;
                }

                Console.Clear();
            }

            return CurrentOption;
        }
    }
}
