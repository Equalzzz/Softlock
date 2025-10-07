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
            Console.Beep();
            string? input;
            (int, int) pos;
            do
            {
                Console.Write("Input position (row column): ");
                input = Console.ReadLine();
            }
            while (!TryParseInputToPosition(input, out pos));
            Console.Write("You've inputed these coords: ");
            Console.Write(pos + "\n");
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
