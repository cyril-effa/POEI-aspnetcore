using System.Collections.ObjectModel;
using System.ComponentModel;
using WPF_Garage.Core.Enum;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Vehicule;
using WPF_Garage.MVVM;


namespace WPF_Garage.ViewModels
{
    public class VehiculeViewModel : NotifyPropertyChanged
    {
        public IVehicule Vehicule { get; }

        public string Nom
        {
            get => Vehicule.Nom;
            set
            {
                if (Vehicule.Nom != value)
                {
                    Vehicule.Nom = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Marque
        {
            get => Vehicule.NomMarque.ToString();
            set
            {
                if (Vehicule.NomMarque.ToString() != value)
                {
                    if (Enum.TryParse<Marque>(value, out var marque))
                    {
                        Vehicule.NomMarque = marque;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public string Prix
        {
            get => Vehicule.Prix.ToString("N2") + " €";
            set
            {
                if (double.TryParse(value.Replace(" €", ""), out var prix) && Vehicule.Prix != prix)
                {
                    Vehicule.Prix = prix;
                    OnPropertyChanged();
                }
            }
        }

        public string MoteurType
        {
            get => Vehicule.Moteur.Type.ToString();
            set
            {
                if (Vehicule.Moteur.Type.ToString() != value)
                {
                    if (Enum.TryParse<TypeMoteur>(value, out var type))
                    {
                        Vehicule.Moteur.Type = type;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public string Cylindre
        {
            get => Vehicule.Moteur.Cylindre;
            set
            {
                if (Vehicule.Moteur.Cylindre != value)
                {
                    Vehicule.Moteur.Cylindre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrixMoteur
        {
            get => Vehicule.Moteur.Prix.ToString("N2") + " €";
            set
            {
                if (double.TryParse(value.Replace(" €", ""), out var prix) && Vehicule.Moteur.Prix != prix)
                {
                    Vehicule.Moteur.Prix = prix;
                    OnPropertyChanged();
                }
            }
        }

        public string OptionsStr
        {
            get => string.Join(", ", Vehicule.Options.Select(o => o.Nom + " (" + o.Prix + "€)"));
        }

        public ObservableCollection<OptionViewModel> Options
        {
            get;
            set;
        }

        public string PrixTotal
        {
            get => Vehicule.PrixTotal.ToString("N2") + " €";
        }

        public VehiculeViewModel(IVehicule vehicule)
        {
            Vehicule = vehicule;
            Options = new ObservableCollection<OptionViewModel>(
                vehicule.Options.Select(o => new OptionViewModel(o))
            );
        }

        public VehiculeViewModel Clone()
        {
            return new VehiculeViewModel(Vehicule.Clone())
            {
                Options = new ObservableCollection<OptionViewModel>(
                    Vehicule.Options.Select(o => new OptionViewModel(o))
                )
            };
        }

        public void AddOption(IOption option)
        {
            Vehicule.Options.Add(option);
            Options.Add(new OptionViewModel(option));
            OnPropertyChanged(nameof(OptionsStr));
            OnPropertyChanged(nameof(PrixTotal));
        }

        public void ClearOptions()
        {
            Vehicule.Options.Clear();
            Options.Clear();
            OnPropertyChanged(nameof(OptionsStr));
            OnPropertyChanged(nameof(PrixTotal));
        }
    }
}