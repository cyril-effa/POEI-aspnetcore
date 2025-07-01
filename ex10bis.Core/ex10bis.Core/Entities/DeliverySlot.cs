namespace ex10bis.Core.Entities
{
    public class DeliverySlot
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool IsAvailable { get; set; }
        public List<Delivery> Assignments { get; set; } = new();
    }
}
