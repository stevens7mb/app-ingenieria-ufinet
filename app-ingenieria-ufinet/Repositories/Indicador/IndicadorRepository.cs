using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons;
using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.Indicadores.Dashboard;
using app_ingenieria_ufinet.Models.Indicadores.Factibilidad;
using app_ingenieria_ufinet.Models.Login;
using app_ingenieria_ufinet.Models.User;
using app_ingenieria_ufinet.Utils;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace app_ingenieria_ufinet.Repositories.Indicador
{
    /// <summary>
    /// interfaces agregar metodos que se vayan utilizando
    /// </summary>
    public interface IIndicadorRepository
    {
        /// <summary>
        /// Obtiene la lista de las factibilidades
        /// </summary>
        /// <returns>retorna una lista de factibilidades</returns>
        List<FactibilidadModel> ListaFactibilidades();

        /// <summary>
        /// Obtiene la lista de las factibilidades de forma paginada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        DataTableResponse<FactibilidadPaginateModel> ListaFactibilidadesPaginate(DataTableRequest request);

        /// <summary>
        /// Obtiene la lista de clientes
        /// </summary>
        /// <returns>retorna una lista de clientes</returns>
        List<ClienteModel> selectClientes();

        /// <summary>
        /// Obtiene la lista de KAM
        /// </summary>
        /// <returns>retorna una lista de KAM</returns>
        List<KamModel> selectKAM();

        /// <summary>
        /// Crea Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        SPResponseGeneric CrearFactibilidad(FactibilidadRequestModel factiblidad);

        /// <summary>
        /// Obtiene los detalles de la factibilidad
        /// </summary>
        /// <param name="IdFactibilidad">Identificador de la Factibilidad</param>
        /// <returns>detalles de usuario</returns>
        FactibilidadDetailsModel DetallesFactibilidad(int idFactibilidad);

        /// <summary>
        /// Edita Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        SPResponseGeneric EditarFactibilidad(FactibilidadRequestModel factiblidad);

        /// <summary>
        /// Desactiva la Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp eliminar factibilidad</returns>
        SPResponseGeneric DesactivarFactibilidad(FactibilidadRequestModel factiblidad);


        /// <summary>
        /// Obtener lista de tipos de servicio
        /// </summary>
        /// <returns>retorna lista de los tipos de servicio</returns>
        List<TipoServicioModel> TiposServicios();

        /// <summary>
        /// Obtiene las respuestas de los procedimientos almacenados para las estadisticas del dashboard, dinamico.
        /// </summary>
        /// <param name="Anio">Año desde el que se desea obtener estadisticas</param>
        /// <param name="Mes">Mes desde el que se desea obtener estadisticad</param>
        /// <returns>retorna lista de los tipos de servicio</returns>
        DashboardModel DatosDashboard(DashboardRequestModel request);

        /// <summary>
        /// Agregar cliente con el nombre
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        bool AddClient(string clientName);

        /// <summary>
        /// Genera archivo de excel
        /// </summary>
        /// <returns></returns>
        MemoryStream GenerateExcelFactibilities();
    }

    /// <summary>
    /// Repositorio con todas las funcionalidades usadas en un usuario
    /// </summary>
    public class IndicadorRepository : IIndicadorRepository
    {
        private IDatabaseUtils _dbUtils;
        private IUserService _userService;
        private readonly DataContext _context;

        public IndicadorRepository(IDatabaseUtils dbUtils, IUserService userService, DataContext context)
        {
            this._dbUtils = dbUtils ?? throw new ArgumentNullException(nameof(dbUtils));
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de las factibilidades
        /// </summary>
        /// <returns>retorna una lista de factibilidades</returns>
        public List<FactibilidadModel> ListaFactibilidades()
        {
            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;
            var procedureParams = new Dictionary<string, object>()
            {
                {"@usuario", username ?? "" }
            };

            var result = this._dbUtils.ExecuteStoredProc<FactibilidadModel>("lista_factibilidades", procedureParams);

            return result;
        }

        /// <summary>
        /// Obtiene la lista de las factibilidades de forma paginada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTableResponse<FactibilidadPaginateModel> ListaFactibilidadesPaginate(DataTableRequest request)
        {

            var req = new DataTablePaginateRequest()
            {
                PageNo = Convert.ToInt32(request.Start / request.Length),
                PageSize = request.Length,
                SortColumn = request.Order[0].Column,
                SortDirection = request.Order[0].Dir,
                SearchValue = request.Search != null ? request.Search.Value.Trim() : ""
            };

            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;

            try
            {
                var serviceTypeFilter = request.Columns?.FirstOrDefault(x => x.Data == "tipoServicio")?.Search.Value;
                var clientFilter = request.Columns?.FirstOrDefault(x => x.Data == "cliente")?.Search.Value;

                var procedureParams = new Dictionary<string, object>()
                {
                    {"@SearchValue", req.SearchValue},
                    {"@PageNo", req.PageNo},
                    {"@PageSize", req.PageSize },
                    {"@SortColumn", req.SortColumn },
                    {"@SortDirection", req.SortDirection},
                    {"@usuario", username ?? ""},
                    {"@tipo_servicio_filter", serviceTypeFilter},
                    {"@cliente_filter", clientFilter}
                };

                var result = this._dbUtils.ExecuteStoredProc<FactibilidadPaginateModel>("lista_factibilidades_paginate", procedureParams);

                var filterServiceTypeValues = _context.TipoServicios.Where(x => x.Estado == -1).ToList();
                var filterClientsValues = _context.Clientes.Where(x => x.Estado == -1).ToList();

                var filters = new List<object> { filterServiceTypeValues, filterClientsValues };

                return new DataTableResponse<FactibilidadPaginateModel>()
                {
                    Draw = request.Draw,
                    RecordsTotal = result != null && result.Any() ? result[0].TotalCount : 0,
                    RecordsFiltered = result != null && result.Any() ? result[0].FilteredCount : 0,
                    Data = result != null && result.Any() ? result.ToArray() : new FactibilidadPaginateModel[0],
                    Error = "",
                    FiltersValues = filters
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de clientes
        /// </summary>
        /// <returns>retorna una lista de clientes</returns>
        public List<ClienteModel> selectClientes()
        {
            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;
            var idBranch = _context?.Usuarios?.FirstOrDefault(x => x.Usuario1 == username)?.IdSucursal;

            var result = _context?.Clientes.Where(x => x.Estado == -1 && x.IdSucursal == idBranch).Select(x => new ClienteModel
            {
                IdCliente = x.IdCliente,
                Nombre = x.Nombre ?? "",
                AreaEstudio = x.AreaEstudio ?? ""
            }).ToList();

            return result ?? [];
        }

        /// <summary>
        /// Obtiene la lista de KAM
        /// </summary>
        /// <returns>retorna una lista de KAM</returns>
        public List<KamModel> selectKAM()
        {
            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;
            var idBranch = _context?.Usuarios?.FirstOrDefault(x => x.Usuario1 == username)?.IdSucursal;

            var result = _context?.Kams.Where(x => x.Estado == -1 && x.IdSucursal == idBranch).Select(x => new KamModel
            {
                IdKAM = x.IdKam,
                Nombre = x.Nombre ?? "",
                Usuario = x.Usuario ?? ""
            }).ToList();

            return result ?? [];
        }

        /// <summary>
        /// Crea Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp crear factibilidad</returns>
        public SPResponseGeneric CrearFactibilidad(FactibilidadRequestModel factiblidad)
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;

            var procedureParams = new Dictionary<string, object>()
            {
                {"@ticket", factiblidad.Ticket},
                {"@estudio", factiblidad.Estudio },
                {"@id_cliente", factiblidad.Cliente },
                {"@id_kam", factiblidad.KAM },
                {"@bw", factiblidad.BW},
                {"@fecha_solicitud", factiblidad.FechaSolicitud},
                {"@fecha_respuesta", factiblidad.FechaRespuesta},
                {"@sitio_con_cobertura", factiblidad.SitioConCobertura},
                {"@sitio_con_cobertura_parcial", factiblidad.SitioConCoberturaParcial},
                {"@sitio_sin_cobertura", factiblidad.SitioSinCobertura},
                {"@usuario", usuario},
                {"@tipo_servicio", factiblidad.IdTipoServicio}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("crear_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Obtiene los detalles de la factibilidad
        /// </summary>
        /// <param name="IdFactibilidad">Identificador de la Factibilidad</param>
        /// <returns>detalles de usuario</returns>
        public FactibilidadDetailsModel DetallesFactibilidad(int idFactibilidad)
        {
            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", idFactibilidad}
            };

            var result = this._dbUtils.ExecuteStoredProc<FactibilidadDetailsModel>("detalles_editar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Edita Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp editar factibilidad</returns>
        public SPResponseGeneric EditarFactibilidad(FactibilidadRequestModel factiblidad)
        {
            var usuario = _userService.GetUser().Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", factiblidad.IdFactibilidad},
                {"@ticket", factiblidad.Ticket},
                {"@estudio", factiblidad.Estudio },
                {"@id_cliente", factiblidad.Cliente },
                {"@id_kam", factiblidad.KAM },
                {"@bw", factiblidad.BW},
                {"@fecha_solicitud", factiblidad.FechaSolicitud},
                {"@fecha_respuesta", factiblidad.FechaRespuesta},
                {"@sitio_con_cobertura", factiblidad.SitioConCobertura},
                {"@sitio_con_cobertura_parcial", factiblidad.SitioConCoberturaParcial},
                {"@sitio_sin_cobertura", factiblidad.SitioSinCobertura},
                {"@usuario", usuario},
                {"@tipo_servicio", factiblidad.IdTipoServicio}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("editar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Desactiva la Factibilidad
        /// </summary>
        /// <returns>retorna respuesta de sp eliminar factibilidad</returns>
        public SPResponseGeneric DesactivarFactibilidad(FactibilidadRequestModel factiblidad)
        {

            var procedureParams = new Dictionary<string, object>()
            {
                {"@id_factibilidad", factiblidad.IdFactibilidad}
            };

            var result = this._dbUtils.ExecuteStoredProc<SPResponseGeneric>("desactivar_factibilidad", procedureParams);

            return result[0];
        }

        /// <summary>
        /// Obtener lista de tipos de servicio
        /// </summary>
        /// <returns>retorna lista de los tipos de servicio</returns>
        public List<TipoServicioModel> TiposServicios()
        {

            var procedureParams = new Dictionary<string, object>() {};

            var result = this._dbUtils.ExecuteStoredProc<TipoServicioModel>("lista_tipos_servicios", procedureParams);

            return result;
        }

        /// <summary>
        /// Verificaa si es admiin o no
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool IsUserAdministrator(string username)
        {
            bool isAdmin = _context.RolUsuarios.Any(x => x.Usuario == username && x.IdRolNavigation.Descripcion == "Admin");

            return isAdmin;
        }

        /// <summary>
        /// Obtiene las respuestas de los procedimientos almacenados para las estadisticas del dashboard, dinamico.
        /// </summary>
        /// <param name="Anio">Año desde el que se desea obtener estadisticas</param>
        /// <param name="Mes">Mes desde el que se desea obtener estadisticad</param>
        /// <returns>retorna lista de los tipos de servicio</returns>
        public DashboardModel DatosDashboard(DashboardRequestModel request)
        {
            //tipos de servicios
            List<TipoServicioModel> servicios = TiposServicios();

            //Usuario logueado
            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;
            var branchId = _context?.Usuarios?.FirstOrDefault(x => x.Usuario1 == username)?.IdSucursal;
            int isAdmin = IsUserAdministrator(username ?? "") ? -1 : 0;

            //Parametros comunes sps
            var procedureParams = new Dictionary<string, object>()
            {
                {"@mes_desde", request.MesDesde == 0 ? null : request.MesDesde},
                {"@mes_hasta", request.MesHasta == 0 ? null : request.MesHasta},
                {"@anio", request.Anio == 0 ? null : request.Anio},
                {"@id_sucursal", branchId },
                {"@es_admin", isAdmin}
            };

            //Datos de Estudios por Ingenieros
            var estudiosPorIngenieros = this._dbUtils.ExecuteStoredProc<FactibilidadesPorIngenieroModel>("dashboard_lista_fact_ingeniero", procedureParams);

            //Datos de Estudios por Clientes
            var estudiosPorClientes = this._dbUtils.ExecuteStoredProc<FactibilidadesClientesModel>("dashboard_lista_fact_clientes", procedureParams);

            //Datos de Estudios BW
            var estudiosBW = this._dbUtils.ExecuteStoredProc<FactibilidadesAnchoBandaModel>("dashboard_fact_bw", procedureParams);

            //Datos de Estudios Sitios Totales
            var estudiosSitiosTotales = this._dbUtils.ExecuteStoredProc<SitiosTotalesModel>("dashboard_fact_sitios_totales", procedureParams);

            //Datos de Estudios por KAMs
            var estudiosPorKams = this._dbUtils.ExecuteStoredProc<FactibilidadesPorKamModel>("dashboard_lista_fact_kams", procedureParams);

            //Datos de indicadores de desempeño por año
            List<IndicadoresDesempeModel> indicadoresDesempeño = new List<IndicadoresDesempeModel>();

            var procedureParamsIndicadores = new Dictionary<string, object>()
            {
                {"@anio", request.Anio == 0 ? null : request.Anio},
                {"@id_sucursal", branchId},
                {"@es_admin", isAdmin}
            };

            indicadoresDesempeño = this._dbUtils.ExecuteStoredProc<IndicadoresDesempeModel>("dashboard_indicadores_desemp_anio_unif", procedureParamsIndicadores);

            //Modelo de respuesta
            DashboardModel result = new DashboardModel();

            result.CantidadEstudiosTotales = estudiosPorIngenieros != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.Sum(x => x.CantidadEstudios) : 0;
            result.NombreIngeniero = estudiosPorIngenieros != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.FirstOrDefault().Ingeniero : "";
            result.NombreKam = estudiosPorKams != null && estudiosPorKams.Count > 0 ? estudiosPorKams.FirstOrDefault().Kam : "";
            result.CantidadEstudiosIngeniero = estudiosPorIngenieros  != null && estudiosPorIngenieros.Count > 0 ? estudiosPorIngenieros.FirstOrDefault().CantidadEstudios : 0;
            result.CantidadEstudiosKam = estudiosPorKams != null && estudiosPorKams.Count > 0 ? estudiosPorKams.FirstOrDefault().CantidadEstudios : 0;
            result.SitiosTotales = estudiosSitiosTotales != null && estudiosSitiosTotales.Count > 0 ? estudiosSitiosTotales.FirstOrDefault().SitiosTotales : 0;
            result.SitiosConCobertura = estudiosSitiosTotales != null && estudiosSitiosTotales.Count > 0 ? estudiosSitiosTotales.FirstOrDefault().SitiosConCobertura : 0;
            result.SitiosConCoberturaParcial = estudiosSitiosTotales != null && estudiosSitiosTotales.Count > 0 ? estudiosSitiosTotales.FirstOrDefault().SitiosConCoberturaParcial : 0;
            result.SitiosSinCobertura = estudiosSitiosTotales != null && estudiosSitiosTotales.Count > 0 ? estudiosSitiosTotales.FirstOrDefault().SitiosSinCobertura : 0;
            result.CantidadTiposServicio = servicios.Count();
            result.AnchoDeBanda = estudiosBW != null && estudiosBW.Count > 0 ? estudiosBW.FirstOrDefault().AnchoDeBanda : 0;
            result.FactibilidadesPorIngeniero = estudiosPorIngenieros;
            result.FactibilidadesPorKam = estudiosPorKams;
            result.RankingClientes = estudiosPorClientes;
            result.IndicadoresDesemp = indicadoresDesempeño;
            result.RankingClientesGrafica = estudiosPorClientes.OrderByDescending(x => x.CantidadEstudios).Take(10).ToList();
            result.RankingKamsGrafica = estudiosPorKams.OrderByDescending(x => x.CantidadEstudios).Take(10).ToList();

            return result;
        }

        public bool AddClient(string clientName)
        {
            var username = _userService?.GetUser()?.Claims?.FirstOrDefault(x => x.Type == "IdUsuario")?.Value;
            var branchId = _context?.Usuarios?.FirstOrDefault(x => x.Usuario1 == username)?.IdSucursal;
            var branchDesc = _context?.Sucursals?.FirstOrDefault(x => x.IdSucursal == branchId)?.Descripcion;
            int maxId = _context?.Clientes.Max(c => (int?)c.IdCliente) + 1 ?? 0;

            Cliente client = new() { 
                IdCliente = maxId,
                Nombre = clientName,
                IdSucursal = branchId,
                Estado = -1,
                AreaEstudio = branchDesc
            };

            _context.Clientes.Add(client);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Genera archivo de excel
        /// </summary>
        /// <returns></returns>
        public MemoryStream GenerateExcelFactibilities()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Factibilidades");

                // Encabezados de columnas
                worksheet.Cell(1, 1).Value = "ID Factibilidad";
                worksheet.Cell(1, 2).Value = "Ticket";
                worksheet.Cell(1, 3).Value = "Estudio";
                worksheet.Cell(1, 4).Value = "Cliente";
                worksheet.Cell(1, 5).Value = "KAM";
                worksheet.Cell(1, 6).Value = "BW";
                worksheet.Cell(1, 7).Value = "Fecha Solicitud";
                worksheet.Cell(1, 8).Value = "Fecha Respuesta";
                worksheet.Cell(1, 9).Value = "Estado";
                worksheet.Cell(1, 10).Value = "Sitio con Cobertura";
                worksheet.Cell(1, 11).Value = "Sitio con Cobertura Parcial";
                worksheet.Cell(1, 12).Value = "Sitio sin Cobertura";
                worksheet.Cell(1, 13).Value = "Sitios Analizados";
                worksheet.Cell(1, 14).Value = "Ingeniero";
                worksheet.Cell(1, 15).Value = "Tipo de Servicio";

                // Formato de celdas
                worksheet.Row(1).Style.Font.Bold = true; // Negrita para la fila de encabezados

                var data = ListaFactibilidades();

                // Llenar datos
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.IdFactibilidad;
                    worksheet.Cell(row, 2).Value = item.Ticket;
                    worksheet.Cell(row, 3).Value = item.Estudio;
                    worksheet.Cell(row, 4).Value = item.Cliente;
                    worksheet.Cell(row, 5).Value = item.KAM;
                    worksheet.Cell(row, 6).Value = item.BW;
                    worksheet.Cell(row, 7).Value = item.FechaSolicitud;
                    worksheet.Cell(row, 7).Style.DateFormat.Format = "dd-MM-yyyy";
                    worksheet.Cell(row, 8).Value = item.FechaRespuesta;
                    worksheet.Cell(row, 8).Style.DateFormat.Format = "dd-MM-yyyy";
                    worksheet.Cell(row, 9).Value = item.Estado;
                    worksheet.Cell(row, 10).Value = item.SitioConCobertura;
                    worksheet.Cell(row, 11).Value = item.SitioConCoberturaParcial;
                    worksheet.Cell(row, 12).Value = item.SitioSinCobertura;
                    worksheet.Cell(row, 13).Value = item.SitiosAnalizados;
                    worksheet.Cell(row, 14).Value = item.Ingeniero;
                    worksheet.Cell(row, 15).Value = item.TipoServicio;

                    row++; 
                }

                // Convertir workbook a MemoryStream
                MemoryStream stream = new();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return stream;
            }
        }
    }
}
