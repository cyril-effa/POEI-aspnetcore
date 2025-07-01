using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class MoteurElectrique : Moteur
    {
        public MoteurElectrique(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.ELECTRIQUE;
        }
    }
}
