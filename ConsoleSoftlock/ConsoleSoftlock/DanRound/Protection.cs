using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public interface Protection
    {
        public bool IsProtectingSoliders { get; set; }
        public bool IsProtectingRockets { get; set; }
    }
}
