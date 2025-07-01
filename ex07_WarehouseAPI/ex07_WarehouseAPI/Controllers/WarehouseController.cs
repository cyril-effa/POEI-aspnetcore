using ex07_WarehouseAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex07_WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public WarehouseController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Récupère la liste de tous les entrepots
        /// </summary>
        /// <returns>La liste des entrepots</returns>
        [HttpGet("AllWarehouses")]
        public IActionResult GetAll()
        {
            return Ok(context.Warehouses.Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ToList());
        }
    }
}
