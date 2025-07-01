using DemoTrip.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.UseCases
{
    public class SearchHotelUseCase : ISearchHotelUseCase
    {
        private readonly IHotelRepository hotelRepository;

        public SearchHotelUseCase(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public SearchHotelResponseDto Execute(SearchHotelRequestDto request)
        {
            var hotels = hotelRepository.GetAll();

            var hotelFound = hotels.Where(hotel => hotel.Location == request.Location && hotel.CheckInDate <= request.DateFrom && hotel.CheckOutDate >= request.DateTo).ToList();

            return new SearchHotelResponseDto()
            {
                Hotels = hotelFound,
                Succes = hotelFound.Any()
            };
        }
    }
}
