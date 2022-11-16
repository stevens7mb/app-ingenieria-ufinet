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
        /// Agrega un nueva compra pi
        /// </summary>
        /// <param name="pi">Modelo Request PI</param>
        /// <returns>respuestas de ejecutar sps</returns>
        public CrearPIResponseModel CrearNuevoPI(PIRequest pi);
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
        /// Agrega un nueva compra pi
        /// </summary>
        /// <param name="pi">Modelo Request PI</param>
        /// <returns>respuestas de ejecutar sps</returns>
        public CrearPIResponseModel CrearNuevoPI(PIRequest pi)
        {
            CrearPIResponseModel response = new CrearPIResponseModel();

            response.numPI = pi.PIResumenCompraRequest.numPI;

            //Resumen de compra 
            var procedureParamsResumen = new Dictionary<string, object>()
            {
                {"@numPI", pi.PIResumenCompraRequest.numPI},
                {"@nombrePI", pi.PIResumenCompraRequest.nombrePI},
                {"@comentarioPI", pi.PIResumenCompraRequest.comentarioPI},
                {"@fechaSolicitud", pi.PIResumenCompraRequest.fechaSolicitud},
                {"@fechaOC", pi.PIResumenCompraRequest.fechaOC},
                {"@fechaBodegaSucursal", pi.PIResumenCompraRequest.fechaBodegaSucursal},
                {"@idSucursal", pi.PIResumenCompraRequest.idSucursal},
                {"@totalPIResumenCompra", pi.PIResumenCompraRequest.totalPIResumenCompra},
                {"@usuario", pi.PIResumenCompraRequest.usuario}
            };

            var resultResumenCompra = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_resumen_compra", procedureParamsResumen);

            //Si se guarda cabecera o resumen exitosamente
            if(resultResumenCompra.Count() > 0)
            {
                response.idResumenCompra = resultResumenCompra[0].Id;

                if (resultResumenCompra[0].Tipo == "success" && resultResumenCompra[0].Id != 0)
                {
                    //Se guardan las bobinas de fo
                    //Resumen de compra 
                    foreach (var fo in pi.PIDetalleFORequest)
                    {
                        var procedureParamsFO = new Dictionary<string, object>()
                        {
                            {"@idPIResumenCompra", resultResumenCompra[0].Id},
                            {"@numPI", fo.numPI},
                            {"@nombrePI", fo.nombrePI},
                            {"@fechaSolicitud", fo.fechaSolicitud},
                            {"@fechaOC", fo.fechaOC},
                            {"@fechaBodegaSucursal", fo.fechaBodegaSucursal},
                            {"@idSucursal", fo.idSucursal},
                            {"@totalDetalleFOPIDesc", fo.totalDetalleFOPIDesc},
                            {"@incurrido", fo.incurrido},
                            {"@idTipoBobinaFO", fo.idTipoBobinaFO},
                            {"@distanciaPIDetalle", fo.distanciaPIDetalle},
                            {"@cantidadBobinas", fo.cantidadBobinas},
                            {"@precioUnitario", fo.precioUnitario},
                            {"@totalDetalleFOPI", fo.totalDetalleFOPI},
                            {"@usuario", fo.usuario},
                        };

                        var resultFO = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_fo", procedureParamsFO);

                        response.responseFO.Add(resultFO[0]);//Agregar resultado

                        //se guardan herrajes
                        foreach(var herraje in fo.herrajes)
                        {
                            var procedureParamsHerraje = new Dictionary<string, object>()
                            {
                                {"@idTipoHerraje", herraje.idTipoHerraje},
                                {"@IdPIDetalleFO", resultFO[0].Id},
                                {"@numPI", herraje.numPI},
                                {"@nombrePI", herraje.nombrePI},
                                {"@fechaSolicitud", herraje.fechaSolicitud},
                                {"@fechaOC", herraje.fechaOC},
                                {"@fechaBodegaSucursal", herraje.fechaBodegaSucursal},
                                {"@idSucursal", herraje.idSucursal},
                                {"@totalDetalleFOPIDesc", herraje.totalDetalleFOPIDesc},
                                {"@incurrido", herraje.incurrido},
                                {"@pendienteIncurrir", herraje.pendienteIncurrir},
                                {"@cantidadHerrajes", herraje.cantidadHerrajes},
                                {"@precioUnitarioHerraje", herraje.precioUnitarioHerraje},
                                {"@totalDetalleCompraHerraje", herraje.totalDetalleCompraHerraje},
                                {"@usuario", herraje.usuario},
                            };

                            var resultHerraje = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("crear_pi_detalle_herraje", procedureParamsHerraje);
                           
                            response.responseHerraje.Add(resultHerraje[0]);//Agregar resultado
                        }
                    }
                }
            }

            return response;
        }

        #endregion PI

    }
}
