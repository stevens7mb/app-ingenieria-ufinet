using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.Login;
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
                {"@usuario", usuario}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_factibilidad", procedureParams);

            return result[0];
        }

    }
}
