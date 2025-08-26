using WarehousePjt.Core.Deliveries.Dtos;

namespace WarehousePjt.Core.Deliveries.Interfaces
{
    public interface IDeliveryUseCase
    {
        Task<CreateDeliveryResponse> Create(CreateDeliveryRequest request);
        Task<ConfirmDeliveryResponse> ConfirmDelivery(ConfirmDeliveryRequest request);
        Task<CancelDeliveryResponse> CancelDelivery(CancelDeliveryRequest request);
    }
}
