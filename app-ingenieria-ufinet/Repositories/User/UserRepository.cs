using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.User
{
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Obtiene la lista de los usuarios
        /// </summary>
        /// <returns>retorna una lista de usuarios</returns>
        List<UserModel> ListaUsuarios();

        /// <summary>
        /// Obtiene la lista de sucursales
        /// </summary>
        /// <returns>retorna una lista de sucursales</returns>
        List<SucursalModel> ListaSucursales();

        /// <summary>
        /// Obtiene la lista de roles
        /// </summary>
        /// <returns>retorna una lista de roles</returns>
        List<RolesModel> ListaRoles();

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric AgregarUsuario(UserRequestModel usuario);

        /// <summary>
        /// Obtiene los detalles de usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>detalles de usuario</returns>
        UserDetailsModel DetallesUsuario(string Usuario);

        /// <summary>
        /// Edita el usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric EditarUsuario(UserRequestModel usuario);

        /// <summary>
        /// Desactiva el usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        SPResponseGeneric DesactivarUsuario(string usuario);
    }

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private IDatabaseUtils _dbUtils;

        public UserRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
        }

        /// <summary>
        /// Obtiene la lista de los usuarios
        /// </summary>
        /// <returns>retorna una lista de usuarios</returns>
        public List<UserModel> ListaUsuarios()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<UserModel>("lista_usuarios", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene la lista de sucursales
        /// </summary>
        /// <returns>retorna una lista de sucursales</returns>
        public List<SucursalModel> ListaSucursales()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<SucursalModel>("lista_sucursales", procedureParams);

           return result;
        }

        /// <summary>
        /// Obtiene la lista de roles
        /// </summary>
        /// <returns>retorna una lista de roles</returns>
        public List<RolesModel> ListaRoles()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<RolesModel>("lista_roles", procedureParams);

            return result;
        }

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric AgregarUsuario(UserRequestModel usuario)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", usuario.Usuario},
                {"@password", usuario.Password},
                {"@name", usuario.Nombre},
                {"@email", usuario.Correo},
                {"@idSucursal", usuario.Sucursal},
                {"@idRoles", usuario.Roles}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_usuario", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Obtiene los detalles de usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>detalles de usuario</returns>
        public UserDetailsModel DetallesUsuario(string Usuario)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", Usuario}
            };

            var result = this._dbUtils.ExecuteStoredProc<UserDetailsModel>("detalles_editar_usuario", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Edita el usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric EditarUsuario(UserRequestModel usuario)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", usuario.Usuario},
                {"@password", usuario.Password},
                {"@name", usuario.Nombre},
                {"@email", usuario.Correo},
                {"@idRoles", usuario.Roles}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("editar_usuario", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Desactiva el usuario
        /// </summary>
        /// <param name="usuario">Modelo de Usuario</param>
        /// <returns>respuesta generica sp</returns>
        public SPResponseGeneric DesactivarUsuario(string usuario)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", usuario}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_usuario", procedureParams);

            return result[0];
        }

    }
}
