namespace ex10_Final.Models
{
    public class DeliverySlot
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool IsAvailable { get; set; }
        public List<DeliveryAssignment> Assignments { get; set; } = new(); // Un assignment par Livreur pour ce créneau
    }
}
