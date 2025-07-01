using DemoTrip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.UseCases
{
    public class SearchTripResponseDto
    {
        public bool Succes { get; set; }
        public Trip Trip { get; set; } = new();
        public int NumberOfNights { get; set; }
        public decimal TotalCost => (Trip?.Flight?.Price ?? 0) + (Trip?.Hotel?.PricePerNight * NumberOfNights ?? 0);
    }
}
