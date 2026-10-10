using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound.Buildings
{
    public class Trap : Building, Protection, Powerable
    {
        public override Vector2 Position { get; set; }
        public override char Symbol { get; set; }
        public override bool IsTransparent { get; set; }
        public bool IsProtectingSoliders { get; set; }
        public bool IsProtectingRockets { get; set; }

        public Trap (int x, int y)
        {
            IsProtectingSoliders = true;
            IsProtectingRockets = false;
            
            Position = new Vector2 (x, y);

            Symbol = '#';
            IsTransparent = true;
        }

        public bool IsPowered(GameField field)
        {
            bool isPowered = false;

            int x = Position.Pos.x;
            int y = Position.Pos.y;

            if (y != 7 && field.Field[y + 1, x].Building is Barracks)
                isPowered = true;
            if (y != 0 && field.Field[y - 1, x].Building is Barracks)
                isPowered = true;
            if (x != 0 && field.Field[y, x - 1].Building is Barracks)
                isPowered = true;
            if (x != 7 && field.Field[y, x + 1].Building is Barracks)
                isPowered = true;

            return isPowered;
        }
    }
}
