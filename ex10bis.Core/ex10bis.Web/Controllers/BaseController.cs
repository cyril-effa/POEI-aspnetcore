using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ex10bis.Web.Controllers
{
    [Authorize()]
    public class BaseController : Controller
    {

    }
}
