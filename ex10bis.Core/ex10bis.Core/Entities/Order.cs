namespace ex10bis.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        // Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Warehouse
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        // Delivery
        public Delivery? Delivery { get; set; }

        // Facture
        public Facture? Facture { get; set; }

        // Order details
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // Shipping information
        public double ShippingCost { get; set; }
        public int ShippingDuration { get; set; }

        public double TotalAmount => OrderDetails?.Sum(od => (double)(od.Quantity * od.UnitPrice)) + ShippingCost ?? 0;
    }

    public enum OrderStatus
    {
        Submitted,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
