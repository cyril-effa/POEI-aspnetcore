using DemoTrip.Core.Entities;
using DemoTrip.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.UnitTests
{
    internal class FakeHotelRepository : IHotelRepository
    {
        private readonly List<Hotel> hotels;

        public FakeHotelRepository(List<Hotel> hotels)
        {
            this.hotels = hotels;
        }

        public List<Hotel> GetAll()
        {
            return hotels;
        }
    }
}
