using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Opt_BarreDeToit : IOption
    {
        public double Prix { get; set; }

        public Opt_BarreDeToit()
        {
            Prix = 10;
        }

        public override string ToString()
        {
            return $"Barre de toit ({Prix}€)";
        }
    }
}
