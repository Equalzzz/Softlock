using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Player p1 = new KeyboardPlayer();
            Player p2 = new KeyboardPlayer();

            SoftlockGame game = new(p1, p2, 8);
            game.StartGameLoop(p1);
        }
    }
}
