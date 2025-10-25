using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public abstract class Building
    {
        public abstract Vector2 Position { get; set; }
        public abstract char Symbol { get; set; }
        public abstract bool IsTransparent { get; set; }

        //public Building(Vector2 position, char symbol) { Position = position; Symbol = symbol; }
    }
}
