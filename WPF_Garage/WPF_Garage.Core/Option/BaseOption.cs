using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Option
{
    public abstract class BaseOption : IOption
    {
        public string Nom { get; set; }
        public double Prix { get; set; }

        public BaseOption(string nom, double prix)
        {
            Nom = nom;
            Prix = prix;
        }
    }
}
