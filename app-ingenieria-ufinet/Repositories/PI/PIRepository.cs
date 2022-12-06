using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Models.Parametrization.Herraje;
using app_ingenieria_ufinet.Models.Parametrization.Moneda;
using app_ingenieria_ufinet.Models.Parametrization.UnidadesMedida;
using app_ingenieria_ufinet.Models.PI;
using app_ingenieria_ufinet.Utils;

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
    }
    #endregion interface

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un pi
    /// </summary>
    public class PIRepository : IPIRepository
    {
        private IDatabaseUtils _dbUtils;

        public PIRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
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
            var cantidadBobinas = request.DistanciaBobina / request.Distancia;

            //ASIGNACION VALORES A RESPONSE
            response.herrajes = resultCalcHerrajes;
            response.TotalHerrajes = totalHerrajes;
            response.TotalBobina = totalBobina;
            response.CantidadBobinas = cantidadBobinas;

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
            CrearPIResponseModel response = new CrearPIResponseModel();

            response.numPI = pi.PIResumenCompraRequest.numPI;

            var totalFibrasIniciales = pi.PIDetalleFORequest.Count();
            var totalHerrajesIniciales = 0;

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
                {"@usuario", "rconde"},///PENDIENTE
                {"@totalPIFO", pi.PIResumenCompraRequest.totalPIFO},
                {"@totalPIHerrajes", pi.PIResumenCompraRequest.totalPIHerrajes},
                {"@totalPI", pi.PIResumenCompraRequest.totalPI}
            };

            var resultResumenCompra = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_resumen_compra", procedureParamsResumen);

            //Si se guarda cabecera o resumen exitosamente
            if(resultResumenCompra.Count() > 0 && resultResumenCompra[0].Tipo == "success")
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
                            {"@usuario", "rconde"}//PENDIENTE
                        };

                        var resultFO = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_fo", procedureParamsFO);

                        responseResultFOs.Add(resultFO[0]);//Agregar resultado

                        //se guardan herrajes
                        foreach(var herraje in fo.herrajes)
                        {
                            var procedureParamsHerraje = new Dictionary<string, object>()
                            {
                                {"@idTipoHerraje", herraje.idTipoHerraje},
                                {"@idPIDetalleFO", resultFO[0].Id},
                                {"@numPI", pi.PIResumenCompraRequest.numPI},
                                {"@cantidadHerrajes", herraje.cantidadHerrajes},
                                {"@precioUnitarioHerraje", herraje.precioUnitarioHerraje},
                                {"@usuario", "rconde"}//PENDIENTE
                            };

                            var resultHerraje = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_herraje", procedureParamsHerraje);

                            responseResultHerrajes.Add(resultHerraje[0]);//Agregar resultado
                        }
                    }
                    response.responseFO = responseResultFOs;
                    response.responseHerraje = responseResultHerrajes;
                }
            }

            //Si total de fibras fueron procesadas.
            if(totalFibrasIniciales == response.responseFO.Select(x=>x.Tipo == "success").Count())
            {
                response.result = "success";
            }
            else
            {
                response.result = "error";
            }

            return response;
        }

        #endregion PI

    }
}
