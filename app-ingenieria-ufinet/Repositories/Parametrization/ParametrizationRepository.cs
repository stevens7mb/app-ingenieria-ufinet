using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.Parametrization.BobinaFO;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Models.Parametrization.Herraje;
using app_ingenieria_ufinet.Models.Parametrization.Moneda;
using app_ingenieria_ufinet.Models.Parametrization.UnidadesMedida;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.Parametrization
{
    #region interface
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IParametrizationRepository
    {
        /// <summary>
        /// Obtiene la lista de tipos de bobina de fibra óptica
        /// </summary>
        /// <returns>retorna una lista de tipos de bobina de fibra optica</returns>
        List<TipoBobinaFO> ListaTiposFO();

        /// <summary>
        /// Agrega un nuevo tipo de bobina fo
        /// </summary>
        /// <param name="tipobobina">Modelo de Tipo Bobina de FO</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric AgregarTipoBobinaFo(TipoBobinaRequestModel tipobobina);

        /// <summary>
        /// Desactiva el tipo de bobina de fo
        /// </summary>
        /// <param name="idTipoBobinaFO">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric DesactivarTipoDeBobinaFO(int idTipoBobinaFO);

        /// <summary>
        /// Listado de monedas
        /// </summary>
        /// <returns>devuelve listado de monedas</returns>
        List<Moneda> ListadoMoneda();

        /// <summary>
        /// Listado de unidades medida para precio
        /// </summary>
        /// <returns>devuelve listado de unidades de medida para el precio</returns>
        List<UnidadesMedidaPrecio> ListaUnidadesMedidaPrecio();

        /// <summary>
        /// Listado de unidades medida para bobina distancia
        /// </summary>
        /// <returns>devuelve listado de unidades de medida para la distancia de la bobina de fo</returns>
        List<UnidadesMedidaBobina> ListaUnidadesMedidaBobina();

        /// <summary>
        /// Obtiene la lista de tipos de herrajes
        /// </summary>
        /// <returns>retorna una lista de tipos de bobina de fibra optica</returns>
        List<TipoHerraje> ListaTiposHerrajes();

        /// <summary>
        /// Agrega un nuevo tipo de herraje
        /// </summary>
        /// <param name="tipoherraje">Modelo de Tipo Herraje</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric AgregarTipoHerraje(TipoHerrajeRequestModel tipoherraje);

        /// <summary>
        /// Desactiva el tipo de herraje
        /// </summary>
        /// <param name="idTipoHerraje">Identificador Tipo Herraje</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric DesactivarTipoHerraje(int idTipoHerraje);
    }
    #endregion interface

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class ParametrizationRepository : IParametrizationRepository
    {
        private IDatabaseUtils _dbUtils;

        public ParametrizationRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
        }

        #region TiposBobinasFO

        /// <summary>
        /// Obtiene la lista de tipos de bobina de fibra óptica
        /// </summary>
        /// <returns>retorna una lista de tipos de bobina de fibra optica</returns>
        public List<TipoBobinaFO> ListaTiposFO()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<TipoBobinaFO>("lista_tipos_bobina_fo", procedureParams);

            return result;
        }


        /// <summary>
        /// Agrega un nuevo tipo de bobina fo
        /// </summary>
        /// <param name="tipobobina">Modelo de Tipo Bobina de FO</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric AgregarTipoBobinaFo(TipoBobinaRequestModel tipobobina)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@tipoBobinaFo", tipobobina.TipoBobinaFO},
                {"@precio", tipobobina.Precio},
                {"@idUnidadMedidaPrecio", tipobobina.IdUnidadMedidaPrecio},
                {"@idMoneda", tipobobina.IdMoneda},
                {"@distanciaBobina", tipobobina.DistanciaBobina},
                {"@idUnidadMedidaBobina", tipobobina.IdUnidadMedidaBobina}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_tipo_bobina_fo", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Desactiva el tipo de bobina de fo
        /// </summary>
        /// <param name="idTipoBobinaFO">Identificador Tipo Bobina FO</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric DesactivarTipoDeBobinaFO(int idTipoBobinaFO)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@idTipoBobinaFo", idTipoBobinaFO}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_tipo_bobina_fo", procedureParams);

            return result[0];
        }
        #endregion TiposBobinasFO

        #region TiposHerraje

        /// <summary>
        /// Obtiene la lista de tipos de herrajes
        /// </summary>
        /// <returns>retorna una lista de tipos de bobina de fibra optica</returns>
        public List<TipoHerraje> ListaTiposHerrajes()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<TipoHerraje>("lista_tipos_herrajes", procedureParams);

            return result;
        }

        /// <summary>
        /// Agrega un nuevo tipo de herraje
        /// </summary>
        /// <param name="tipoherraje">Modelo de Tipo Herraje</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric AgregarTipoHerraje(TipoHerrajeRequestModel tipoherraje)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@tipoHerraje", tipoherraje.tipoHerraje},
                {"@precio", tipoherraje.precio},
                {"@idMoneda", tipoherraje.idMoneda},
                {"@idTipoBobinaFO", tipoherraje.idTipoBobinaFO}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_tipo_herraje", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Desactiva el tipo de herraje
        /// </summary>
        /// <param name="idTipoHerraje">Identificador Tipo Herraje</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric DesactivarTipoHerraje(int idTipoHerraje)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@idTipoHerraje", idTipoHerraje}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_tipo_herraje", procedureParams);

            return result[0];
        }

        #endregion TiposHerraje

        #region Comunes
        /// <summary>
        /// Listado de monedas
        /// </summary>
        /// <returns>devuelve listado de monedas</returns>
        public List<Moneda> ListadoMoneda()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<Moneda>("lista_monedas", procedureParams);

            return result;
        }

        /// <summary>
        /// Listado de unidades medida para precio
        /// </summary>
        /// <returns>devuelve listado de unidades de medida para el precio</returns>
        public List<UnidadesMedidaPrecio> ListaUnidadesMedidaPrecio()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<UnidadesMedidaPrecio>("lista_unidades_medida_precio", procedureParams);

            return result;
        }

        /// <summary>
        /// Listado de unidades medida para bobina distancia
        /// </summary>
        /// <returns>devuelve listado de unidades de medida para la distancia de la bobina de fo</returns>
        public List<UnidadesMedidaBobina> ListaUnidadesMedidaBobina()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<UnidadesMedidaBobina>("lista_unidades_medida_bobina", procedureParams);

            return result;
        }

        #endregion Comunes
    }
}
