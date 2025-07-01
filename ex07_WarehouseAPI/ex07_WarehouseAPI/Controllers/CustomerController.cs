using ex07_WarehouseAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex07_WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CustomerController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Récupère la liste de tous les clients, triés par montant total des commandes décroissant
        /// </summary>
        /// <returns>La liste des clients triés</returns>
        [HttpGet("GetAllByTotalAmount")]
        public IActionResult GetAllByTotalAmount()
        {
            var customers = context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .AsEnumerable()
                .Select(c => new
                {
                    c.Name,
                    c.Address,
                    NbCommands = c.Orders.Count,
                    TotalAmount = c.Orders.Sum(o => o.TotalAmount),
                    AverageAmount = c.Orders.Average(o => o.TotalAmount)
                }).ToList();

            customers.Sort((c1, c2) => c2.TotalAmount.CompareTo(c1.TotalAmount));

            return Ok(customers);
        }
    }
}
