using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Deliveries.Dtos
{
    public record CreateDeliveryRequest(
        int OrderId,
        Order Order,
        string LivreurId,
        List<DeliverySlot> DeliverySlots);
    public record CreateDeliveryResponse(
            bool Success,
            string Response,
            Delivery? Delivery);

    public record ConfirmDeliveryRequest(
        Order Order,
        Customer Customer);
    public record ConfirmDeliveryResponse(
        bool Success,
        string Message);

    public record CancelDeliveryRequest(
        Order Order);
    public record CancelDeliveryResponse(
        bool Success,
        string Message);
}
