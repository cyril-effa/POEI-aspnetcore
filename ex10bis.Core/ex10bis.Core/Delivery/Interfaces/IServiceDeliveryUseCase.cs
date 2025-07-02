using ex10bis.Core.Delivery.Dtos;

namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IServiceDeliveryUseCase
    {
        Task<ConfirmDeliveryResponse> ConfirmDelivery(ConfirmDeliveryRequest request);
        Task<CancelDeliveryResponse> CancelDelivery(CancelDeliveryRequest request);
    }
}
