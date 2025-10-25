using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound.Buildings
{
    public class Wall : Building, Protection, Powerable
    {
        public bool IsProtectingSoliders { get; set; }
        public bool IsProtectingRockets { get; set; }
        public override Vector2 Position { get; set; }
        public override char Symbol { get; set; }
        public override bool IsTransparent { get; set; }

        public Wall (int x, int y)
        {
            IsProtectingSoliders = false;
            IsProtectingRockets = true;

            Position = new Vector2 (x, y);

            Symbol = 'X';
            IsTransparent = true;
        }

        public bool IsPowered()
        {
            return true;
        }
    }
}
