using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class GameManager
    {
        public Player Player1;
        public Player Player2;

        public Player CurrentPlayer;

        public GameManager(Player p1, Player p2)
        {
            Player1 = p1;
            Player2 = p2;
        }

        public void PrintField ()
        {
            Console.WriteLine("X 1 2 3 4 5 6 7 8 | 8 7 6 5 4 3 2 1 X");
            for (int y = 0; y < 8; y++)
            {
                Console.Write((y + 1) + " ");
                for (int x = 0; x < 8; x++)
                {
                    Console.Write(Player1.Field.Field[y, x].Symbol + " ");
                }
                Console.Write("| ");
                for (int x = 0; x < 8; x++)
                {
                    Console.Write(Player2.Field.Field[y, x].Symbol + " ");
                }
                Console.Write((y + 1) + "\n");
            }
        }

        public void StartGame ()
        {
            CurrentPlayer = Player1;
        }
    }
}
