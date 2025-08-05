using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Moteur;
using WPF_Garage.Core.Option;

namespace WPF_Garage.Core.Vehicule
{
    public class VehiculeD4 : BaseVehicule
    {
        public VehiculeD4() : base("D4", Marque.TROEM, new MoteurEssence("200c", 10000), 30000)
        {
            Options.Add(new OptionBarreDeToit());
        }

        public override IVehicule Clone()
        {
            return new VehiculeD4
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
