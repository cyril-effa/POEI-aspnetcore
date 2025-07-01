using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;

namespace ex10bis.Core.Delivery.UseCases
{
    public class ReadDeliveryUseCase(IDeliveryRepository deliveryRepository) : IReadDeliveryUseCase
    {
        public Task<ReadDeliveryResponse> Execute(ReadDeliveryRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadDeliveryResponse(false, "Invalid request", null));
            }
            var delivery = deliveryRepository.GetByIdAsync(request.Id).Result;
            if (delivery == null)
            {
                return Task.FromResult(new ReadDeliveryResponse(false, "Delivery not found", null));
            }
            return Task.FromResult(new ReadDeliveryResponse(true, "Delivery retrieved successfully", delivery));
        }
    }
}
