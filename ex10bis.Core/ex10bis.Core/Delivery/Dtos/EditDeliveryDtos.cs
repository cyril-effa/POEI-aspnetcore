using ex10bis.Core.Entities;

namespace ex10bis.Core.Delivery.Dtos
{
    public record EditDeliveryRequest(
        int Id,
        int OrderId,
        Entities.Order Order,
        string LivreurId,
        List<DeliverySlot> DeliverySlots);
    public record EditDeliveryResponse(
        bool Success,
        string Response,
        Entities.Delivery? Delivery);
}
