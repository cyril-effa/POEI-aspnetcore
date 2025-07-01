namespace ex10bis.Core.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
