using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class V_D4 : Vehicule
    {
        public V_D4()
        {
            Nom = "D4";
            NomMarque = Marque.TROEM;
            Prix = 30000;
            PrixTotal = Prix;
        }
    }
}
