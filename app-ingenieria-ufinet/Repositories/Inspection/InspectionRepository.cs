

using app_ingenieria_ufinet.Models.Inspection;
using app_ingenieria_ufinet.Models.Parametrization.BobinFO;
using app_ingenieria_ufinet.Utils;

namespace app_ingenieria_ufinet.Repositories.Inspection
{
    #region interface
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IInspectionRepository
    {
        /// <summary>
        /// Obtiene la lista de inspecciones realizadas
        /// </summary>
        /// <returns>retorna una lista de inspecciones realizadas</returns>
        List<InspectionIndividualModel> ListaInspeccionesRealizadas();

        /// <summary>
        /// Obtiene detalle de inspeccion por id(IdInspeccionTrabajo)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InspectionDoneModel ObtenerInspeccionPorId(int id);
    }
    #endregion interface

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class InspectionRepository : IInspectionRepository
    {
        private IDatabaseUtils _dbUtils;

        public InspectionRepository(IDatabaseUtils dbUtils)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
        }

        #region Inspeccion

        /// <summary>
        /// Obtiene la lista de inspecciones realizadas
        /// </summary>
        /// <returns>retorna una lista de inspecciones realizadas</returns>
        public List<InspectionIndividualModel> ListaInspeccionesRealizadas()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<InspectionIndividualModel>("lista_inspecciones_trabajo", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene detalle de inspeccion por id(IdInspeccionTrabajo)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InspectionDoneModel ObtenerInspeccionPorId(int id)
        {
            InspectionDoneModel inspection = new InspectionDoneModel();
            List<CategoriaModel> categoriasList = new List<CategoriaModel>();

            //Datos individuales
            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_inspeccion_trabajo", id}
            };
            var datosIndividuales = this._dbUtils.ExecuteStoredProc<InspectionIndividualModel>("detalle_individual_inspeccion_por_id", procedureParams);

            //Preguntas y Respuestas por Categoria
            var tasks = this._dbUtils.ExecuteStoredProc<DetallePreguntasInspeccionModel>("detalle_preguntas_inspeccion_por_id", procedureParams);
            if (tasks!= null && tasks.Any())
            {
                categoriasList = tasks
                .GroupBy(t => t.IdCategoriaTarea)
                .Select(g => new CategoriaModel
                {
                    IdCategoriaTarea = g.Key,
                    Categoria = g.First().Categoria,
                    preguntas = g.ToList()
                })
                .ToList();
            }

            //Asignacion a response
            inspection.datosIndividuales = datosIndividuales[0];
            inspection.categorias = categoriasList;

            return inspection;
        }

        #endregion
    }
}
