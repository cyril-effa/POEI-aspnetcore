using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IEditDeliveryUseCase
    {
        Task<EditDeliveryResponse> Execute(EditDeliveryRequest request);
    }
}