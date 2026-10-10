using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public interface Shooting
    {
        public bool IsShootingSoliders { get; set; }
        public bool IsShootingRockets { get; set; }

        public bool Shoot(GameField field1, GameField field2, Direction direction);
    }
}
