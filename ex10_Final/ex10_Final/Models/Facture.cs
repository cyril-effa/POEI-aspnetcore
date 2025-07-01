namespace ex10_Final.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public string NumeroFacture { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; }
    }
}
