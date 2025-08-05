using WPF_Garage.Core.Interface;

namespace WPF_Garage.Core.Garage
{
    public class Garage : IGarage
    {
        public List<IVehicule> Vehicules { get; set; } = new List<IVehicule>();
    }
}
