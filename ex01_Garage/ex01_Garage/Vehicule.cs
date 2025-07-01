using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal class Vehicule
    {
        public double Prix { get; set; }
        public double PrixTotal { get; set; }
        public string Nom { get; set; }
        public Marque NomMarque { get; set; }
        public Moteur Moteur { get; set; }

        private List<IOption> options = new List<IOption>();

        public void AddOption(IOption option)
        {
            options.Add(option);
            PrixTotal += option.Prix;
        }

        public void SetMoteur(Moteur moteur)
        {
            Moteur = moteur;
            PrixTotal += moteur.Prix;
        }

        public override string ToString()
        {
            string str = $"+ Voiture {NomMarque} : {Nom} Moteur {Moteur} ";
            for (int i = 0; i < options.Count; i++)
            {
                str += i == 0 ? "[" : "";
                str += options[i].ToString();
                str += i < options.Count - 1 ? ", " : "] ";
            }
            str += $"d'une valeur totale de {PrixTotal}€\r\n";
            return str;
        }
    }
}
