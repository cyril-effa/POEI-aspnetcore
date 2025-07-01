using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class MoteurHybride : Moteur
    {
        public MoteurHybride(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.HYBRIDE;
        }
    }
}
