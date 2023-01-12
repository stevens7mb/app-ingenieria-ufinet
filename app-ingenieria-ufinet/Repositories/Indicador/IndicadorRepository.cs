using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Indicadores.Dashboard;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.Indicador
{
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IIndicadorRepository
    {
        /// <summary>
        /// Obtiene la lista de las factibilidades
        /// </summary>
        /// <returns>retorna una lista de factibilidades</returns>
        List<FactibilidadModel> ListaFactibilidades();

        /// <summary>
        /// Obtiene la lista de clientes
        /// </summary>
        /// <returns>retorna una lista de clientes</returns>
        List<ClienteModel> selectClientes();

        /// <summary>
        /// Obtiene la lista de KAM
        /// </summary>
        /// <returns>retorna una lista de KAM</returns>
        List<KamModel> selectKAM();

        /// <summary>
        /// Crea Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        SPResponseGeneric CrearFactibilidad(FactibilidadRequestModel factiblidad);

        /// <summary>
        /// Obtiene los detalles de la factibilidad
        /// </summary>
        /// <param name="IdFactibilidad">Identificador de la Factibilidad</param>
        /// <returns>detalles de usuario</returns>
        FactibilidadDetailsModel DetallesFactibilidad(int idFactibilidad);

        /// <summary>
        /// Edita Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        SPResponseGeneric EditarFactibilidad(FactibilidadRequestModel factiblidad);

        /// <summary>
        /// Desactiva la Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp eliminar factibilidad</returns>
        SPResponseGeneric DesactivarFactibilidad(FactibilidadRequestModel factiblidad);


        /// <summary>
        /// Obtener lista de tipos de servicio
        /// </summary>
        /// <returns>retorna lista de los tipos de servicio</returns>
        List<TipoServicioModel> TiposServicios();

        /// <summary>
        /// Obtiene las respuestas de los procedimientos almacenados para las estadisticas del dashboard, dinamico.
        /// </summary>
        /// <param name="Anio">Año desde el que se desea obtener estadisticas</param>
        /// <param name="Mes">Mes desde el que se desea obtener estadisticad</param>
        /// <returns>retorna lista de los tipos de servicio</returns>
        DashboardModel DatosDashboard(DashboardRequestModel request);
    }

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class IndicadorRepository : IIndicadorRepository
    {
        private IDatabaseUtils _dbUtils;
        private IUserService _userService;
        private readonly DataContext _context;

        public IndicadorRepository(IDatabaseUtils dbUtils, IUserService userService, DataContext context)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de las factibilidades
        /// </summary>
        /// <returns>retorna una lista de factibilidades</returns>
        public List<FactibilidadModel> ListaFactibilidades()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<FactibilidadModel>("lista_factibilidades", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene la lista de clientes
        /// </summary>
        /// <returns>retorna una lista de clientes</returns>
        public List<ClienteModel> selectClientes()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<ClienteModel>("lista_clientes", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene la lista de KAM
        /// </summary>
        /// <returns>retorna una lista de KAM</returns>
        public List<KamModel> selectKAM()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<KamModel>("lista_kam", procedureParams);

            return result;
        }

        /// <summary>
        /// Crea Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        public SPResponseGeneric CrearFactibilidad(FactibilidadRequestModel factiblidad)
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;

            var procedureParams = new Dictionary<string, object>()
            {
                {"@ticket", factiblidad.Ticket},
                {"@estudio", factiblidad.Estudio },
                {"@id_cliente", factiblidad.Cliente },
                {"@id_kam", factiblidad.KAM },
                {"@bw", factiblidad.BW},
                {"@fecha_solicitud", factiblidad.FechaSolicitud},
                {"@fecha_respuesta", factiblidad.FechaRespuesta},
                {"@sitio_con_cobertura", factiblidad.SitioConCobertura},
                {"@sitio_con_cobertura_parcial", factiblidad.SitioConCoberturaParcial},
                {"@sitio_sin_cobertura", factiblidad.SitioSinCobertura},
                {"@usuario", usuario},
                {"@tipo_servicio", factiblidad.IdTipoServicio}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Obtiene los detalles de la factibilidad
        /// </summary>
        /// <param name="IdFactibilidad">Identificador de la Factibilidad</param>
        /// <returns>detalles de usuario</returns>
        public FactibilidadDetailsModel DetallesFactibilidad(int idFactibilidad)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", idFactibilidad}
            };

            var result = this._dbUtils.ExecuteStoredProc<FactibilidadDetailsModel>("detalles_editar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Edita Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp editar factibilidad</returns>
        public SPResponseGeneric EditarFactibilidad(FactibilidadRequestModel factiblidad)
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", factiblidad.IdFactibilidad},
                {"@ticket", factiblidad.Ticket},
                {"@estudio", factiblidad.Estudio },
                {"@id_cliente", factiblidad.Cliente },
                {"@id_kam", factiblidad.KAM },
                {"@bw", factiblidad.BW},
                {"@fecha_solicitud", factiblidad.FechaSolicitud},
                {"@fecha_respuesta", factiblidad.FechaRespuesta},
                {"@sitio_con_cobertura", factiblidad.SitioConCobertura},
                {"@sitio_con_cobertura_parcial", factiblidad.SitioConCoberturaParcial},
                {"@sitio_sin_cobertura", factiblidad.SitioSinCobertura},
                {"@usuario", usuario},
                {"@tipo_servicio", factiblidad.IdTipoServicio}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("editar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Desactiva la Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp eliminar factibilidad</returns>
        public SPResponseGeneric DesactivarFactibilidad(FactibilidadRequestModel factiblidad)
        {

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", factiblidad.IdFactibilidad}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Obtener lista de tipos de servicio
        /// </summary>
        /// <returns>retorna lista de los tipos de servicio</returns>
        public List<TipoServicioModel> TiposServicios()
        {

            var procedureParams = new Dictionary<string, object>() {};

            var result = this._dbUtils.ExecuteStoredProc<TipoServicioModel>("lista_tipos_servicios", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene las respuestas de los procedimientos almacenados para las estadisticas del dashboard, dinamico.
        /// </summary>
        /// <param name="Anio">Año desde el que se desea obtener estadisticas</param>
        /// <param name="Mes">Mes desde el que se desea obtener estadisticad</param>
        /// <returns>retorna lista de los tipos de servicio</returns>
        public DashboardModel DatosDashboard(DashboardRequestModel request)
        {
            //tipos de servicios
            List<TipoServicioModel> servicios = TiposServicios();
            
            //Parametros comunes sps
            var procedureParams = new Dictionary<string, object>()
            {
                {"@mes", request.Mes == 0 ? null : request.Mes},
                {"@anio", request.Anio == 0 ? null : request.Anio}
            };

            //Datos de Estudios por Ingenieros
            var estudiosPorIngenieros = this._dbUtils.ExecuteStoredProc<FactibilidadesPorIngenieroModel>("dashboard_lista_fact_ingeniero", procedureParams);

            //Datos de Estudios por Clientes
            var estudiosPorClientes = this._dbUtils.ExecuteStoredProc<FactibilidadesClientesModel>("dashboard_lista_fact_clientes", procedureParams);

            //Datos de Estudios BW
            var estudiosBW = this._dbUtils.ExecuteStoredProc<FactibilidadesAnchoBandaModel>("dashboard_fact_bw", procedureParams);

            //Datos de Estudios Sitios Totales
            var estudiosSitiosTotales = this._dbUtils.ExecuteStoredProc<SitiosTotalesModel>("dashboard_fact_sitios_totales", procedureParams);

            //Modelo de respuesta
            DashboardModel result = new DashboardModel();

            result.CantidadEstudiosTotales = estudiosPorIngenieros != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.Sum(x => x.CantidadEstudios) : 0;
            result.NombreIngeniero = estudiosPorIngenieros != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.FirstOrDefault().Ingeniero : "";
            result.CantidadEstudiosIngeniero = estudiosPorIngenieros  != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.FirstOrDefault().CantidadEstudios : 0;
            result.SitiosTotales = estudiosSitiosTotales != null && estudiosSitiosTotales.Count > 0 ? estudiosSitiosTotales.FirstOrDefault().SitiosTotales : 0;
            result.CantidadTiposServicio = servicios.Count();
            result.AnchoDeBanda = estudiosBW != null && estudiosBW.Count > 0 ? estudiosBW.FirstOrDefault().AnchoDeBanda : 0;
            result.FactibilidadesPorIngeniero = estudiosPorIngenieros;
            result.RankingClientes = estudiosPorClientes;

            return result;
        }

    }
}
