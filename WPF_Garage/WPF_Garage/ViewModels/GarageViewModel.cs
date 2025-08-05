using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Option;
using WPF_Garage.Core.Vehicule;
using WPF_Garage.MVVM;
using WPF_Garage.Views;

namespace WPF_Garage.ViewModels
{
    public class GarageViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<VehiculeViewModel> vehicules;
        public ObservableCollection<VehiculeViewModel> Vehicules
        {
            get => vehicules;
            set
            {
                vehicules = value;
                OnPropertyChanged();
            }
        }

        private VehiculeViewModel selectedVehicule;
        public VehiculeViewModel SelectedVehicule
        {
            get => selectedVehicule;
            set
            {
                selectedVehicule = value;
                EditableVehicule = value.Clone();
                OnPropertyChanged();
            }
        }

        private VehiculeViewModel editableVehicule;
        public VehiculeViewModel EditableVehicule
        {
            get => editableVehicule;
            set
            {
                editableVehicule = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<TypeMoteur> TypesMoteur => Enum.GetValues<TypeMoteur>();
        public IList SelectedOptions { get; set; } = new List<OptionViewModel>();

        private bool _isInfoPanelVisible = true;
        public bool IsInfoPanelVisible
        {
            get => _isInfoPanelVisible;
            set
            {
                _isInfoPanelVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToggleInfoPanelCommand { get; }
        public ICommand AddVehiculeCommand { get; }
        public ICommand EditVehiculeCommand { get; }
        public ICommand AddOptionCommand { get; }
        public ICommand DeleteOptionsCommand { get; }

        private bool isAddingVehicule;

        public GarageViewModel()
        {
            Vehicules = new ObservableCollection<VehiculeViewModel>
            {
                new VehiculeViewModel(new VehiculeA300B()),
                new VehiculeViewModel(new VehiculeD4()),
                new VehiculeViewModel(new VehiculeLagouna())
            };

            ToggleInfoPanelCommand = new RelayCommand(_ => IsInfoPanelVisible = !IsInfoPanelVisible);
            AddVehiculeCommand = new RelayCommand(_ => AddVehicule());
            EditVehiculeCommand = new RelayCommand(_ => EditVehicule());
            AddOptionCommand = new RelayCommand(_ => AddOption());
            DeleteOptionsCommand = new RelayCommand(_ => DeleteOptions());
        }

        private void AddVehicule()
        {
            isAddingVehicule = true;
            var fenetre = new AddVehiculeWindow();
            bool? resultat = fenetre.ShowDialog();

            if (resultat == true)
            {
                Vehicules.Add(fenetre.CreatedVehicule);
            }
            
            isAddingVehicule = false;
        }

        private void EditVehicule()
        {
            if (SelectedVehicule != null && EditableVehicule != null)
            {
                SelectedVehicule.Nom = EditableVehicule.Nom;
                SelectedVehicule.Marque = EditableVehicule.Marque;
                SelectedVehicule.Prix = EditableVehicule.Prix;
                SelectedVehicule.MoteurType = EditableVehicule.MoteurType;
                SelectedVehicule.Cylindre = EditableVehicule.Cylindre;
                SelectedVehicule.PrixMoteur = EditableVehicule.PrixMoteur;
                SelectedVehicule.ClearOptions();
                foreach (var option in EditableVehicule.Options)
                {
                    SelectedVehicule.AddOption(option.Option);
                }
            }
        }

        private void AddOption()
        {
            // Get all options available in the system
            var toutesLesOptions = GetAllOptions();

            var optionsDisponibles = toutesLesOptions
                .Where(option => !SelectedVehicule.Options.Contains(option))
                .Select(option => option.Option);

            var fenetre = new AddOptionWindow(optionsDisponibles);
            bool? resultat = fenetre.ShowDialog();

            if (resultat == true)
            {
                foreach (var option in fenetre.OptionsAjoutees)
                {
                    EditableVehicule.Options.Add(new OptionViewModel(option));
                }
            }
        }

        private List<OptionViewModel> GetAllOptions()
        {
            return new List<OptionViewModel>()
            {
                new OptionViewModel(new OptionBarreDeToit()),
                new OptionViewModel(new OptionClimatisation()),
                new OptionViewModel(new OptionGPS()),
                new OptionViewModel(new OptionSiegeChauffant()),
                new OptionViewModel(new OptionVitreElectrique())
            };
        }

        private void DeleteOptions()
        {
            foreach (var option in SelectedOptions.Cast<OptionViewModel>().ToList())
            {
                EditableVehicule.Options.Remove(option);
            }
        }
    }
}