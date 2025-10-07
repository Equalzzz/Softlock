using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
    public class GameState
    {
        public Field Player1Field { get; }
        public Field Player2Field { get; }
        public Player? CurrentPlayer { get; set; }
        public GameState(in Field p1field, in Field p2field)
        {
            Player1Field = p1field;
            Player2Field = p2field;
        }
        public GameState(Player p1, Player p2, int width, int height)
        {
            Player1Field = new Field(p1, width, height);
            Player2Field = new Field(p2, width, height);
        }
        public GameState(Player p1, Player p2, int size) : this(p1, p2, size, size) { }
        public int ApplyAction(int a) { return 1; }
    }
}
