using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Delivery.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;
using ex10bis.Core.Order.Interfaces;
using ex10bis.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ex10bis.Web.Controllers
{
    [Authorize(Roles = "livreur,magasinier")]
    public class DeliveryController (IDeliveryRepository deliveryRepository, ICreateDeliveryUseCase createDeliveryUseCase, IOrderRepository orderRepository, ICustomerRepository customerRepository, IFactureRepository factureRepository, UserManager<IdentityUser> userManager) : BaseController
    {
        // GET: DeliveryAssignment
        public async Task<IActionResult> Index(string selectedLivreurId = null)
        {
            // Récupérer tous les livreurs
            var livreurs = await userManager.GetUsersInRoleAsync("livreur");
            var livreurList = livreurs
                .Select(l => new SelectListItem
                {
                    Value = l.Id,
                    Text = l.UserName
                })
                .ToList();

            // Pré-sélectionner l'utilisateur connecté si c'est un livreur
            var currentUser = await userManager.GetUserAsync(User);
            if (User.IsInRole("livreur"))
            {
                selectedLivreurId ??= currentUser.Id;
            }

            ViewBag.Livreurs = livreurList;
            ViewBag.SelectedLivreurId = selectedLivreurId;

            // Filtrer les livraisons
            var deliveries = await deliveryRepository.ListAsync();

            if (!string.IsNullOrEmpty(selectedLivreurId))
            {
                deliveries = deliveries.Where(a => a.LivreurId == selectedLivreurId).ToList();
            }

            // Récupérer les statuts des commandes associées
            var orderIds = deliveries.Select(a => a.OrderId).Distinct().ToList();
            var orderStatuses = (await orderRepository.ListAsync())
                .Where(o => orderIds.Contains(o.Id))
                .ToDictionary(o => o.Id, o => o.OrderStatus);

            ViewBag.OrderStatuses = orderStatuses;

            return View(deliveries);
        }

        public async Task<IActionResult> ConfirmerLivraison(int id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
                return NotFound("Delivery not found");

            if (delivery.LivreurId != userManager.GetUserId(User))
                return Forbid();

            // Modifier le status de la commande 

            var order = await orderRepository.GetByIdAsync(delivery.OrderId);
            if (order == null)
                return NotFound("Order not found");

            order.OrderStatus = OrderStatus.Delivered;

            // Génération de la facture PDF
            var facture = new Facture
            {
                NumeroFacture = $"{DateTime.Now:yyyy-MM}-{order.Id}",
                OrderId = order.Id,
                Date = DateTime.Now
            };

            var factureFileName = $"Facture_{facture.NumeroFacture}.pdf";
            var facturePath = Path.Combine("wwwroot", "factures", factureFileName);
            facture.FilePath = "/factures/" + factureFileName;
            FactureService.GenerateFacture(facturePath, facture, order);
            await factureRepository.AddAsync(facture);

            // Lier la facture à la commande
            
            order.Facture = facture;
            await orderRepository.UpdateAsync(order);

            // Envoi du mail avec pièce jointe
            var customer = await customerRepository.GetByIdAsync(order.CustomerId);
            if (customer == null)
                return NotFound("Customer not found");

            FactureService.SendFactureByEmail(
                        customer.Email,
                        "Votre facture de livraison",
                        $"Bonjour {customer.Name},<br/>Veuillez trouver en pièce jointe la facture de votre commande n°{order.Id}.",
                        await System.IO.File.ReadAllBytesAsync(facturePath),
                        facturePath);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AnnulerLivraison(int id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
                return NotFound("DeliveryAssignment not found");

            if (delivery.LivreurId != userManager.GetUserId(User))
                return Forbid();

            // Modifier le status de la commande 
            var order = await orderRepository.GetByIdAsync(delivery.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Cancelled;
                await orderRepository.UpdateAsync(order);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
