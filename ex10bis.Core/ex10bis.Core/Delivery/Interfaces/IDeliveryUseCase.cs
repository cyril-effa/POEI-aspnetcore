using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IDeliveryUseCase
    {
        Task<CreateDeliveryResponse> Create(CreateDeliveryRequest request);
        Task<ConfirmDeliveryResponse> ConfirmDelivery(ConfirmDeliveryRequest request);
        Task<CancelDeliveryResponse> CancelDelivery(CancelDeliveryRequest request);
    }
}
