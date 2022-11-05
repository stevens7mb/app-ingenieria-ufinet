using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.Login
{
    /// <summary>
    /// Interfaces, agregar métodos
    /// </summary>
    public interface ILoginRepository
    {
        Usuario LoginUsuario(string username, string password);
        List<Roles> LoginRoles(string username);
    }

    /// <summary>
    /// Repositorio con funcionalidades para el login
    /// </summary>
    public class LoginRepository: ILoginRepository
    {
        private IDatabaseUtils _dbUtils;

        public LoginRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
        }

        /// <summary>
        /// Funcionalidad para hacer login y obtener datos
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Usuario LoginUsuario(string username, string password)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", username},
                {"password", password}
            };

            var result = this._dbUtils.ExecuteStoredProc<Usuario>("login", procedureParams);
            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                Usuario usuario = result[0];

                bool respuesta = usuario.Contraseña == password ? true : false;

                if (respuesta == true)
                {
                    return usuario;            }
                else
                {
                    //Contraseña incorrecta
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtener los roles de la base de datos
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Lista de roles</returns>
        public List<Roles> LoginRoles(string username)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@username", username}
            };

            var result = this._dbUtils.ExecuteStoredProc<Roles>("login_roles_by_user", procedureParams);
            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                List<Roles> rol = result;
                return rol;
            }
        }
    }
}
