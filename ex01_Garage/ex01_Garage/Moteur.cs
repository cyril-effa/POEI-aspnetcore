using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal abstract class Moteur
    {
        public TypeMoteur Type { get; set; }
        public string Cylindre { get; set; }
        public double Prix { get; set; }

        public Moteur (string cylindre, double prix)
        {
            Cylindre = cylindre;
            Prix = prix;
        }

        public override string ToString()
        {
            return $"{Type} {Cylindre} ({Prix}€)";
        }
    }
}
