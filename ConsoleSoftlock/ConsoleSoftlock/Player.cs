using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public abstract class Player
    {
        public int Score;
        public abstract int[] GetAction(GameState state);
    }
}
