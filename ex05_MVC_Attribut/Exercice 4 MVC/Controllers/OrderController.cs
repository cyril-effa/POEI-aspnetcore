using BO;
using Exercice_5_MVC.Service;
using Exercice_5_MVC.Service.Exercice_4_MVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Exercice_5_MVC.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService;

        public OrderController()
        {
            orderService = new OrderService();
        }

        // GET: OrderController
        public ActionResult Index()
        {
            return View(orderService.GetOrders().Select(x => Mapping.ToOrderViewModel(x)));
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var foundOrder = orderService.GetOrders().FirstOrDefault(w => w.Id == id);

            return View(Mapping.ToOrderViewModel(foundOrder));
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Order order = new Order();
                ApplyFormCollectionToOrder(collection, order);
                orderService.Add(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void ApplyFormCollectionToOrder(IFormCollection collection, Order foundOrder)
        {
            foreach (var field in collection)
            {
                if (field.Key == nameof(foundOrder.Id))
                {
                    foundOrder.Id = int.Parse(field.Value);
                }
                else if (field.Key == nameof(foundOrder.CustomerName))
                {
                    foundOrder.CustomerName = field.Value;
                }
                else if (field.Key == nameof(foundOrder.Email))
                {
                    foundOrder.Email = field.Value;
                }
                else if (field.Key == nameof(foundOrder.ShippingAddress))
                {
                    foundOrder.ShippingAddress = field.Value;
                }
                else if (field.Key == nameof(foundOrder.OrderDate))
                {
                    foundOrder.OrderDate = DateTime.Parse(field.Value);
                }
                else if (field.Key == nameof(foundOrder.TotalAmount))
                {
                    foundOrder.TotalAmount = double.Parse(field.Value);
                }
                else if (field.Key == nameof(foundOrder.OrderStatus))
                {
                    foundOrder.OrderStatus = field.Value;
                }
                else if (field.Key == nameof(foundOrder.OrderDetails))
                {
                    // Désérialisation JSON en List<OrderDetail>
                    foundOrder.OrderDetails = JsonSerializer.Deserialize<List<OrderDetail>>(field.Value);
                }
                else if (field.Key == nameof(foundOrder.WarehouseId))
                {
                    foundOrder.WarehouseId = int.Parse(field.Value);
                }
            }
        }


        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var foundOrder = orderService.GetOrders().FirstOrDefault(w => w.Id == id);

            return View(Mapping.ToOrderViewModel(foundOrder));
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var foundOrder = orderService.GetOrders().FirstOrDefault(w => w.Id == id);
                if (foundOrder is null)
                {
                    throw new Exception();
                }
                ApplyFormCollectionToOrder(collection, foundOrder);
                orderService.Update(foundOrder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            var foundOrder = orderService.GetOrders().FirstOrDefault(w => w.Id == id);

            return View(Mapping.ToOrderViewModel(foundOrder));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var foundOrder = orderService.GetOrders().FirstOrDefault(w => w.Id == id);
                if (foundOrder != null)
                {
                    orderService.Remove(foundOrder);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
