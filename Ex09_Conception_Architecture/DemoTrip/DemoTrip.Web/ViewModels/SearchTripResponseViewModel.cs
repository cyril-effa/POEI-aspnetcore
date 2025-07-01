using DemoTrip.Core.Entities;

namespace DemoTrip.Web.ViewModels
{
    public class SearchTripResponseViewModel
    {
        public Trip Trip { get; set; } = new();
        public int NumberOfNights { get; set; }
        public decimal TotalCost { get; set; }
    }
}
