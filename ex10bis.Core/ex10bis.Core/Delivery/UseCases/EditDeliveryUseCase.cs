using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;

namespace ex10bis.Core.Delivery.UseCases
{
    public class EditDeliveryUseCase(IDeliveryRepository deliveryRepository) : IEditDeliveryUseCase
    {
        public Task<EditDeliveryResponse> Execute(EditDeliveryRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditDeliveryResponse(false, "Invalid request", null));
            }
            var delivery = deliveryRepository.GetByIdAsync(request.Id).Result;
            if (delivery == null)
            {
                return Task.FromResult(new EditDeliveryResponse(false, "Delivery not found", null));
            }
            delivery.OrderId = request.OrderId;
            delivery.Order = request.Order;
            delivery.LivreurId = request.LivreurId;
            delivery.DeliverySlots = request.DeliverySlots ?? new List<Entities.DeliverySlot>();
            deliveryRepository.UpdateAsync(delivery);
            return Task.FromResult(new EditDeliveryResponse(true, $"Delivery updated successfully", delivery));
        }
    }
}
