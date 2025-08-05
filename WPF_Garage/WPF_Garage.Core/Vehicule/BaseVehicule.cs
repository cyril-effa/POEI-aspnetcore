using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Moteur;

namespace WPF_Garage.Core.Vehicule
{
    public abstract class BaseVehicule : IVehicule
    {
        public double Prix { get; set; }
        public double PrixTotal;
        public string Nom { get; set; }
        public Marque NomMarque { get; set; }
        public BaseMoteur Moteur { get; set; }
        public List<IOption> Options { get; set; } = new List<IOption>();

        public BaseVehicule(string nom, Marque nomMarque, BaseMoteur moteur, double prix)
        {
            Nom = nom;
            NomMarque = nomMarque;
            Moteur = moteur;
            Prix = prix;
        }

        public string ToString()
        {
            return $"{Nom} ({NomMarque}) - {Moteur.Type} - {Moteur.Cylindre} - Prix: {Prix:C}";
        }

        public virtual IVehicule Clone()
        {
            return Clone();
        }
    }
}
