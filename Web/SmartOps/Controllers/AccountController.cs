using Microsoft.AspNetCore.Mvc;

namespace SmartOps.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
