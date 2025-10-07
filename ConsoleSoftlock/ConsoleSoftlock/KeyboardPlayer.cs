using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public class KeyboardPlayer : Player
    {
        public override int GetAction(GameState state)
        {
            string? input;
            do
            {
                Console.Write("Please, select an action:" + "\n" + "1. Shoot" + "\n" + "2. Build" + "\n" + "Your choice: ");
                input = Console.ReadLine();
            } while (input != "1" && input != "2");

            switch (input)
            {
                case "1":
                    // ToDo: Стрельба
                    break;
                case "2":
                    (int, int) pos;
                    do
                    {
                        Console.Write("Input position (row column): ");
                        input = Console.ReadLine();
                    }
                    while (!TryParseInputToPosition(input, out pos));
                    Console.Write("You've inputed these coords: ");
                    Console.Write(pos + "\n");
                    //Console.Write("Please, select a building: ");

                    // ToDo: Выбор постройки
                    // Пиздец ниже починим но для этого нужен Илюшка
                    return pos.Item1 * 10 + pos.Item2;
            }

            Console.Beep();

            return 0;
        }
        static bool TryParseInputToPosition(string? input, out (int x, int y) position)
        {
            position = default;
            if (input == null) return false;
            string[] arr = input.Split(' ');
            if (arr.Length != 2) return false;
            if (int.TryParse(arr[0], out int x) && int.TryParse(arr[1], out int y))
            {
                position = (x, y);
                return true;
            }
            return false;
        }
    }
}
