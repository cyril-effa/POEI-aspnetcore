using System.Windows;
using WPF_Garage.ViewModels;

namespace WPF_Garage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GarageViewModel();
        }
    }
}