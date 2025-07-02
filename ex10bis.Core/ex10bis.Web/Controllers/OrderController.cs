using ex10bis.Core.Article.Interfaces;
using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Delivery.Dtos;
using ex10bis.Core.Delivery.Interfaces;
using ex10bis.Core.Dtos;
using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;
using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;
using ex10bis.Core.Warehouse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ex10bis.Web.Controllers
{
    public class OrderController(IOrderRepository orderRepository, ICrudOrderUseCase crudOrderUseCase, IServiceOrderUseCase serviceOrderUseCase, ICustomerRepository customerRepository, IWarehouseRepository warehouseRepository, IArticleRepository articleRepository,
                                 IDeliverySlotRepository deliverySlotRepository, IDeliveryRepository deliveryRepository, ICreateDeliveryUseCase createDeliveryUseCase, UserManager<IdentityUser> userManager) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var orders = await orderRepository.ListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var response = await crudOrderUseCase.Read(new ReadOrderRequest(id.Value));
            if (!response.Success) return NotFound();

            ViewBag.LivreurName = await GetLivreurName(response.Order);
            ViewBag.DeliveryHours = GetDeliveryHours(response.Order);
            ViewBag.AllDays = await GetAllDaysFromDeliverySlots();
            ViewBag.SlotsByDay = await GetSlotsByDay();

            return View(response.Order);
        }

        private async Task<string> GetLivreurName(Order order)
        {
            if (order.Delivery == null) return null;
            var userId = userManager.GetUserId(User);
            var livreur = await userManager.FindByIdAsync(order.Delivery.LivreurId);
            return livreur.UserName;
        }

        private string GetDeliveryHours(Order order)
        {
            if (order.Delivery == null) return null;
            var slots = order.Delivery.DeliverySlots.OrderBy(s => s.DateAndTime).ToList();
            if (slots.Any())
            {
                return slots.First().DateAndTime.ToString("dd/MM/yyyy HH:mm")
                    + " -> " + slots.Last().DateAndTime.ToString("dd/MM/yyyy HH:mm");
            }
            return null;
        }

        private async Task<List<DateTime>> GetAllDaysFromDeliverySlots()
        {
            var allDays = await deliverySlotRepository.ListAsync();
            return allDays.Select(s => s.DateAndTime.Date).Distinct().OrderBy(d => d).ToList();
        }

        private async Task<Dictionary<DateTime, List<DeliverySlot>>> GetSlotsByDay()
        {
            var allDays = await deliverySlotRepository.ListAsync();
            return allDays.GroupBy(s => s.DateAndTime.Date)
                          .ToDictionary(g => g.Key, g => g.OrderBy(s => s.DateAndTime).ToList());
        }

        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Create()
        {
            CreateOrderRequest request = new CreateOrderRequest( 0, null, 0, null, null, null, DateTime.Now, OrderStatus.Submitted, new List<OrderDetail>(), 0, 0);
            await SetViewBagsAsync();
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            /*
                // Associer chaque OrderDetail à l'Order courant
                foreach (var detail in order.OrderDetails)
                {
                    detail.OrderId = order.Id; // Sera mis à jour après SaveChanges si Id est auto-incrémenté
                    detail.Order = order; // Optionnel, pour EF
                }
            */

            var customer = await GetCustomerAsync();
            var newRequest = request with
            {
                CustomerId = customer.Id,
                Customer = customer,
                Delivery = null,
                Facture = null,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Submitted,
                OrderDetails = request.OrderDetails?.Where(od => od.ArticleId > 0 && od.Quantity > 0).ToList() ?? new List<OrderDetail>(),
                ShippingCost = 0,
                ShippingDuration = Random.Shared.Next(0, 120)
            };

            var response = await crudOrderUseCase.Create(newRequest);
            if (response.Success)
                return RedirectToAction(nameof(Index));

            await SetViewBagsAsync();
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var response = await crudOrderUseCase.Read(new ReadOrderRequest(id.Value));
            if (!response.Success) return NotFound();

            var customer = await GetCustomerAsync();
            if (customer == null) return Forbid();
            if (response.Order.CustomerId != customer.Id || response.Order.OrderStatus != OrderStatus.Submitted) return Forbid();

            var editRequest = new EditOrderRequest(
                response.Order.Id,
                response.Order.CustomerId,
                response.Order.Customer,
                response.Order.WarehouseId,
                response.Order.Warehouse,
                response.Order.Delivery,
                response.Order.Facture,
                response.Order.OrderDate,
                response.Order.OrderStatus,
                response.Order.OrderDetails,
                response.Order.ShippingCost,
                response.Order.ShippingDuration);

            await SetViewBagsAsync();
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditOrderRequest request)
        {
            var customer = await GetCustomerAsync();
            var newRequest = request with
            {
                CustomerId = customer.Id,
                Customer = customer,
                Delivery = null,
                Facture = null,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Submitted,
                OrderDetails = request.OrderDetails?.Where(od => od.ArticleId > 0 && od.Quantity > 0).ToList() ?? new List<OrderDetail>(),
                ShippingCost = 0,
                ShippingDuration = Random.Shared.Next(0, 120)
            };

            var response = await crudOrderUseCase.Edit(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var response = await crudOrderUseCase.Read(new ReadOrderRequest(id.Value));
            if (!response.Success) return NotFound();

            if (!userManager.GetRolesAsync(userManager.GetUserAsync(User).Result).Result.Contains("admin"))
            {
                var customer = await GetCustomerAsync();
                if (customer == null) return Forbid();
                if (response.Order.CustomerId != customer.Id || response.Order.OrderStatus != OrderStatus.Submitted) return Forbid();
            }

            await SetViewBagsAsync();
            return View(response.Order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteOrderRequest request)
        {
            var response = await crudOrderUseCase.Delete(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));

            await SetViewBagsAsync();
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        [Authorize(Roles = "magasinier")]
        [HttpPost]
        public async Task<IActionResult> Process(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            // Appel à l'API DHLFakeApi
            var shippingResponse = await CallShippingApiAsync(order, await customerRepository.GetByIdAsync(order.CustomerId));
            if (shippingResponse == null)
            {
                TempData["Error"] = "Erreur lors de l'appel à l'API de livraison.";
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }

            var processResponse = await serviceOrderUseCase.Process(new ProcessOrderRequest
            (
                Order: order,
                ShippingResponse: shippingResponse
            ));

            if (!processResponse.Success)
            {
                ModelState.AddModelError("", processResponse.Message);
                TempData["Error"] = "Erreur lors du traitement de la commande.";
            }

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        private async Task<ShippingResponse?> CallShippingApiAsync(Order order, Customer customer)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7031"); // Adaptez le port si besoin

            var request = new ShippingRequest(
                customer.Address,
                int.TryParse(customer.City, out var cp) ? cp : -1,
                order.OrderDetails.Select(od => $"{od.Quantity}x {od.Article.Name}").ToList()
            );

            var response = await httpClient.PostAsJsonAsync("/api/shippingApi", request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ShippingResponse>();
        }

        [Authorize(Roles = "magasinier")]
        [HttpPost]
        public async Task<IActionResult> PlanDelivery(int id, int slotId)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var slot = await deliverySlotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return NotFound();

            var startTime = slot.DateAndTime;
            var endTime = startTime.AddMinutes(order.ShippingDuration + 60);
            var finMatin = new TimeSpan(12, 30, 0);
            var finAprem = new TimeSpan(17, 30, 0);

            if (startTime.TimeOfDay < finMatin && endTime.TimeOfDay > finMatin)
                endTime = endTime.AddHours(1);
            if (startTime.TimeOfDay < finAprem && endTime.TimeOfDay > finAprem)
                endTime = endTime.AddHours(14.5);

            var slotsToUpdate = (await deliverySlotRepository.ListAsync())
                .Where(s => s.DateAndTime >= startTime && s.DateAndTime < endTime).ToList();

            // Récupérer tous les livreurs
            var livreursIds = await userManager.GetUsersInRoleAsync("Livreur");

            // Récupérer les livreurs déjà occupés sur un créneau qui chevauche
            var busyLivreursIds = new List<string>();
            foreach (var s in slotsToUpdate)
            {
                var deliveries = await deliveryRepository.ListAsync();

                foreach (var d in deliveries)
                {
                    foreach (var ds in d.DeliverySlots)
                    {
                        if (ds.DateAndTime > startTime && ds.DateAndTime < endTime)
                        {
                            if (!busyLivreursIds.Contains(d.LivreurId))
                            {
                                busyLivreursIds.Add(d.LivreurId);
                                break; // Ce livreur est déjà occupé, inutile de vérifier d'autres slots pour lui
                            }
                        }
                    }
                }
            }

            // Filtrer les livreurs disponibles
            var availableLivreursIds = livreursIds.Select(l => l.Id).Except(busyLivreursIds).ToList();

            if (!availableLivreursIds.Any())
            {
                // Aucun livreur disponible
                TempData["Error"] = "Aucun livreur disponible pour ce créneau.";
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }

            // Créer l'affectation
            var response = await createDeliveryUseCase.Execute(new CreateDeliveryRequest
            (
                OrderId: order.Id,
                Order: order,
                LivreurId: availableLivreursIds[Random.Shared.Next(availableLivreursIds.Count)],
                DeliverySlots: slotsToUpdate
            ));

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Response);
                TempData["Error"] = "Erreur lors de la création de la livraison.";
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }

            // Marquer les slots comme indisponibles
            foreach (var s in slotsToUpdate)
            {
                // Comparer le nombre d'assignements avec le nombre de livreurs disponibles
                if (s.Assignments.Count >= livreursIds.Count)
                {
                    s.IsAvailable = false; // Marquer le créneau comme indisponible si tous les livreurs sont occupés
                }
            }


            //var response = serviceOrderUseCase.PlanDelivery(new PlanDeliveryRequest(id, slotId));


            // Mettre à jour le statut de la commande
            order.OrderStatus = OrderStatus.Shipped;
            await orderRepository.UpdateAsync(order);
            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        private async Task<Customer> GetCustomerAsync()
        {
            var customers = await customerRepository.ListAsync();
            return customers.Where(c => c.UserId == userManager.GetUserId(User)).FirstOrDefault();
        }

        private async Task SetViewBagsAsync()
        {
            var warehouses = await warehouseRepository.ListAsync();
            ViewBag.Warehouses = new SelectList(warehouses, "Id", "Name");
            var articles = await articleRepository.ListAsync();
            ViewBag.Articles = new SelectList(articles, "Id", "Name");
            ViewBag.ArticlesPrice = articles.ToDictionary(a => a.Id.ToString(), a => a.Price);
        }
    }
}
