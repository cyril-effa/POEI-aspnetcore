using DemoTrip.Core.Entities;
using DemoTrip.Core.UseCases;

namespace DemoTrip.UnitTests
{
    [TestClass]
    public class SearchFlightUseCase_Test
    {
        [TestMethod]
        public void When_NotFound_AnyFlight_Should_Response_False_And_Empty()
        {
            var request = new SearchFlightRequestDto()
            {
                Destination = "NY",
                Origin = "CDG",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };
            var response = new SearchFlightUseCase(new FakeFlightRepository(new List<Flight>())).Execute(request);

            Assert.IsFalse(response.Succes);
            Assert.AreEqual(response.Flights.Count, 0);
        }


        [TestMethod]
        public void When_Found_AnyFlight_Should_Response_True_And_OneFlight()
        {
            var request = new SearchFlightRequestDto()
            {
                Destination = "NY",
                Origin = "CDG",
                DateFrom = DateTime.Today.AddHours(12),
                DateTo = DateTime.Today.AddDays(3)
            };

            var flights = new List<Flight>()
            {
               new Flight() { Id = 1, Destination = "LA", Origin = "NY", DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20) },
               new Flight() { Id = 1, Destination = "NY", Origin = "CDG" ,DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20)  },
            };

            var response = new SearchFlightUseCase(new FakeFlightRepository(flights)).Execute(request);

            Assert.IsTrue(response.Succes);
            Assert.AreEqual(response.Flights.Count, 1);
        }

        [TestMethod]
        public void When_Found_AnyFlight_WrongDate_Should_Response_False()
        {
            var request = new SearchFlightRequestDto()
            {
                Destination = "NY",
                Origin = "CDG",
                DateFrom = DateTime.Today.AddHours(12),
                DateTo = DateTime.Today.AddHours(16)
            };

            var flights = new List<Flight>()
            {
               new Flight() { Id = 1, Destination = "LA", Origin = "NY", DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20) },
               new Flight() { Id = 1, Destination = "NY", Origin = "CDG" ,DepartureTime = DateTime.Today.AddHours(14), ArrivalTime =DateTime.Today.AddHours(20)  },
            };

            var response = new SearchFlightUseCase(new FakeFlightRepository(flights)).Execute(request);

            Assert.IsFalse(response.Succes);
        }
    }
}