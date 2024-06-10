using app_ingenieria_ufinet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace app_ingenieria_ufinet.Repositories.Common
{
    public class CommonRepository(DataContext context) : ICommonRepository
    {
        private readonly DataContext _context = context;

        /// <summary>
        /// Get Select list items by entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectListItems<TEntity>(string valueField, string textField) where TEntity : class
        {
            var entities = _context.Set<TEntity>().ToList();

            var selectListItems = entities.Select(entity => new SelectListItem
            {
                Value = entity.GetType().GetProperty(valueField)?.GetValue(entity)?.ToString(),
                Text = entity.GetType().GetProperty(textField)?.GetValue(entity)?.ToString()
            }).ToList();

            return selectListItems;
        }

        /// <summary>
        /// Get Municipalities by state id
        /// </summary>
        /// <returns></returns>
        public List<Municipality> GetMunicipalitiesByStateId(int stateId)
        {
            var municipalities = _context.Municipalities.Where(x=>x.IdState == stateId).ToList();

            return municipalities;
        }

        /// <summary>
        /// Obtener el id de ingeniero por el usuario
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int? GetEngineerIdByUser(string userCode)
        {
            int? engineerId = _context?.Engineers?.FirstOrDefault(x => x.UserName == userCode)?.EngineerId;

            return engineerId;
        }
    }
}
