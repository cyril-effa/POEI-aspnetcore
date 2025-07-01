using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Garage
    {
        private List<Vehicule> voitures = new List<Vehicule>();

        public void AddVoiture(Vehicule v)
        {
            voitures.Add(v);
        }

        public override string ToString()
        {
            string str = string.Empty;

            if (voitures.Count == 0)
            {
                str = "Aucune voiture sauvegardée !\r\n**************************\r\n*     Garage .NET        *\r\n**************************\r\n";

                return str;
            }

            foreach (var v in voitures)
            {
                str += v.ToString() + "\r\n";
            }
            return str;
        }
    }
}
