using DemoTrip.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTrip.Core.Interfaces
{
    public interface ISearchTripUseCase
    {
        SearchTripResponseDto Execute(SearchTripRequestDto request);
    }
}
