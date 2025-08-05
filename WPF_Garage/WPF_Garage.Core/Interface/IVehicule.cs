using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Moteur;

namespace WPF_Garage.Core.Interface
{
    public interface IVehicule
    {
        public double Prix { get; set; }
        public double PrixTotal => Prix + Moteur.Prix + Options.Sum(o => o.Prix);
        public string Nom { get; set; }
        public Marque NomMarque { get; set; }
        public BaseMoteur Moteur { get; set; }
        public List<IOption> Options { get; set; }

        public IVehicule Clone();
    }
}
