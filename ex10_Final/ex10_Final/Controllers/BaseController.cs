using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ex10_Final.Controllers
{
    [Authorize()]
    public class BaseController : Controller
    {

    }
}
