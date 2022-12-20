using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class IndicadoresController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult TablaDeDatos()
        {
            return View();
        }
    }
}
