using DemoTrip.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.Interfaces
{
    public interface ISearchHotelUseCase
    {
        SearchHotelResponseDto Execute(SearchHotelRequestDto request);
    }
}
