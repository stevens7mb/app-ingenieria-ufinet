using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.User
{
    public interface IUserRepository
    {
        List<UserModel> ListaUsuarios();
        List<SucursalModel> ListaSucursales();
        List<RolesModel> ListaRoles();
        SPResponseGeneric AgregarUsuario(UserRequestModel usuario);
        UserDetailsModel DetallesUsuario(string Usuario);
        SPResponseGeneric EditarUsuario(UserRequestModel usuario);
        SPResponseGeneric DesactivarUsuario(string usuario);
    }
    public class UserRepository : IUserRepository
    {
        private IDatabaseUtils _dbUtils;

        public UserRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
        }

        public List<UserModel> ListaUsuarios()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<UserModel>("lista_usuarios", procedureParams);

            return result;
        }

        public List<SucursalModel> ListaSucursales()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<SucursalModel>("lista_sucursales", procedureParams);

           return result;
        }

        public List<RolesModel> ListaRoles()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<RolesModel>("lista_roles", procedureParams);

            return result;
        }

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

        public UserDetailsModel DetallesUsuario(string Usuario)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", Usuario}
            };

            var result = this._dbUtils.ExecuteStoredProc<UserDetailsModel>("detalles_editar_usuario", procedureParams);

            return result[0];
        }

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
