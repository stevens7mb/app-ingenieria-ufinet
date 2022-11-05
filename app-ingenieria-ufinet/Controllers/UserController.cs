using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Repositories.Login;
using app_ingenieria_ufinet.Repositories.User;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Collections.Generic;
using System.Linq;

namespace app_ingenieria_ufinet.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Usuario()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaUsuarios()
        {
            List<UserModel> result = this._userRepository.ListaUsuarios();

            return Json(new { data = result });
        }

        [HttpGet]
        public JsonResult ListaSucursales()
        {
            List<SucursalModel> result = this._userRepository.ListaSucursales();

            List<Select2Model> select2 = new List<Select2Model>();

            if(result.Count > 0)
            {
                select2 = result
                .Select(x => new Select2Model { id = x.idSucursal, text = x.Descripcion })
                .ToList();
            }

            return Json(select2);
        }

        [HttpGet]
        public JsonResult ListaRoles()
        {
            List<RolesModel> result = this._userRepository.ListaRoles();

            List<Select2Model> select2 = new List<Select2Model>();

            if (result.Count > 0)
            {
                select2 = result
                .Select(x => new Select2Model { id = x.idRol, text = x.Descripcion })
                .ToList();
            }

            return Json(select2);
        }

        [HttpPost]
        public JsonResult AgregarUsuario([FromBody]UserRequestModel usuario)
        {
            SPResponseGeneric result = this._userRepository.AgregarUsuario(usuario);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DetallesUsuario([FromBody] UserRequestModel usuario)
        {
            UserDetailsModel result = this._userRepository.DetallesUsuario(usuario.Usuario);

            return Json(result);
        }

        [HttpPost]
        public JsonResult EditarUsuario([FromBody] UserRequestModel usuario)
        {
            SPResponseGeneric result = this._userRepository.EditarUsuario(usuario);

            return Json(result);
        }

        [HttpPost]
        public JsonResult DesactivarUsuario([FromBody] UserRequestModel usuario)
        {
            SPResponseGeneric result = this._userRepository.DesactivarUsuario(usuario.Usuario);

            return Json(result);
        }

    }
}
