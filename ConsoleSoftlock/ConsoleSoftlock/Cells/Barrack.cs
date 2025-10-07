using ConsoleSoftlock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.Cells
{
    public class Barrack : ICell
    {
        public char Symbol { get; set; }

        public Barrack() {
            // ToDo: Брать символы из конфига
            Symbol = 'o';
        }
    }
}
