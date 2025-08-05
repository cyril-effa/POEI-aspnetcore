using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_Garage.Core.Interface;
using WPF_Garage.Core.Vehicule;
using WPF_Garage.MVVM;

namespace WPF_Garage.ViewModels
{
    public class AddVehiculeViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<VehiculeViewModel> existingVehicules;
        public ObservableCollection<VehiculeViewModel> ExistingVehicules
        {
            get => existingVehicules;
            set
            {
                existingVehicules = value;
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
        public ICommand EditVehiculeCommand { get; }
        public Action<bool?> CloseAction { get; set; }

        public AddVehiculeViewModel()
        {
            ExistingVehicules = new ObservableCollection<VehiculeViewModel>
            {
                new VehiculeViewModel(new VehiculeA300B()),
                new VehiculeViewModel(new VehiculeD4()),
                new VehiculeViewModel(new VehiculeLagouna())
            };

            EditVehiculeCommand = new RelayCommand(_ => CloseWindow());
        }
        private void CloseWindow()
        {
            CloseAction?.Invoke(true);
        }
    }
}
