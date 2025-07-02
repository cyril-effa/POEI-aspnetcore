using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;

namespace ex10bis.Infrastructure.Services
{
    public class FactureService : IFactureService
    {
        public void GenerateFacture(string facturePath, Facture facture, Order order)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(facturePath));

            var content = new string[]
                {
                    $"Facture n° {facture.NumeroFacture}",
                    $"Date : {facture.Date:dd/MM/yyyy}",
                    $"Commande n° {order.Id}",
                    $"Client : {order.CustomerId}",
                    "Articles :"
                }.Concat(order.OrderDetails.Select(detail =>
                    $"   - {detail.Article.Name} ({detail.UnitPrice:C2}) x {detail.Quantity} = {detail.Quantity * detail.UnitPrice:C2}"))
                .Concat(new[]
                {
                    $"Coût de livraison : {order.ShippingCost:C2}",
                    $"Montant total : {order.TotalAmount:C2}",
                    "Merci pour votre commande !"
                }).ToArray();

            PDFService.GeneratePDF(facturePath, content);
        }

        public void SendFactureByEmail(string to, string subject, string body, byte[] facture, string facturePath)
        {
            if (File.Exists(facturePath))
            {
                EmailService.SendFakeMailWithAttachment(to, subject, body, facture, Path.GetFileName(facturePath));
            }
            else
            {
                throw new FileNotFoundException("Le fichier de facture n'existe pas.", facturePath);
            }
        }
    }
}
