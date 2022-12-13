using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Models.Parametrization.Herraje;
using app_ingenieria_ufinet.Models.Parametrization.Moneda;
using app_ingenieria_ufinet.Models.Parametrization.UnidadesMedida;
using app_ingenieria_ufinet.Models.PI;
using app_ingenieria_ufinet.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Security.Claims;

namespace app_ingenieria_ufinet.Repositories.PI
{
    #region interface
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IPIRepository
    {
        /// <summary>
        /// Calcula los herrajes para la bobina y los totales
        /// </summary>
        /// <param name="request">Modelo Request Calculo</param>
        /// <returns>respuestas de ejecutar sp</returns>
        CalculoBobinaHerrajeResponseModel CalcularBobinaHerrajes(CalculoBobinaHerrajeRequest request);

        /// <summary>
        /// Agrega un nueva compra pi
        /// </summary>
        /// <param name="pi">Modelo Request PI</param>
        /// <returns>respuestas de ejecutar sps</returns>
        CrearPIResponseModel CrearNuevoPI(PIRequest pi);

        /// <summary>
        /// Listado de PI
        /// </summary>
        /// <returns>retorna listado de pi generadas</returns>
        List<ListaPIResponseModel> ListadoPIGeneradas();

        /// <summary>
        /// Eliminar PI
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta generica sp</returns>
        SPResponseGeneric EliminarPI(int idResumenCompraPI);

        /// <summary>
        /// Cargar detalles de PI por ID
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta modelo detalles pi</returns>
        DetallesPIResumenResponseModel CargarDetallesPIResumen(int idResumenCompraPI);

        /// <summary>
        /// Cargar detalles de PI por ID fos
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta modelo detalles pi</returns>
        List<DetallesPIFOResponseModel> CargarDetallesPIFO(int idResumenCompraPI);

    }
    #endregion interface

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un pi
    /// </summary>
    public class PIRepository : IPIRepository
    {
        private IDatabaseUtils _dbUtils;
        private IUserService _userService;
        private readonly DataContext _context;

        public PIRepository(IDatabaseUtils dbUtils, IUserService userService, DataContext context)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
            _userService = userService;
            _context = context;
        }


        #region PI

        /// <summary>
        /// Calcula los herrajes para la bobina y los totales
        /// </summary>
        /// <param name="request">Modelo Request Calculo</param>
        /// <returns>respuestas de ejecutar sp</returns>
        public CalculoBobinaHerrajeResponseModel CalcularBobinaHerrajes(CalculoBobinaHerrajeRequest request)
        {
            //instancia de modelo
            CalculoBobinaHerrajeResponseModel response = new CalculoBobinaHerrajeResponseModel();

            //CALCULAR HERRAJES
            var procedureParamsCalcHerrajes = new Dictionary<string, object>()
            {
                {"@id_tipo_bobina_fo", request.IdTipoBobinaFO},
                {"@distancia", request.Distancia}
            };
            //resultado de stored procedure
            var resultCalcHerrajes = this._dbUtils.ExecuteStoredProc<CalculoHerrajesResponseModel>("calc_herrajes", procedureParamsCalcHerrajes);

            //CALCULO DE TOTALES
            //total Bobina
            var totalBobina = request.Distancia * request.Precio;
            //total de Herrajes
            var totalHerrajes = resultCalcHerrajes.Sum(x => x.FormulaOut * x.Precio);
            //cantidad de bobinas
            var cantidadBobinas = request.Distancia / request.DistanciaBobina;

            //ASIGNACION VALORES A RESPONSE
            response.herrajes = resultCalcHerrajes;
            response.TotalHerrajes = totalHerrajes;
            response.TotalBobina = totalBobina;
            response.CantidadBobinas = decimal.Round(cantidadBobinas, 2);

            //respuesta de metodo
            return response;
        }

