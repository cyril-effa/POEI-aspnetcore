using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Opt_GPS : IOption
    {
        public double Prix { get; set; }

        public Opt_GPS()
        {
            Prix = 30;
        }

        public override string ToString()
        {
            return $"GPS ({Prix}€)";
        }
    }
}
