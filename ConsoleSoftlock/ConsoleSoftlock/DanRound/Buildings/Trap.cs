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

        public bool IsPowered()
        {
            return true;
        }
    }
}
