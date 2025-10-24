using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y) { this.x = x; this.y = y; }
    }

    public class Vector2
    {
        public Position Pos { get; }
        public Direction Dir { get; }

        public Vector2 (int x, int y)
        {
            Dir = Direction.None;
            Pos = new Position(x, y);
        }

        public Vector2(int x, int y, Direction dir)
        {
            Pos = new Position(x, y);
            Dir = dir;
        }
    }
}
