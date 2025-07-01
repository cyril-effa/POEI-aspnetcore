using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;

namespace ex10bis.Core.Delivery.UseCases
{
    public class CreateDeliveryUseCase(IDeliveryRepository deliveryRepository) : ICreateDeliveryUseCase
    {
        public async Task<CreateDeliveryResponse> Execute(CreateDeliveryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var delivery = new Entities.Delivery
            {
                OrderId = request.OrderId,
                Order = request.Order,
                LivreurId = request.LivreurId,
                DeliverySlots = request.DeliverySlots
            };
            await deliveryRepository.AddAsync(delivery);
            return new CreateDeliveryResponse(true, "Delivery created successfully", delivery);
        }
    }
}