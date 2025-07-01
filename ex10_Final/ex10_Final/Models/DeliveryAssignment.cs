namespace ex10_Final.Models
{
    public class DeliveryAssignment
    {
        public int Id { get; set; }
        public int DeliverySlotId { get; set; }
        public int OrderId { get; set; }
        public string LivreurId { get; set; }

        public List<DeliverySlot> DeliverySlots { get; set; } // Un assignment chevauche plusieurs créneaux
    }
}