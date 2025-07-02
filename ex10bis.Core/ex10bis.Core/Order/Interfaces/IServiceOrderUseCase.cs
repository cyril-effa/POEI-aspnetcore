using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface IServiceOrderUseCase
    {
        Task<ProcessOrderResponse> Process(ProcessOrderRequest request);

        Task<PlanDeliveryResponse> PlanDelivery(PlanDeliveryRequest request);
    }
}
