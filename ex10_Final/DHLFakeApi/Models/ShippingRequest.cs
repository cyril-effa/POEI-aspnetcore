namespace DHLFakeApi.Models
{
    public class ShippingRequest
    {
        public string Adresse { get; set; }
        public int CodePostal { get; set; }
        public List<string> Articles { get; set; }
    }
}
