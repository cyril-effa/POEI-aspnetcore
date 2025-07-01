using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Opt_SiegeChauffant : IOption
    {
        public double Prix { get; set; }

        public Opt_SiegeChauffant()
        {
            Prix = 40;
        }

        public override string ToString()
        {
            return $"Siege chauffant ({Prix}€)";
        }
    }
}
