using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Opt_VitreElectrique : IOption
    {
        public double Prix { get; set; }

        public Opt_VitreElectrique()
        {
            Prix = 50;
        }

        public override string ToString()
        {
            return $"Vitre électrique ({Prix}€)";
        }
    }
}
