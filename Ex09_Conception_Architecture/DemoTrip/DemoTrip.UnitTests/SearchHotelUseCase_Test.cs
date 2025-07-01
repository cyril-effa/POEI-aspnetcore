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
    public class SearchHotelUseCase_Test
    {
        [TestMethod]
        public void When_NotFound_AnyHotel_Should_Response_False_And_Empty()
        {
            var request = new SearchHotelRequestDto()
            {
                Location = "NYC",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(2)
            };
            var response = new SearchHotelUseCase(new FakeHotelRepository(new List<Hotel>())).Execute(request);

            Assert.IsFalse(response.Succes);
            Assert.AreEqual(response.Hotels.Count, 0);
        }


        [TestMethod]
        public void When_Found_AnyHotel_Should_Response_True_And_OneHotel()
        {
            var request = new SearchHotelRequestDto()
            {
                Location = "NYC",
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(2)
            };

            var hotels = new List<Hotel>()
            {
                new Hotel() { Id = 1, Name = "Hotel A", Location = "NYC", CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(2) },
                new Hotel() { Id = 2, Name = "Hotel B", Location = "LAX", CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(3) }
            };

            var response = new SearchHotelUseCase(new FakeHotelRepository(hotels)).Execute(request);

            Assert.IsTrue(response.Succes);
            Assert.AreEqual(response.Hotels.Count, 1);
        }
    }
}
