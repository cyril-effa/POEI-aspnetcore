using System.Windows;
using System.Windows.Controls;
using WPF_Garage.ViewModels;

namespace WPF_Garage.Views.UserControls
{
    public partial class VehiculeDetailsControl : UserControl
    {
        public VehiculeDetailsControl()
        {
            InitializeComponent();
        }
        public string ValidateButtonText
        {
            get => (string)GetValue(ValidateButtonTextProperty);
            set => SetValue(ValidateButtonTextProperty, value);
        }

        public static readonly DependencyProperty ValidateButtonTextProperty =
            DependencyProperty.Register(
                nameof(ValidateButtonText),
                typeof(string),
                typeof(VehiculeDetailsControl),
                new PropertyMetadata("Valider")); // Valeur par défaut

        public VehiculeViewModel EditableVehicule
        {
            get => (VehiculeViewModel)GetValue(EditableVehiculeProperty);
            set => SetValue(EditableVehiculeProperty, value);
        }

        public static readonly DependencyProperty EditableVehiculeProperty =
            DependencyProperty.Register("EditableVehicule", typeof(VehiculeViewModel), typeof(VehiculeDetailsControl), new PropertyMetadata(null));

    }
}
