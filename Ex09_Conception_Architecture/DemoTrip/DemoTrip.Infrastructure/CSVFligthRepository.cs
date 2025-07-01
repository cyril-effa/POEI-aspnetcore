using DemoTrip.Core.Entities;
using DemoTrip.Core.Interfaces;

namespace DemoTrip.Infrastructure
{
    public class CSVFligthRepository : IFlightRepository
    {
        private readonly string csvFilePath;

        public CSVFligthRepository(string csvFilePath)
        {
            this.csvFilePath = csvFilePath;
        }

        public List<Flight> GetAll()
        {
            var lines = File.ReadAllLines(csvFilePath);
            var output = new List<Flight>();
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(';');

                Flight flight = new Flight()
                {
                    //Id,Origin,Destination,OriginName,DestinationName,DepartureTime,ArrivalTime,Price
                    Id = int.Parse(values[0]),
                    Origin = values[1],
                    Destination = values[2],
                    OriginName = values[3],
                    DestinationName = values[4],
                    DepartureTime = DateTime.Parse(values[5]),
                    ArrivalTime = DateTime.Parse(values[6]),
                    Price = decimal.Parse(values[7])
                };

                output.Add(flight);
            }

            return output;
        }
    }
}