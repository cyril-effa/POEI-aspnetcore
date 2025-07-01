

using DemoTrip.Core.Interfaces;

namespace DemoTrip.Core.UseCases
{
    public class SearchFlightUseCase : ISearchFlightUseCase
    {
        private readonly IFlightRepository flightRepository;

        public SearchFlightUseCase(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }


        public SearchFlightResponseDto Execute(SearchFlightRequestDto request)
        {
            var flights =  flightRepository.GetAll();

            var flyFound =  flights.Where(fly => fly.Origin == request.Origin && fly.Destination == request.Destination
                                       && fly.DepartureTime >= request.DateFrom && fly.ArrivalTime <= request.DateTo).ToList();

            return new SearchFlightResponseDto()
            {
                Flights = flyFound,
                Succes = flyFound.Any()
            };
        }
    }
}
