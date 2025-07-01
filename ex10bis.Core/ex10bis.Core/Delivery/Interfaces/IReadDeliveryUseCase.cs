using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IReadDeliveryUseCase
    {
        Task<ReadDeliveryResponse> Execute(ReadDeliveryRequest request);
    }
}