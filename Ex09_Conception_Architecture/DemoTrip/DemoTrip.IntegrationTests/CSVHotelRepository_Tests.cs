using DemoTrip.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.IntegrationTests
{
    [TestClass]
    public class CSVHotelRepository_Tests
    {
        [TestMethod]
        public void ReadCSV_Should_Return_Hotel()
        {

            var repo = new CSVHotelRepository("hotels.csv");

            var hotels = repo.GetAll();

            Assert.IsTrue(hotels.Count > 0);
        }
    }
}
