using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Moteur
{
    public class MoteurHybride : BaseMoteur
    {
        public MoteurHybride(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.HYBRIDE;
        }

        public override BaseMoteur Clone()
        {
            return new MoteurHybride(Cylindre, Prix)
            {
                Type = this.Type
            };
        }
    }
}
