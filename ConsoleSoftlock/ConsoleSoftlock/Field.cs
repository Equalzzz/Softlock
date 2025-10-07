using ConsoleSoftlock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public readonly struct Field(Player owner, int width, int height)
    {
        public Player Owner { get; } = owner;
        public ICell[,] Grid { get; } = new ICell[width, height];
        public int Width { get; } = width;
        public int Height { get; } = height;
    }
}
