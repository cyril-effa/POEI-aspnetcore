using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Delivery.UseCases
{
    public class DeliveryUseCase(IDeliveryRepository deliveryRepository, IOrderRepository orderRepository, IFactureRepository factureRepository, IFactureService factureService) : IDeliveryUseCase
    {
        public async Task<CreateDeliveryResponse> Create(CreateDeliveryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var delivery = new Entities.Delivery
            {
                OrderId = request.OrderId,
                Order = request.Order,
                LivreurId = request.LivreurId,
                DeliverySlots = request.DeliverySlots
            };
            await deliveryRepository.AddAsync(delivery);
            return new CreateDeliveryResponse(true, "Delivery created successfully", delivery);
        }

        public async Task<ConfirmDeliveryResponse> ConfirmDelivery(ConfirmDeliveryRequest request)
        {
            // Génération de la facture PDF
            var order = request.Order;
            var facture = new Facture
            {
                NumeroFacture = $"{DateTime.Now:yyyy-MM}-{order.Id}",
                OrderId = order.Id,
                Date = DateTime.Now
            };

            var factureFileName = $"Facture_{facture.NumeroFacture}.pdf";
            var facturePath = Path.Combine("wwwroot", "factures", factureFileName);
            facture.FilePath = "/factures/" + factureFileName;
            factureService.GenerateFacture(facturePath, facture, order);
            await factureRepository.AddAsync(facture);

            // Lier la facture à la commande
            order.OrderStatus = OrderStatus.Delivered;
            order.Facture = facture;
            await orderRepository.UpdateAsync(order);

            // Envoi du mail avec pièce jointe
            var customer = request.Customer;
            factureService.SendFactureByEmail(
                        customer.Email,
                        "Votre facture de livraison",
                        $"Bonjour {customer.Name},<br/>Veuillez trouver en pièce jointe la facture de votre commande n°{order.Id}.",
                        await File.ReadAllBytesAsync(facturePath),
                        facturePath);

            return new ConfirmDeliveryResponse(true, "Delivery confirmed successfully");
        }

        public async Task<CancelDeliveryResponse> CancelDelivery(CancelDeliveryRequest request)
        {
            var order = request.Order;
            order.OrderStatus = OrderStatus.Cancelled;
            await orderRepository.UpdateAsync(order);

            return new CancelDeliveryResponse(true, "Delivery cancelled successfully");
        }
    }
}
