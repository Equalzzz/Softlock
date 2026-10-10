using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class GameField
    {
        public Cell[,] Field;

        public GameField ()
        {
            Field = new Cell[8, 8];

            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    Field[y, x] = new Cell();
                }
        }
    }
}
