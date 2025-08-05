using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Moteur
{
    public class MoteurEssence : BaseMoteur
    {
        public MoteurEssence(string cylindre, double prix) : base(cylindre, prix)
        {
            Type = TypeMoteur.ESSENCE;
        }

        public override BaseMoteur Clone()
        {
            return new MoteurEssence(Cylindre, Prix)
            {
                Type = this.Type
            };
        }
    }
}
