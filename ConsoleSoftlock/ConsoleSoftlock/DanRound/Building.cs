using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public abstract class Building
    {
        public Vector2 Position { get; }
        public char Symbol { get; }
        public bool IsTransparent { get; set; }

        public Building(Vector2 position, char symbol) { Position = position; Symbol = symbol; }
    }
}
