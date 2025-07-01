using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class V_Lagouna : Vehicule
    {
        public V_Lagouna()
        {
            Nom = "Lagouna";
            NomMarque = Marque.RENO;
            Prix = 20000;
            PrixTotal = Prix;
        }
    }
}
