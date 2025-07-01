using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex07_WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OrderController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Ajoute une nouvelle commande à la liste des commandes.
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                OrderStatus = orderDto.OrderStatus,
                WarehouseId = orderDto.WarehouseId,
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    ArticleId = od.ArticleId,
                    Quantity = od.Quantity
                }).ToList()
            };

            context.Orders.Add(order);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order.Id);
        }

        /// <summary>
        /// Récupère une commande par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la commande</param>
        /// <returns>Retourne la commande</returns>
        [HttpGet("GetOrder/{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .Select(o => new
                {
                    Id = o.Id,
                    Customer = o.CustomerId,
                    Warehouse = o.WarehouseId,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    OrderDetails = o.OrderDetails.Select(od => new
                    {
                        Id = od.Id,
                        OrderId = od.OrderId,
                        ArticleId = od.ArticleId,
                        Article = od.Article,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    }),
                    TotalAmount = o.TotalAmount
                })
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .Select(o => new
                {
                    Id = o.Id,
                    Customer = o.CustomerId,
                    Warehouse = o.WarehouseId,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    OrderDetails = o.OrderDetails.Select(od => new
                    {
                        Id = od.Id,
                        OrderId = od.OrderId,
                        ArticleId = od.ArticleId,
                        Article = od.Article,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    }),
                    TotalAmount = o.TotalAmount
                })
                .ToList();
            return Ok(orders);
        }

        /// <summary>
        /// Met à jour le statut d'une commande.
        /// </summary>
        /// <param name="orderId">Identifiant de la commande</param>
        /// <param name="status">Status de la commande</param>
        /// <returns></returns>
        [HttpPatch("UpdateOrderStatus")]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = context.Orders.Find(orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = status;
            context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Supprime une commande par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la commande</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }
            context.Orders.Remove(order);
            context.SaveChanges();

            return NoContent();

        }
    }
}
