using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WPF_Garage.ViewModels;

namespace WPF_Garage.Views
{
    public partial class AddVehiculeWindow : Window
    {
        public AddVehiculeViewModel ViewModel { get; }
        public VehiculeViewModel CreatedVehicule => ViewModel.EditableVehicule;

        public AddVehiculeWindow()
        {
            InitializeComponent();
            ViewModel = new AddVehiculeViewModel();
            ViewModel.CloseAction = result => this.DialogResult = result;
            DataContext = ViewModel;
        }
    }
}
