using ex10bis.Core.Entities;

namespace ex10bis.Core.Delivery.Dtos
{
    public record CreateDeliveryRequest(
        int OrderId,
        Entities.Order Order,
        string LivreurId,
        List<DeliverySlot> DeliverySlots);
    public record CreateDeliveryResponse(
            bool Success,
            string Response,
            Entities.Delivery? Delivery);
}
