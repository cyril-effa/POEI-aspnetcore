using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Moteur
{
    public abstract class BaseMoteur : IMoteur
    {
        public TypeMoteur Type { get; set; }
        public string Cylindre { get; set; }
        public double Prix { get; set; }

        public BaseMoteur(string cylindre, double prix)
        {
            Cylindre = cylindre;
            Prix = prix;
        }

        public abstract BaseMoteur Clone();
    }
}
