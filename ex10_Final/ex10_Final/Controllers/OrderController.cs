using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ex10_Final.Data;
using ex10_Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ex10_Final.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
                return NotFound();

            var orderDetails = _context.OrderDetail
                .Include(od => od.Article)
                .Where(od => od.OrderId == id)
                .ToList();
            ViewBag.OrderDetails = order.OrderDetails;

            // Récupérer l'affectation de livraison pour cette commande
            var assignment = await _context.DeliveryAssignment
                .Include(a => a.DeliverySlots)
                .FirstOrDefaultAsync(a => a.OrderId == order.Id);

            string livreurName = null;
            string horaireLivraison = null;
            if (assignment != null)
            {
                // Récupérer le livreur (IdentityUser) par son Id
                var livreur = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == assignment.LivreurId);
                if (livreur != null)
                {
                    livreurName = livreur.UserName; // ou .Name si tu as une propriété Name
                }

                // Calcul des horaires de début et de fin
                var slots = assignment.DeliverySlots.OrderBy(s => s.DateAndTime).ToList();
                if (slots.Any())
                {
                    horaireLivraison = slots.First().DateAndTime.ToString("dd/MM/yyyy HH:mm")
                        + " -> " + slots.Last().DateAndTime.ToString("dd/MM/yyyy HH:mm");
                }
            }

            ViewBag.LivreurName = livreurName;
            ViewBag.HoraireLivraison = horaireLivraison;

            var allDays = _context.DeliverySlot
                .Select(s => s.DateAndTime.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            var slotsByDay = allDays.ToDictionary(
                day => day,
                day => _context.DeliverySlot.Where(s => s.DateAndTime.Date == day).OrderBy(s => s.DateAndTime).ToList()
            );

            ViewBag.AllDays = allDays;
            ViewBag.SlotsByDay = slotsByDay;

            var facturePath = _context.Facture
                .Where(f => f.OrderId == order.Id)
                .Select(f => f.FilePath)
                .FirstOrDefault();

            ViewBag.FacturePath = facturePath;

            return View(order);
        }

        // GET: Order/Create
        [Authorize(Roles = "customer")]
        public IActionResult Create()
        {
            var order = new Order();

            // Charger l'identifiant de l'utilisateur connecté
            order.CustomerId = _context.Customer
                .Where(c => c.UserId == _userManager.GetUserId(User))
                .Select(c => c.Id)
                .FirstOrDefault();

            order.OrderDetails.Add(new OrderDetail());

            // Charger la liste des entrepôts
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name");
            ViewBag.Articles = new SelectList(_context.Article.ToList(), "Id", "Name");
            ViewBag.ArticlesPrice = _context.Article.ToDictionary(a => a.Id.ToString(), a => a.Price);

            return View(order);
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,OrderDate,OrderStatus,WarehouseId,OrderDetails")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDetails = order.OrderDetails?.Where(od => od.ArticleId != 0 && od.Quantity > 0).ToList() ?? new List<OrderDetail>();

                order.DureeLivraison = Random.Shared.Next(0, 120);

                // Associer chaque OrderDetail à l'Order courant
                foreach (var detail in order.OrderDetails)
                {
                    detail.Order = order; // Optionnel, pour EF
                    detail.OrderId = order.Id; // Sera mis à jour après SaveChanges si Id est auto-incrémenté
                }

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repasser les listes pour la vue en cas d'erreur
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name");
            ViewBag.Articles = new SelectList(_context.Article.ToList(), "Id", "Name");
            ViewBag.ArticlesPrice = _context.Article.ToDictionary(a => a.Id.ToString(), a => a.Price);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var customerId = _context.Customer
                .Where(c => c.UserId == _userManager.GetUserId(User))
                .Select(c => c.Id)
                .FirstOrDefault();

            if (order.CustomerId != customerId || order.OrderStatus != OrderStatus.Submitted)
            {
                return Forbid();
            }

            // Charger la liste des entrepôts
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name");

            ViewBag.OrderStatusList = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OrderDate,OrderStatus,WarehouseId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var customerId = _context.Customer
                .Where(c => c.UserId == _userManager.GetUserId(User))
                .Select(c => c.Id)
                .FirstOrDefault();

            if (order.CustomerId != customerId || order.OrderStatus != OrderStatus.Submitted)
            {
                return Forbid();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        [Authorize(Roles = "magasinier")]
        [HttpPost]
        public async Task<IActionResult> Process(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
                return NotFound();

            // Appel à l'API DHLFakeApi
            var shippingResponse = await CallShippingApiAsync(order, _context.Customer.FirstOrDefault(c => c.Id == order.CustomerId));
            if (shippingResponse == null)
            {
                TempData["Error"] = "Erreur lors de l'appel à l'API de livraison.";
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }

            order.ShippingCost = shippingResponse.Cost;

            order.OrderStatus = OrderStatus.Processing;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        private async Task<ShippingResponse?> CallShippingApiAsync(Order order, Customer customer)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7108"); // Adaptez le port si besoin

            var request = new ShippingRequest
            {
                Adresse = customer.Address,
                CodePostal = int.TryParse(customer.City, out var cp) ? cp : -1,
                Articles = order.OrderDetails.Select(od => $"{od.Quantity}x {od.Article.Name}").ToList()
            };

            var response = await httpClient.PostAsJsonAsync("/api/shipping", request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ShippingResponse>();
        }

        [Authorize(Roles = "magasinier")]
        [HttpPost]
        public async Task<IActionResult> PlanLivraison(int id, int slotId)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return NotFound();

            var slot = await _context.DeliverySlot.FindAsync(slotId);
            if (slot == null)
                return NotFound();

            var startTime = slot.DateAndTime;
            var endTime = startTime.AddMinutes(order.DureeLivraison + 60);
            var finMatin = new TimeSpan(12, 30, 0);
            var finAprem = new TimeSpan(17, 30, 0);

            if (startTime.TimeOfDay < finMatin && endTime.TimeOfDay > finMatin)
                endTime = endTime.AddHours(1);
            if (startTime.TimeOfDay < finAprem && endTime.TimeOfDay > finAprem)
                endTime = endTime.AddHours(14.5);

            var slotsToUpdate = await _context.DeliverySlot
                .Where(s => s.DateAndTime >= startTime && s.DateAndTime < endTime)
                .ToListAsync();

            // Récupérer tous les livreurs
            var livreursIds = await _userManager.GetUsersInRoleAsync("Livreur");

            // Récupérer les livreurs déjà occupés sur un créneau qui chevauche
            var busyLivreursIds = new List<string>();
            foreach (var s in slotsToUpdate)
            {
                var allAssignments = await _context.DeliveryAssignment
                    .Include(a => a.DeliverySlots)
                    .ToListAsync();

                foreach (var a in allAssignments)
                {
                    foreach (var ds in a.DeliverySlots)
                    {
                        if (ds.DateAndTime > startTime && ds.DateAndTime < endTime)
                        {
                            if (!busyLivreursIds.Contains(a.LivreurId))
                            {
                                busyLivreursIds.Add(a.LivreurId);
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
            var assignment = new DeliveryAssignment
            {
                DeliverySlotId = slot.Id,
                OrderId = order.Id,
                LivreurId = availableLivreursIds[Random.Shared.Next(availableLivreursIds.Count)],
                DeliverySlots = slotsToUpdate
            };
            _context.DeliveryAssignment.Add(assignment);

            // Marquer les slots comme indisponibles
            foreach (var s in slotsToUpdate)
            {
                // Comparer le nombre d'assignements avec le nombre de livreurs disponibles
                if (s.Assignments.Count >= livreursIds.Count)
                {
                    s.IsAvailable = false; // Marquer le créneau comme indisponible si tous les livreurs sont occupés
                }
            }

            // Mettre à jour le statut de la commande
            order.OrderStatus = OrderStatus.Shipped;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }
    }
}
