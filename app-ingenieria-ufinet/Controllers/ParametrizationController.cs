using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Models.Parametrization.Herraje;
using app_ingenieria_ufinet.Models.Parametrization.Moneda;
using app_ingenieria_ufinet.Models.Parametrization.UnidadesMedida;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Repositories.Parametrization;
using app_ingenieria_ufinet.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class ParametrizationController : Controller
    {
        private readonly IParametrizationRepository _parametrizationRepository;

        public ParametrizationController(IParametrizationRepository parametrizationRepository)
        {
            _parametrizationRepository = parametrizationRepository;
        }

        #region Views
        public IActionResult TipoBobinaFo()
        {
            return View();
        }

        public IActionResult TipoHerraje()
        {
            return View();
        }
        #endregion Views

        #region TipoBobinaFO

        [HttpGet]
        public JsonResult ListaTiposBobinaFO()
        {
            List<TipoBobinaFO> result = this._parametrizationRepository.ListaTiposFO();

            return Json(new { data = result });
        }

        [HttpPost]
        public JsonResult AgregarTipoBobinaFO([FromBody] TipoBobinaRequestModel tipoBobina)
        {
            SPResponseGeneric result = this._parametrizationRepository.AgregarTipoBobinaFo(tipoBobina);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DesactivarTipoBobinaFO([FromBody] TipoBobinaRequestModel tipoBobina)
        {
            SPResponseGeneric result = this._parametrizationRepository.DesactivarTipoDeBobinaFO((int)tipoBobina.idTipoBobinaFO);

            return Json(result);
        }
        #endregion TipoBobinaFO

        #region dropdowns

        [HttpGet]
        public JsonResult ListaMonedas()
        {
            List<Moneda> listaMoneda = _parametrizationRepository.ListadoMoneda();

            return Json(listaMoneda);
        }

        [HttpGet]
        public JsonResult ListaUnidadesMedidaPrecio()
        {
            List<UnidadesMedidaPrecio> listaUnidades = _parametrizationRepository.ListaUnidadesMedidaPrecio();

            return Json(listaUnidades);
        }

        [HttpGet]
        public JsonResult ListaUnidadesMedidaBobina()
        {
            List<UnidadesMedidaBobina> listaUnidades = _parametrizationRepository.ListaUnidadesMedidaBobina();

            return Json(listaUnidades);
        }

        [HttpGet]
        public JsonResult ListaTiposDeBobinaFO()
        {
            List<TipoBobinaFO> tiposBobina = _parametrizationRepository.ListaTiposFO();

            return Json(tiposBobina);
        }
        #endregion dropdowns

        #region TipoHerraje
        [HttpGet]
        public JsonResult ListaTiposHerrajes()
        {
            List<TipoHerraje> result = this._parametrizationRepository.ListaTiposHerrajes();

            return Json(new { data = result });
        }

        [HttpPost]
        public JsonResult AgregarTipoHerraje([FromBody] TipoHerrajeRequestModel tipoHerraje)
        {
            SPResponseGeneric result = this._parametrizationRepository.AgregarTipoHerraje(tipoHerraje);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DesactivarTipoHerraje([FromBody] TipoHerrajeRequestModel tipoHerraje)
        {
            SPResponseGeneric result = this._parametrizationRepository.DesactivarTipoHerraje((int)tipoHerraje.idTipoHerraje);

            return Json(result);
        }
        #endregion TipoHerraje

    }
}
