namespace ex10bis.Core.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string LivreurId { get; set; }
        public List<DeliverySlot> DeliverySlots { get; set; }
    }
}