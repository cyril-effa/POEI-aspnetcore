using System.ComponentModel.DataAnnotations;

namespace DemoTrip.Web.ViewModels
{
    public class SearchHotelRequestViewModel
    {
        [Required]
        public string? Location { get; set; }

        [Required]
        public DateTime? DateFrom { get; set; }

        [Required]
        public DateTime? DateTo { get; set; }
    }
}
