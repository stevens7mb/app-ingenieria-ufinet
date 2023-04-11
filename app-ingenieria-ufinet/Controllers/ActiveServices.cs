using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class ActiveServices : Controller
    {
        public IActionResult TablaDatos()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
