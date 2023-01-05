using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Repositories.Indicador;
using app_ingenieria_ufinet.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly IIndicadorRepository _indicadorRepository;

        public IndicadoresController(IIndicadorRepository indicadorRepository)
        {
            _indicadorRepository = indicadorRepository;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult TablaDeDatos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaFactibilidades()
        {
            List<FactibilidadModel> result = this._indicadorRepository.ListaFactibilidades();

            return Json(new { data = result });
        }

        [HttpGet]
        public JsonResult selectClientes()
        {
            List<ClienteModel> result = this._indicadorRepository.selectClientes();

            return Json(result);
        }

        [HttpGet]
        public JsonResult selectKAM()
        {
            List<KamModel> result = this._indicadorRepository.selectKAM();

            return Json(result);
        }

        [HttpPost]
        public JsonResult CrearFactibilidad([FromBody] FactibilidadRequestModel factibilidad)
        {
            SPResponseGeneric result = this._indicadorRepository.CrearFactibilidad(factibilidad);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DetallesFactibilidad([FromBody] FactibilidadRequestModel factibilidad)
        {
            FactibilidadDetailsModel result = this._indicadorRepository.DetallesFactibilidad((int)factibilidad.IdFactibilidad);

            return Json(result);
        }

        [HttpPost]
        public JsonResult EditarFactibilidad([FromBody] FactibilidadRequestModel factibilidad)
        {
            SPResponseGeneric result = this._indicadorRepository.EditarFactibilidad(factibilidad);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DesactivarFactibilidad([FromBody] FactibilidadRequestModel factibilidad)
        {
            SPResponseGeneric result = this._indicadorRepository.DesactivarFactibilidad(factibilidad);

            return Json(result);
        }

    }
}
