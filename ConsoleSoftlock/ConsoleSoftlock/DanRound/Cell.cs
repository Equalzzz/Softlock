using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class Cell
    {
        public Building? Building { get; set; }
        public char Symbol { get; set; }
        public bool IsCollapsed { get; set; }

        public Cell () { 
            IsCollapsed = false;
            Symbol = '+';
        }

        public void SetBuilding (Building building) {
            IsCollapsed = false;
            Building = building;
            Symbol = building.Symbol;
        }

        public void Collapse ()
        {
            Building = null;
            Symbol = 'G';
            IsCollapsed = true;
        }
    }
}
