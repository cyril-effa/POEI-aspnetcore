using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IDeleteDeliveryUseCase
    {
        Task<DeleteDeliveryResponse> Execute(DeleteDeliveryRequest request);
    }
}