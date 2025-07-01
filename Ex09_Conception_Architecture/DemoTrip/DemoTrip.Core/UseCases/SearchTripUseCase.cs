using DemoTrip.Core.Entities;
using DemoTrip.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.UseCases
{
    public class SearchTripUseCase : ISearchTripUseCase
    {
        private readonly IFlightRepository flightRepository;
        private readonly IHotelRepository hotelRepository;

        public SearchTripUseCase(IFlightRepository flightRepository, IHotelRepository hotelRepository)
        {
            this.flightRepository = flightRepository;
            this.hotelRepository = hotelRepository;
        }

        public SearchTripResponseDto Execute(SearchTripRequestDto request)
        {
            var flights = flightRepository.GetAll();
            var hotels = hotelRepository.GetAll();

            var flightFound = flights.Where(flight => flight.Destination == request.Destination && flight.Origin == request.Origin
                                         && flight.DepartureTime >= request.DateFrom && flight.ArrivalTime <= request.DateTo).ToList();
            Trip tripFound = null;

            foreach (var flight in flightFound)
            {
                var hotelFound = hotels.Where(hotel => hotel.Location == request.Destination
                                           && hotel.CheckInDate <= flight.ArrivalTime && hotel.CheckOutDate >= request.DateTo).ToList();

                if (hotelFound.Any())
                {
                    tripFound = new Trip()
                    {
                        Id = Random.Shared.Next(10000,99999),
                        Name = "MyTrip",
                        Flight = flight,
                        Hotel = hotelFound.FirstOrDefault(),
                    };
                    break;
                }
            }

            return new SearchTripResponseDto()
            {
                Succes = tripFound != null,
                Trip = tripFound,
                NumberOfNights = tripFound != null ? (request.DateTo - tripFound.Flight.ArrivalTime).Days : 0
            };
        }
    }
}
