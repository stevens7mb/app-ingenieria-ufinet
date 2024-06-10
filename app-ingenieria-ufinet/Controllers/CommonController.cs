using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Repositories.Common;
using Microsoft.AspNetCore.Mvc;

namespace app_ingenieria_ufinet.Controllers
{
    public class CommonController(ICommonRepository commonRepository) : Controller
    {
        private readonly ICommonRepository _commonRepository = commonRepository;

        /// <summary>
        /// Get Select list items by entity name
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <returns>Json SelectItems</returns>
        [HttpGet]
        public JsonResult GetSelectListItems(string entityName, string valueField, string textField)
        {
            // Obtener el tipo de entidad usando reflexión
            Type? entityType = Array.Find(typeof(DataContext).Assembly.GetTypes(), t => t.Name == entityName);

            // Comprobar si se pudo obtener el tipo de entidad
            if (entityType == null)
            {
                // Manejar el caso en que el tipo de entidad no se pueda obtener
                return Json(null); // O devuelve un error o una lista vacía, según lo que necesites
            }

            // Obtener el método GetSelectListItems genérico utilizando reflexión
            var method = _commonRepository.GetType().GetMethod("GetSelectListItems");
            var genericMethod = method?.MakeGenericMethod(entityType);

            // Invocar el método GetSelectListItems con el tipo de entidad
            var selectListItems = genericMethod?.Invoke(_commonRepository, new object[] { valueField, textField });

            return Json(selectListItems);
        }

        /// <summary>
        /// Get municipalities by state id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMunicipalitiesByStateId(int stateId)
        {
            var municipalities = _commonRepository.GetMunicipalitiesByStateId(stateId);
            return Json(municipalities);
        }
    }
}
