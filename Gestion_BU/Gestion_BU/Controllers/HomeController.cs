using Microsoft.AspNetCore.Mvc;

namespace Gestion_BU.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
     }
}
