using System.Windows;
using WPF_Garage.Core.Interface;

namespace WPF_Garage.Views
{
    public partial class AddOptionWindow : Window
    {
        public List<IOption> OptionsAjoutees { get; private set; } = new();

        public AddOptionWindow(IEnumerable<IOption> optionsDisponibles)
        {
            InitializeComponent();
            OptionsListBox.ItemsSource = optionsDisponibles;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            OptionsAjoutees = OptionsListBox.SelectedItems.Cast<IOption>().ToList();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
