using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Option;

namespace WPF_Garage.Core.Vehicule
{
    public class VehiculeLagouna : BaseVehicule
    {
        public VehiculeLagouna() : base("Lagouna", Marque.RENO, new Moteur.MoteurElectrique("150c", 5000), 20000)
        {
            Options.Add(new OptionClimatisation());
            Options.Add(new OptionSiegeChauffant());
            Options.Add(new OptionVitreElectrique());
        }

        public override IVehicule Clone()
        {
            return new VehiculeLagouna
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
