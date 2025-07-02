using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;
using ex10bis.Core.Order.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ex10bis.Web.Controllers
{
    [Authorize(Roles = "livreur,magasinier")]
    public class DeliveryController (IDeliveryRepository deliveryRepository, IServiceDeliveryUseCase serviceDeliveryUseCase, ICreateDeliveryUseCase createDeliveryUseCase, IOrderRepository orderRepository, ICustomerRepository customerRepository, IFactureRepository factureRepository, UserManager<IdentityUser> userManager) : BaseController
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

        public async Task<IActionResult> ConfirmDelivery(int id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
                return NotFound("DeliveryAssignment not found");
            if (delivery.LivreurId != userManager.GetUserId(User))
                return Forbid();
            if (delivery.OrderId == 0)
                return NotFound("Order not found for this delivery");
            var order = delivery.Order;
            if (order == null)
                return NotFound("Order not found for this delivery");
            var customer = await customerRepository.GetByIdAsync(order.CustomerId);
            if (customer == null)
                return NotFound("Customer not found for this order");

            var response = await serviceDeliveryUseCase.ConfirmDelivery(new ConfirmDeliveryRequest(
                Order: order,
                Customer: customer
            ));

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelDelivery(int id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
                return NotFound("DeliveryAssignment not found");
            if (delivery.LivreurId != userManager.GetUserId(User))
                return Forbid();
            if (delivery.OrderId == 0)
                return NotFound("Order not found for this delivery");
            var order = delivery.Order;
            if (order == null)
                return NotFound("Order not found for this delivery");

            var response = await serviceDeliveryUseCase.CancelDelivery(new CancelDeliveryRequest(
                Order: order
            ));

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
