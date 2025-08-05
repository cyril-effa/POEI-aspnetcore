using WPF_Garage.Core.Interface;
using WPF_Garage.MVVM;

namespace WPF_Garage.ViewModels
{
    public class OptionViewModel : NotifyPropertyChanged
    {
        public IOption Option { get; }

        public string Nom
        {
            get => Option.Nom;
            set
            {
                Option.Nom = value;
                OnPropertyChanged();
            }
        }

        public string Prix
        {
            get => Option.Prix.ToString("N2") + " €";
            set
            {
                if (double.TryParse(value.Replace(" €", ""), out var prix) && Option.Prix != prix)
                {
                    Option.Prix = prix;
                    OnPropertyChanged();
                }
            }
        }

        public OptionViewModel(IOption option)
        {
            Option = option;
        }
    }
}
