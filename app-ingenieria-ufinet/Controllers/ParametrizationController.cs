using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
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
        public IActionResult TipoBobinaFo()
        {
            return View();
        }

        public IActionResult TipoHerraje()
        {
            return View();
        }

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
        #endregion dropdowns

    }
}
