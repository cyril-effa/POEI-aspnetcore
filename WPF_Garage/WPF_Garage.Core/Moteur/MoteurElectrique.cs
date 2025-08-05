using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Moteur
{
    public class MoteurElectrique : BaseMoteur
    {
        public MoteurElectrique(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.ELECTRIQUE;
        }

        public override BaseMoteur Clone()
        {
            return new MoteurElectrique(Cylindre, Prix)
            {
                Type = this.Type
            };
        }
    }
}
