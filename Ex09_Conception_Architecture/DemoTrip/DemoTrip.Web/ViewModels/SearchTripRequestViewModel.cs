using DemoTrip.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace DemoTrip.Web.ViewModels
{
    public class SearchTripRequestViewModel
    {
        [Required]
        public string? Origin { get; set; }

        [Required]
        public string? Destination { get; set; }

        [Required]
        public DateTime? DateFrom { get; set; }

        [Required]
        public DateTime? DateTo { get; set; }
    }
}
