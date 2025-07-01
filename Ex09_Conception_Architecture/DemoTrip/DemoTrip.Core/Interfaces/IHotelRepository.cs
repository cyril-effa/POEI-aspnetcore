using DemoTrip.Core.Entities;

namespace DemoTrip.Core.Interfaces
{
    public interface IHotelRepository
    {
        List<Hotel> GetAll();
    }
}