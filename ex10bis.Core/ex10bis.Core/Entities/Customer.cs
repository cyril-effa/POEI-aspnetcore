namespace ex10bis.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
