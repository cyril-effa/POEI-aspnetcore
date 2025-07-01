using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;

namespace ex10bis.Core.Delivery.UseCases
{
    public class DeleteDeliveryUseCase(IDeliveryRepository deliveryRepository) : IDeleteDeliveryUseCase
    {
        public Task<DeleteDeliveryResponse> Execute(DeleteDeliveryRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteDeliveryResponse(false, "Invalid request"));
            }
            var delivery = deliveryRepository.GetByIdAsync(request.Id).Result;
            if (delivery == null)
            {
                return Task.FromResult(new DeleteDeliveryResponse(false, "Delivery not found"));
            }
            deliveryRepository.DeleteAsync(delivery);
            return Task.FromResult(new DeleteDeliveryResponse(true, "Delivery deleted successfully"));
        }
    }
}
