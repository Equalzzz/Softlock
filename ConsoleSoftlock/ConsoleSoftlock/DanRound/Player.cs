using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class Player
    {
        public String Name { get; set; }
        public GameField Field { get; set; }

        public Player (string name, GameField field)
        {
            Name = name;
            Field = field;
        }
    }
}
