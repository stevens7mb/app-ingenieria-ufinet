﻿using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Models.PI;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Repositories.Parametrization;
using app_ingenieria_ufinet.Repositories.PI;
using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class PIController : Controller
    {
        private readonly IParametrizationRepository _parametrizationRepository;
        private readonly IPIRepository _piRepository;

        public PIController(IParametrizationRepository parametrizationRepository, IPIRepository piRepository)
        {
            _parametrizationRepository = parametrizationRepository;
            _piRepository = piRepository;
        }

        #region Views
        public IActionResult PIList()
        {
            return View();
        }

        public IActionResult ConsolidacionPI()
        {
            return View();
        }
        public IActionResult PIProcesadas()
        {
            return View();
        }

        #endregion Views

        [HttpPost]
        public JsonResult ObtenerTipoBobinaFO([FromBody] TipoBobinaRequestModel request)
        {
            var idTipoBobinaFO = request.idTipoBobinaFO;
            TipoBobinaFO result = this._parametrizationRepository.ObtenerTipoFO((int)idTipoBobinaFO);

            return Json(result);
        }

        [HttpPost]
        public JsonResult CalcularBobinaHerrajes([FromBody] CalculoBobinaHerrajeRequest request)
        {

            CalculoBobinaHerrajeResponseModel result = this._piRepository.CalcularBobinaHerrajes(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult GuardarPI([FromBody] PIRequest request)
        {

            CrearPIResponseModel result = this._piRepository.CrearNuevoPI(request);

            return Json(result);
        }

        [HttpGet]
        public JsonResult ListadoPIGeneradas()
        {

            List<ListaPIResponseModel> result = this._piRepository.ListadoPIGeneradas();

            return Json(new { data = result });
        }

        [HttpPost]
        public JsonResult EliminarPI([FromBody] PIRequestId request)
        { 

            SPResponseGeneric result = this._piRepository.EliminarPI(request.idResumenCompraPI);

            return Json(result);
        }

        [HttpPost]
        public JsonResult CargarDetallesPI([FromBody] PIRequestId request)
        {
            DetallesPIResponseModel result = new DetallesPIResponseModel();

            var resumen = this._piRepository.CargarDetallesPIResumen(request.idResumenCompraPI);
            var fos = this._piRepository.CargarDetallesPIFO(request.idResumenCompraPI);

            result.detallesResumenPI = resumen;
            result.detallesFO = fos;

            return Json(result);
        }
    }
}
