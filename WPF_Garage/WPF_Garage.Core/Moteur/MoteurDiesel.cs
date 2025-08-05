using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Moteur
{
    public class MoteurDiesel : BaseMoteur
    {
        public MoteurDiesel(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.DIESEL;
        }

        public override BaseMoteur Clone()
        {
            return new MoteurDiesel(Cylindre, Prix)
            {
                Type = this.Type
            };
        }
    }
}
