namespace DemoTrip.Core.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
    }
}
