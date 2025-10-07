using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public class KeyboardPlayer : Player
    {
        public override int[] GetAction(GameState state)
        {
            int[] action = new int[4];
            string? input;

            do
            {
                Console.Write("Please, select an action:" + "\n" + "1. Shoot" + "\n" + "2. Build" + "\n" + "Your choice: ");
                input = Console.ReadLine();
            } while (input != "1" && input != "2");

            (int, int) pos;

            switch (input)
            {
                case "1":
                    // ToDo: int для стрельбы
                    action[0] = 1;

                    do
                    {
                        Console.Write("Input position (row column): ");
                        input = Console.ReadLine();
                    }
                    while (!TryParseInputToPosition(input, out pos));

                    action[1] = pos.Item1;
                    action[2] = pos.Item2;
                    break;
                case "2":
                    action[0] = 2;

                    do
                    {
                        Console.Write("Input position (row column): ");
                        input = Console.ReadLine();
                    }
                    while (!TryParseInputToPosition(input, out pos));

                    action[1] = pos.Item1;
                    action[2] = pos.Item2;

                    Console.Write("You've inputed these coords: ");
                    Console.Write(pos + "\n");

                    input = "";

                    do
                    {
                        Console.Write("Please, select a building:" + "\n" + "1. Barrack" + "\n" + "2. Trap" + "\n" + "Your choise: ");
                        input = Console.ReadLine();
                    } while (input != "1" && input != "2");

                    action[3] = int.Parse(input);


                    // ToDo: Вменяемый выбор постройки

                    break;
            }

            Console.Beep();

            return action;
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
