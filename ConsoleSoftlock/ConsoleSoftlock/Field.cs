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

        public void Out ()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(Grid[x, y].Symbol);
                Console.Write("\n");
            }
        }

        public void Fill (ICell cell)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Grid[x, y] = cell;
        }
    }
}
