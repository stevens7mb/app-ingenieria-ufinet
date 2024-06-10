using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.ServiceDesk;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace app_ingenieria_ufinet.Repositories.Common
{
    public interface ICommonRepository
    {
        /// <summary>
        /// Get select list items by entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSelectListItems<TEntity>(string valueField, string textField) where TEntity : class;

        /// <summary>
        /// Get Municipalities by state id
        /// </summary>
        /// <returns></returns>
        List<Municipality> GetMunicipalitiesByStateId(int stateId);

        /// <summary>
        /// Obtener el id de ingeniero por el usuario
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int? GetEngineerIdByUser(string userCode);
    }
}
