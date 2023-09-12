

using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
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

        /// <summary>
        /// Devuelve un listado de las asignaciones creadas
        /// </summary>
        /// <returns></returns>
        List<AsignationModel> ListaAsignaciones();

        /// <summary>
        /// Retorna lista de tecnicos activos
        /// </summary>
        /// <returns></returns>
        List<ActiveTechnical> ListaTecnicosActivos();

        /// <summary>
        /// Retorna lista de contratistas
        /// </summary>
        /// <returns></returns>
        List<ContratistModel> ListaContratistas();

        /// <summary>
        /// Retorna una lista de ingenieros
        /// </summary>
        /// <returns></returns>
        List<EngineerModel> ListaIngenieros();

        /// <summary>
        /// Crea una nueva asignacion que se mostrará en asignaciones y en app móvil
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SPResponseGeneric CrearAsignacion(AsignationRequestModel request);

        /// <summary>
        /// Elimina una asignación permanentemente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SPResponseGeneric EliminarAsignacion(AsignationRequestModel request);

        /// <summary>
        /// Crea Supervision, ya sea aceptada o rechazada
        /// </summary>
        /// <param name="accept"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        SPResponseGenericWithVal CrearSupervision(bool accept, InspectionSupervitionDoneRequest request);
    }
    #endregion interface

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class InspectionRepository : IInspectionRepository
    {
        private IDatabaseUtils _dbUtils;
        private IUserService _userService;

        public InspectionRepository(IDatabaseUtils dbUtils, IUserService userService)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
             _userService = userService;
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

        /// <summary>
        /// Devuelve un listado de las asignaciones creadas
        /// </summary>
        /// <returns></returns>
        public List<AsignationModel> ListaAsignaciones()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<AsignationModel>("lista_asignaciones_inspeccion", procedureParams);

            return result;
        }

        /// <summary>
        /// Retorna lista de tecnicos activos
        /// </summary>
        /// <returns></returns>
        public List<ActiveTechnical> ListaTecnicosActivos()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<ActiveTechnical>("lista_tecnicos_activos", procedureParams);

            return result;
        }

        /// <summary>
        /// Retorna lista de contratistas
        /// </summary>
        /// <returns></returns>
        public List<ContratistModel> ListaContratistas()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<ContratistModel>("lista_contratistas", procedureParams);

            return result;
        }

        /// <summary>
        /// Retorna una lista de ingenieros
        /// </summary>
        /// <returns></returns>
        public List<EngineerModel> ListaIngenieros()
        {
            var procedureParams = new Dictionary<string, object>() { };

            var result = this._dbUtils.ExecuteStoredProc<EngineerModel>("lista_ingenieros", procedureParams);

            return result;
        }

        /// <summary>
        /// Crea una nueva asignacion que se mostrará en asignaciones y en app móvil
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SPResponseGeneric CrearAsignacion(AsignationRequestModel request)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_servicio_nuevo", request.IdServicioNuevo},
                {"@id_cliente", request.IdCliente },
                {"@id_tecnico", request.IdTecnico },
                {"@id_contrata", request.IdContrata },
                {"@id_ingeniero", request.IdIngeniero}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_asignacion", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Elimina una asignación permanentemente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SPResponseGeneric EliminarAsignacion(AsignationRequestModel request)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_servicio_nuevo", request.IdServicioNuevo}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("eliminar_asignacion", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Crea Supervision, ya sea aceptada o rechazada
        /// </summary>
        /// <param name="accept"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public SPResponseGenericWithVal CrearSupervision(bool accept, InspectionSupervitionDoneRequest request)
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;
            List<SPResponseGenericWithVal> result = new List<SPResponseGenericWithVal>();

            //Aceptar
            if(accept){
                var procedureParams = new Dictionary<string, object>()
                {
                    {"@id_inspeccion_trabajo", request.idInspeccionTrabajo},
                    {"@id_servicio_nuevo", request.idServicioNuevo},
                    {"@usuario", usuario }
                };

                result = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("sup_acept_inspection_by_id", procedureParams);
            }
            //Rechazar la supervision, enviar a otro estado.
            else{
                //Principal
                var procedureParams = new Dictionary<string, object>()
                {
                    {"@id_inspeccion_trabajo", request.idInspeccionTrabajo},
                    {"@id_servicio_nuevo", request.idServicioNuevo},
                    {"@usuario", usuario }
                };
                result = this._dbUtils.ExecuteStoredProc<SPResponseGenericWithVal>("sup_reject_inspection_by_id", procedureParams);

                //Detalle cada tarea supervisada.
                var answers = request.categorias
                                        .SelectMany(categoria => categoria.preguntas)
                                        .Where(pregunta => !string.IsNullOrEmpty(pregunta.observacion));

                if(answers != null){
                    foreach (var answer in answers)
                    {
                        var procedureParamsAnsw = new Dictionary<string, object>()
                        {
                            {"@id_supervision", result.FirstOrDefault().Id},
                            {"@id_inspeccion_trabajo_tarea", answer.idTarea},
                            {"@observaciones", answer.observacion }
                        };

                        var resultAnswers = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("sup_reject_inspection_by_id_detail", procedureParamsAnsw);
                    }

                }
            }
            
            return result.FirstOrDefault();
        }
        #endregion
    }
}
