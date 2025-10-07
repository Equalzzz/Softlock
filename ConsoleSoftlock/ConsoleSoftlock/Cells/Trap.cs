using ConsoleSoftlock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.Cells
{
    public class Trap : ICell
    {
        public char Symbol { get; set; }

        public Trap ()
        {
            Symbol = '#';
        }
    }
}
