using DemoTrip.Core.Entities;
using DemoTrip.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.UnitTests
{
    [TestClass]
    public class SearchTripUseCase_Test
    {
        [TestMethod]
        public void When_NotFound_AnyTrip_Should_Response_False()
        {
            var request = new SearchTripRequestDto()
            {
                Origin = "CDG",
                Destination = "NYC",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(2)
            };
            var response = new SearchTripUseCase(new FakeFlightRepository(new List<Flight>()), new FakeHotelRepository(new List<Hotel>())).Execute(request);

            Assert.IsFalse(response.Succes);
        }


        [TestMethod]
        public void When_Found_Trip_Should_Response_True()
        {
            var request = new SearchTripRequestDto()
            {
                Origin = "CDG",
                Destination = "NYC",
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(2)
            };

            var flights = new List<Flight>()
            {
               new Flight() { Id = 1, Destination = "LA", Origin = "NY", DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20) },
               new Flight() { Id = 1, Destination = "NYC", Origin = "CDG" ,DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20)  },
            };

            var hotels = new List<Hotel>()
            {
                new Hotel() { Id = 1, Name = "Hotel A", Location = "NYC", CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(2) },
                new Hotel() { Id = 2, Name = "Hotel B", Location = "LAX", CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(3) }
            };

            var response = new SearchTripUseCase(new FakeFlightRepository(flights), new FakeHotelRepository(hotels)).Execute(request);

            Assert.IsTrue(response.Succes);
        }
    }
}
