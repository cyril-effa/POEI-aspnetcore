using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ex_08.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {

    }
}
