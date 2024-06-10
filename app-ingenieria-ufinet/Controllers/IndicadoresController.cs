using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.Indicadores.Dashboard;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Repositories.Indicador;
using app_ingenieria_ufinet.Repositories.User;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

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

        [HttpPost]
        [Route("Indicadores/ListaFactibilidadesPaginate")]
        public DataTableResponse<FactibilidadPaginateModel> ListaFactibilidadesPaginate(DataTableRequest request)
        {

            request.Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
            request.Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            request.Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            request.Search = new DataTableSearch()
            {
                Value = Request.Form["search[value]"].FirstOrDefault()
            };

            request.Order = new DataTableOrder[] {
            new DataTableOrder()
            {
                Dir = Request.Form["order[0][dir]"].FirstOrDefault(),
                Column = Convert.ToInt32(Request.Form["order[0][column]"].FirstOrDefault())
            }};

            return _indicadorRepository.ListaFactibilidadesPaginate(request);
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

        [HttpGet]
        public JsonResult selectTiposServicios()
        {
            List<TipoServicioModel> result = this._indicadorRepository.TiposServicios();

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

        [HttpPost]
        public JsonResult DatosDashboard([FromBody] DashboardRequestModel request)
        {
            DashboardModel result = this._indicadorRepository.DatosDashboard(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult AddClient(string clientName)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos de entrada no válidos" });
            }

            bool result = this._indicadorRepository.AddClient(clientName);

            return Json(result);
        }
    }
}
