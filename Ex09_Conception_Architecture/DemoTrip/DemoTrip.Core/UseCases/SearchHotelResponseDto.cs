using DemoTrip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.UseCases
{
    public class SearchHotelResponseDto
    {
        public bool Succes { get; set; }
        public List<Hotel> Hotels { get; set; } = new();
    }
}
