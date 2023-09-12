using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.Inspection;
using app_ingenieria_ufinet.Models.PI;
using app_ingenieria_ufinet.Repositories.Inspection;
using app_ingenieria_ufinet.Repositories.Parametrization;
using app_ingenieria_ufinet.Repositories.PI;
using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class InspectionController : Controller
    {
        private readonly IInspectionRepository _inspectionRepository;

        public InspectionController(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        #region Vistas
        public IActionResult Inspection()
        {
            return View();
        }

        public IActionResult InspectionDetalle(int id)
        {
            var result = this._inspectionRepository.ObtenerInspeccionPorId(id);

            return View(result);
        }

        public IActionResult Asignation()
        {
            return View();
        }

        public IActionResult InspectionSupervition()
        {
            return View();
        }

        public IActionResult InspectionSupervitionDetail(int id)
        {
            var result = this._inspectionRepository.ObtenerInspeccionPorId(id);

            return View(result);
        }
        #endregion

        #region metodos data
        [HttpGet]
        public JsonResult ListaInspeccionesRealizadas()
        {

            List<InspectionIndividualModel> result = this._inspectionRepository.ListaInspeccionesRealizadas();

            return Json(new { data = result });
        }

        [HttpGet]
        public JsonResult ListaAsignaciones()
        {

            List<AsignationModel> result = this._inspectionRepository.ListaAsignaciones();

            return Json(new { data = result });
        }

        [HttpGet]
        public JsonResult SelectTecnicos()
        {
            List<ActiveTechnical> result = this._inspectionRepository.ListaTecnicosActivos();

            return Json(result);
        }

        [HttpGet]
        public JsonResult SelectContratistas()
        {
            List<ContratistModel> result = this._inspectionRepository.ListaContratistas().Where(x => x.Estado == -1).ToList();

            return Json(result);
        }

        [HttpGet]
        public JsonResult SelectIngenieros()
        {
            List<EngineerModel> result = this._inspectionRepository.ListaIngenieros().Where(x => x.Estado == -1).ToList();

            return Json(result);
        }

        [HttpPost]
        public JsonResult CrearAsignacion([FromBody] AsignationRequestModel request)
        {
            SPResponseGeneric result = this._inspectionRepository.CrearAsignacion(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult EliminarAsignacion([FromBody] AsignationRequestModel request)
        {
            SPResponseGeneric result = this._inspectionRepository.EliminarAsignacion(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult CrearSupervisionRechazar([FromBody] InspectionSupervitionDoneRequest request)
        {

            SPResponseGenericWithVal result = this._inspectionRepository.CrearSupervision(false, request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult CrearSupervisionAceptar([FromBody] InspectionSupervitionDoneRequest request)
        {
            SPResponseGenericWithVal result = this._inspectionRepository.CrearSupervision(true, request);

            return Json(result);
        }
        #endregion
    }
}
