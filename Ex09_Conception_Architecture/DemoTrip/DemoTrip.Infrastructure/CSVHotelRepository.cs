using DemoTrip.Core.Entities;
using DemoTrip.Core.Interfaces;

namespace DemoTrip.Infrastructure
{
    public class CSVHotelRepository : IHotelRepository
    {
        private readonly string csvFilePath;

        public CSVHotelRepository(string csvFilePath)
        {
            this.csvFilePath = csvFilePath;
        }

        public List<Hotel> GetAll()
        {
            var lines = File.ReadAllLines(csvFilePath);
            var output = new List<Hotel>();
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(';');
                
                Hotel hotel = new Hotel()
                {
                    // Id,Name,Location,CheckInDate,CheckOutDate,PricePerNight
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    Location = values[2],
                    CheckInDate = DateTime.Parse(values[3]),
                    CheckOutDate = DateTime.Parse(values[4]),
                    PricePerNight = decimal.Parse(values[5])
                };

                output.Add(hotel);
            }

            return output;
        }
    }
}