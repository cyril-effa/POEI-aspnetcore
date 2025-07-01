using DemoTrip.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.UseCases
{
    public class SearchTripRequestDto
    {
        public SearchTripRequestDto()
        {
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
