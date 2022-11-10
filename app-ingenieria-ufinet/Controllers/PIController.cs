using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class PIController : Controller
    {
        public IActionResult PIList()
        {
            return View();
        }
    }
}
