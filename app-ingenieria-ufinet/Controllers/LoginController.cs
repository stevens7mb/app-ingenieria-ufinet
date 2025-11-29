using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Repositories.Login;

namespace app_ingenieria_ufinet.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        #region Vistas
        public IActionResult Login(string mensaje)
        {
            ViewBag.MensajeLogin = mensaje;
            return View();
        }
        #endregion Vistas


        #region Metodos Funciones

        [HttpGet]
        public async Task<IActionResult> IniciarSesion(string username, string password)
        {
            app_ingenieria_ufinet.Models.Login.Usuario usuario = this._loginRepository.LoginUsuario(username, password);
            if (usuario == null)
            {
                var mensaje = "No tienes credenciales correctas";

                return RedirectToAction("Login", "Login", new { mensaje = mensaje});
            }
            else
            {
                //DEBEMOS CREAR UNA IDENTIDAD (name y role)
                //Y UN PRINCIPAL
                //DICHA IDENTIDAD DEBEMOS COMBINARLA CON LA COOKIE DE 
                //AUTENTIFICACION
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //TODO USUARIO PUEDE CONTENER UNA SERIE DE CARACTERISTICAS
                //LLAMADA CLAIMS.  DICHAS CARACTERISTICAS PODEMOS ALMACENARLAS
                //DENTRO DE USER PARA UTILIZARLAS A LO LARGO DE LA APP
                Claim claimUserName = new Claim(ClaimTypes.Name, usuario.Nombre);
                Claim claimIdUsuario = new Claim("IdUsuario", usuario.User.ToString());
                Claim claimEmail = new Claim("EmailUsuario", usuario.Email == null ? "" : usuario.Email);
                Claim sucursal = new("Sucursal", usuario.Sucursal.ToString());

                identity.AddClaim(claimUserName);
                identity.AddClaim(claimIdUsuario);
                identity.AddClaim(claimEmail);
                identity.AddClaim(sucursal);

                //Logica de roles se obtienen con base a usuario
                List<Roles> roles = this._loginRepository.LoginRoles(usuario.User);
                if (roles != null && roles.Count > 0 )
                {
                    foreach(var rol in roles)
                    {
                        Claim claimRole = new Claim(ClaimTypes.Role, rol.Descripcion.ToString());
                        identity.AddClaim(claimRole);
                    }
                }

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(45)
                });

                return Redirect("~/Home/Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        #endregion Metodos Funciones

    }

}
