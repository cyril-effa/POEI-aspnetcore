using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface ICreateDeliveryUseCase
    {
        Task<CreateDeliveryResponse> Execute(CreateDeliveryRequest request);
    }
}