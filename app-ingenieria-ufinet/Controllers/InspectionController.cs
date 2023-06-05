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
        #endregion

        #region metodos data
        [HttpGet]
        public JsonResult ListaInspeccionesRealizadas()
        {

            List<InspectionIndividualModel> result = this._inspectionRepository.ListaInspeccionesRealizadas();

            return Json(new { data = result });
        }
        #endregion
    }
}
