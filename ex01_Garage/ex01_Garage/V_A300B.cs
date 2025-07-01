using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class V_A300B : Vehicule
    {
        public V_A300B()
        {
            Nom = "A300B";
            NomMarque = Marque.PIGEOT;
            Prix = 10000;
            PrixTotal = Prix;
        }
    }
}
