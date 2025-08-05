using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Moteur;
using WPF_Garage.Core.Option;

namespace WPF_Garage.Core.Vehicule
{
    public class VehiculeA300B : BaseVehicule
    {
        public VehiculeA300B() : base("A300B", Marque.PIGEOT, new MoteurDiesel("100c", 5000), 10000)
        {
            Options.Add(new OptionClimatisation());
            Options.Add(new OptionGPS());
        }

        public override IVehicule Clone()
        {
            return new VehiculeA300B
            {
                Nom = this.Nom,
                NomMarque = this.NomMarque,
                Moteur = this.Moteur.Clone(),
                Prix = this.Prix,
                Options = new List<IOption>(this.Options)
            };
        }
    }
}
