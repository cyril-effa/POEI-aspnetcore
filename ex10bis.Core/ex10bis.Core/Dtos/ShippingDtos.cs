namespace ex10bis.Core.Dtos
{
    public record ShippingRequest(string Adresse, int CodePostal, List<string> Articles);
    public record ShippingResponse(int ShippingNumber, double Cost, string Status);
}