        /// <summary>
        /// Agrega un nueva compra pi
        /// </summary>
        /// <param name="pi">Modelo Request PI</param>
        /// <returns>respuestas de ejecutar sps</returns>
        public CrearPIResponseModel CrearNuevoPI(PIRequest pi)
        {
            try
            {
                CrearPIResponseModel response = new CrearPIResponseModel();
                var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;
                response.numPI = pi.PIResumenCompraRequest.numPI;
                var totalFibrasIniciales = pi.PIDetalleFORequest.Count();
                var totalHerrajesIniciales = 0;

                pi.PIDetalleFORequest.ForEach(x => totalHerrajesIniciales = totalHerrajesIniciales + x.herrajes.Count());

                //Resumen de compra 
                var procedureParamsResumen = new Dictionary<string, object>()
                {
                    {"@numPI", pi.PIResumenCompraRequest.numPI},
                    {"@nombrePI", pi.PIResumenCompraRequest.nombrePI},
                    {"@comentarioPI", pi.PIResumenCompraRequest.comentarioPI},
                    {"@fechaSolicitud", pi.PIResumenCompraRequest.fechaSolicitud},
                    {"@fechaOC", pi.PIResumenCompraRequest.fechaOC},
                    {"@fechaBodegaSucursal", pi.PIResumenCompraRequest.fechaBodegaSucursal},
                    {"@idSucursal", 1},///PENDIENTE
                    {"@totalPIResumenCompra", pi.PIResumenCompraRequest.totalPIEncabezado},
                    {"@incurrido", pi.PIResumenCompraRequest.incurrido},
                    {"@usuario", usuario},
                    {"@totalPIFO", pi.PIResumenCompraRequest.totalPIFO},
                    {"@totalPIHerrajes", pi.PIResumenCompraRequest.totalPIHerrajes},
                    {"@totalPI", pi.PIResumenCompraRequest.totalPI}
                };

                var resultResumenCompra = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_resumen_compra", procedureParamsResumen);

                //Si se guarda cabecera o resumen exitosamente
                if (resultResumenCompra.Count() > 0 && resultResumenCompra[0].Tipo == "success")
                {
                    response.idResumenCompra = resultResumenCompra[0].Id;

                    if (resultResumenCompra[0].Tipo == "success" && resultResumenCompra[0].Id != 0)
                    {
                        var responseResultFOs = new List<SPResponseGenericWithVal>();
                        var responseResultHerrajes = new List<SPResponseGenericWithVal>();

                        //Se guardan las bobinas de fo
                        //Resumen de compra 
                        foreach (var fo in pi.PIDetalleFORequest)
                        {
                            var procedureParamsFO = new Dictionary<string, object>()
                            {
                                {"@idPIResumenCompra", resultResumenCompra[0].Id},
                                {"@numPI", pi.PIResumenCompraRequest.numPI},
                                {"@idTipoBobinaFO", fo.idTipoBobinaFO},
                                {"@distanciaPIDetalle", fo.distanciaPIDetalle},
                                {"@cantidadBobinas", fo.cantidadBobinas},
                                {"@precioUnitario", fo.precioUnitario},
                                {"@totalDetalleFOPI", fo.totalDetalleFOPI},
                                {"@usuario", usuario}
                            };

                            var resultFO = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_fo", procedureParamsFO);

                            responseResultFOs.Add(resultFO[0]);//Agregar resultado

                            //se guardan herrajes
                            foreach (var herraje in fo.herrajes)
                            {
                                var procedureParamsHerraje = new Dictionary<string, object>()
                                {
                                    {"@idTipoHerraje", herraje.idTipoHerraje},
                                    {"@idPIDetalleFO", resultFO[0].Id},
                                    {"@numPI", pi.PIResumenCompraRequest.numPI},
                                    {"@cantidadHerrajes", herraje.cantidadHerrajes},
                                    {"@precioUnitarioHerraje", herraje.precioUnitarioHerraje},
                                    {"@usuario", usuario}
                                };

                                var resultHerraje = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_herraje", procedureParamsHerraje);

                                responseResultHerrajes.Add(resultHerraje[0]);//Agregar resultado
                            }
                        }
                        response.responseFO = responseResultFOs;
                        response.responseHerraje = responseResultHerrajes;
                    }
                }

                //Si total de fibras y herrajes fueron procesadas.
                if (totalFibrasIniciales == response.responseFO.Select(x => x.Tipo == "success").Count() && totalHerrajesIniciales == response.responseHerraje.Select(x => x.Tipo == "success").Count())
                {
                    response.result = "success";
                }
                else
                {
                    //Revertimos PI
                    var procedureParams = new Dictionary<string, object>()
                    {
                        {"@id_resumen_compra_pi", resultResumenCompra[0].Id}
                    };
                        var respuesta = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_pi", procedureParams);

                    response.result = "error";
                }

                return response;
            }
            catch(Exception e)
            {
                CrearPIResponseModel response = new CrearPIResponseModel();
                response.result = "error";
                return response;
            }
        }

        /// <summary>
        /// Listado de PI
        /// </summary>
        /// <returns>retorna listado de pi generadas</returns>
        public List<ListaPIResponseModel> ListadoPIGeneradas()
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;

            List<ListaPIResponseModel> lista = new List<ListaPIResponseModel>();

            var procedureParams = new Dictionary<string, object>()
            {
                {"@usuario", usuario}
            };

            lista = this._dbUtils.ExecuteStoredProc<ListaPIResponseModel>("lista_pi_generadas", procedureParams);

            return lista;
        }

        /// <summary>
        /// Eliminar PI
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta generica sp</returns>
        public SPResponseGeneric EliminarPI(int idResumenCompraPI)
        {

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_resumen_compra_pi", idResumenCompraPI}
            };

            var respuesta = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_pi", procedureParams);

            return respuesta[0];
        }

        /// <summary>
        /// Cargar detalles de PI por ID
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta modelo detalles pi</returns>
        public DetallesPIResumenResponseModel CargarDetallesPIResumen(int idResumenCompraPI)
        {

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_resumen_compra_pi", idResumenCompraPI}
            };

            var respuesta = this._dbUtils.ExecuteStoredProc<DetallesPIResumenResponseModel>("detalles_pi_generada_resumen", procedureParams);

            return respuesta[0];
        }

        /// <summary>
        /// Cargar detalles de PI por ID fos
        /// </summary>
        /// param name="idResumenCompraPI">Id del resumen de pi</param>
        /// <returns>retorna respuesta modelo detalles pi</returns>
        public List<DetallesPIFOResponseModel> CargarDetallesPIFO(int idResumenCompraPI)
        {

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_resumen_compra_pi", idResumenCompraPI}
            };

            var respuesta = this._dbUtils.ExecuteStoredProc<DetallesPIFOResponseModel>("detalles_pi_generada_fo", procedureParams);

            return respuesta;
        }
        #endregion PI

    }
}
