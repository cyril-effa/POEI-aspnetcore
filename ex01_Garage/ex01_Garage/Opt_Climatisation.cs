using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Opt_Climatisation : IOption
    {
        public double Prix { get; set; }

        public Opt_Climatisation()
        {
            Prix = 20;
        }

        public override string ToString()
        {
            return $"Climatisation ({Prix}€)";
        }
    }
}
