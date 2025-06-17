using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.ServiceDesk;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace app_ingenieria_ufinet.Repositories.ServiceDesk
{
    public class ServiceDeskRepository(DataContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : IServiceDeskRepository
    {
        private readonly DataContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        /// <summary>
        /// Obtiene los tickets generados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTableResponse<ServiceDeskTicketViewModel>> TicketsPaginateAsync(DataTableRequest request)
        {
            var req = new DataTablePaginateRequest()
            {
                PageNo = Convert.ToInt32(request.Start / request.Length),
                PageSize = request.Length,
                SortColumn = request.Order[0].Column,
                SortDirection = request.Order[0].Dir,
                SearchValue = request.Search != null ? request.Search.Value.Trim() : ""
            };

            var query = _context.ServiceDeskTickets.AsQueryable(); 

            // Aplica filtros si se proporciona un valor de búsqueda
            if (!string.IsNullOrEmpty(req.SearchValue))
            {
                query = query.Where(f => f.IdAffectedElementNavigation.Description.ToString().Contains(req.SearchValue) ||
                                         f.IdAffectedElement.ToString().Contains(req.SearchValue));
            }

            // Ordena los resultados
            var property = typeof(ServiceDeskTicket).GetProperties()[req.SortColumn].Name;

            if (req.SortDirection == "asc")
            {
                query = query.OrderBy(x => EF.Property<object>(x, property));
            }
            else
            {
                query = query.OrderByDescending(x => EF.Property<object>(x, property));
            }


            // Obtiene el número total de registros y registros filtrados
            var totalCount = await query.CountAsync();
            
            var filteredCount = !string.IsNullOrEmpty(req.SearchValue) ?
                                await query.CountAsync() : totalCount;

            //Estado de creacion Id
            int idCreationState = _context.TicketStates.FirstOrDefault(x => x.Description == "Creado")?.IdTicketState ?? -1;

            // Pagina los resultados
            var paginatedResult = await query.Skip(req.PageNo * req.PageSize)
                                             .Take(req.PageSize)
                                             .Select(f => new ServiceDeskTicketViewModel
                                             {
                                                PrefixId = f.IdPrefix,
                                                TicketId = f.IdTicket,
                                                TicketCode = f.IdPrefixNavigation.PrefixDesc + "-" + f.IdTicket,
                                                TicketName = f.TicketName,
                                                Creator = f.CreatorNavigation == null ? "" : f.CreatorNavigation.Name,
                                                CreatedDateTime = f.ServiceDeskTicketStatusHistories
                                                    .Where(x => x.IdTicketState == idCreationState)
                                                    .Select(x => (DateTime?)x.ChangeDateTime)
                                                    .FirstOrDefault() ?? DateTime.MinValue,
                                                Assigner = f.AssignerNavigation != null ? f.AssignerNavigation.Name : "",
                                                IncidentTypeDescription = f.IdIncidentTypeNavigation.Description,
                                                Assignee = f.AssigneeNavigation == null ? "" : f.AssigneeNavigation.Name,
                                                TicketStatus = f.IdTicketStateNavigation.Description
                                             })
                                             .ToListAsync();

            return new DataTableResponse<ServiceDeskTicketViewModel>()
            {
                Draw = request.Draw,
                RecordsTotal = totalCount,
                RecordsFiltered = filteredCount,
                Data = [.. paginatedResult],
                Error = ""
            };
        }

        /// <summary>
        /// Crea ticket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateTicketResponse CreateTicket(CreateTicketRequest request)
        {
            CreateTicketResponse response = new CreateTicketResponse();
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var creator = _context.Engineers.FirstOrDefault(x => x.UserName == request.UserCreate && x.ActiveStatus == -1 && x.UserNameNavigation.Activo == -1);
                var ticketCreateStatus = _context.TicketStates.FirstOrDefault(x => x.Description == "Creado");
                var regionId = _context.States.FirstOrDefault(x => x.IdState == request.StateId)?.IdRegion ?? -1;
                request.PrefixId = _context.Prefixes.FirstOrDefault(x => x.PrefixDesc == "GMT")?.IdPrefix ?? 0;

                // Crear una nueva instancia de ServiceDeskTicket con los datos proporcionados en request
                var newTicket = new ServiceDeskTicket
                {
                    IdPrefix = request.PrefixId,
                    TicketName = request.TicketName,
                    Creator = creator != null ? creator.EngineerId : 0,
                    Coordinate = request.Coordinate,
                    IdMunicipality = request.MunicipalityId,
                    IdState = request.StateId,
                    IdRegion = regionId,
                    Address = request.Address,
                    IdPriorityLevel = request.PriorityId,
                    IdIncidentType = request.IncidentTypeId,
                    IdPrimaryCause = request.PrimaryCauseId,
                    IdAffectedElement = request.AffectedElementId,
                    FaultDetail = request.SummaryTicket,
                    IdTicketState = ticketCreateStatus != null ? ticketCreateStatus.IdTicketState : 0
                };

                // Agregar el nuevo ticket a la base de datos
                _context.ServiceDeskTickets.Add(newTicket);
                _context.SaveChanges();

                // Crear una instancia de ServiceDeskTicketStatusHistory
                var newHistory = new ServiceDeskTicketStatusHistory
                {
                    IdPrefix = newTicket.IdPrefix,
                    IdTicket = newTicket.IdTicket,
                    IdTicketState = ticketCreateStatus == null ? 0 : ticketCreateStatus.IdTicketState,
                    ChangeDateTime = DateTime.Now,
                    ChangedByEngineerId = creator == null ? 0 : creator.EngineerId,
                    ChangedByUser = request.UserCreate
                };

                _context.ServiceDeskTicketStatusHistories.Add(newHistory);
                _context.SaveChanges();

                //Se guardan archivos
                foreach(var file in request.Files)
                {
                    UploadFileTicket(file, newTicket.IdPrefix, newTicket.IdTicket);
                }

                // Si todas las operaciones son exitosas, commit la transacción
                transaction.Commit();

                response.PrefixId = newTicket.IdPrefix;
                response.TicketId = newTicket.IdTicket;
                response.PrefixDesc = newTicket.IdPrefixNavigation.PrefixDesc;

                // Devolver valores, el indicador de éxito (true), prefijo id, prefijo y id
                return response;
            }
            catch (Exception)
            {
                // Si ocurre algún error, hacer rollback de la transacción
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public void UploadFileTicket(IFormFile file, int prefixId, int ticketId, int typeStatusId = (int)TicketFileTypeStatus.Initial)
        {
            string filename = GetUniqueFileName(file.FileName);
            string? targetFolder = _configuration["FileUploadSettings:TargetFolder"];
            string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, targetFolder ?? "");

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string filePath = Path.Combine(targetPath, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            string relativeFilePath = Path.Combine(targetFolder ?? "", filename);

            ServiceDeskTicketFile serviceDeskTicketFile = new()
            {
                NameFile = filename,
                PathFile = relativeFilePath,
                FileSize = file.Length,
                IdPrefix = prefixId,
                IdTicket = ticketId,
                TypeStatusId = typeStatusId
            };

            _context.ServiceDeskTicketFiles.Add(serviceDeskTicketFile);
            _context.SaveChanges();
        }

        // Método para obtener un nombre de archivo único para evitar conflictos
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string? targetFolder = _configuration["FileUploadSettings:TargetFolder"];
            string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, targetFolder ?? "");

            string filePath = Path.Combine(targetPath, fileName);

            // Verificar si ya existe un archivo con el mismo nombre
            if (System.IO.File.Exists(filePath))
            {
                // Generar un nombre único agregando un sufijo numérico
                int count = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string fileExtension = Path.GetExtension(fileName);
                string newFileName = $"{fileNameWithoutExtension}_{count}{fileExtension}";

                // Verificar la existencia del nuevo nombre generado
                while (System.IO.File.Exists(Path.Combine(targetPath, newFileName)))
                {
                    count++;
                    newFileName = $"{fileNameWithoutExtension}_{count}{fileExtension}";
                }

                fileName = newFileName;
            }

            return fileName;
        }

        /// <summary>
        /// Obtiene información del ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<Ticket?> GetTicketAsync(int prefixId, int ticketId)
        {
            var createdState = await _context.ServiceDeskTicketStatusHistories
                .Include(t => t.ChangedByEngineer)
                .FirstOrDefaultAsync(x => x.IdPrefix == prefixId && x.IdTicket == ticketId);

            var ticket = await _context.ServiceDeskTickets
                .Where(x => x.IdPrefix == prefixId && x.IdTicket == ticketId)
                .OrderBy(x => x.TicketName)
                .Select(e => new Ticket
                {
                    PrefixId = e.IdPrefix,
                    TicketId = e.IdTicket,
                    TicketCode = e.IdPrefixNavigation.PrefixDesc + "-" + e.IdTicket.ToString(),
                    TicketName = e.TicketName,
                    TicketStatus = e.IdTicketStateNavigation.Description ?? "",
                    Priority = e.IdPriorityLevelNavigation.Description ?? "",
                    CreatedBy = createdState != null ? createdState.ChangedByEngineer.Name ?? "" : "",
                    CreatedDate = createdState != null ? createdState.ChangeDateTime : DateTime.MinValue,
                    AssigneeBy = e.AssignerNavigation == null ? "" : e.AssignerNavigation.Name ?? "",
                    AssigneeFor = e.AssigneeNavigation == null ? "" : e.AssigneeNavigation.Name ?? "",
                    Coordinate = e.Coordinate ?? "",
                    Municipality = e.IdMunicipalityNavigation.Name,
                    State = e.IdStateNavigation.Name,
                    Region = e.IdRegionNavigation == null ? "" : e.IdRegionNavigation.Description ?? "",
                    Address = e.Address ?? "",
                    IncidentType = e.IdIncidentTypeNavigation == null ? "" : e.IdIncidentTypeNavigation.Description ?? "",
                    PrimaryCause = e.IdPrimaryCauseNavigation == null ? "" : e.IdPrimaryCauseNavigation.Description ?? "",
                    AffectedElement = e.IdAffectedElementNavigation.Description ?? "",
                    FaultDetail = e.FaultDetail ?? "",
                    Files = e.ServiceDeskTicketFiles
                                .Where(tf => tf.IdPrefix == e.IdPrefix && tf.IdTicket == e.IdTicket && tf.TypeStatusId == (int)TicketFileTypeStatus.Initial)
                                .Select(tf => new TicketFile
                                {
                                    TicketFileId = tf.TicketFileId,
                                    NameFile = tf.NameFile,
                                    Path = tf.PathFile
                                }).ToList(),
                    FinishFiles = e.ServiceDeskTicketFiles
                                .Where(tf => tf.IdPrefix == e.IdPrefix && tf.IdTicket == e.IdTicket && tf.TypeStatusId == (int)TicketFileTypeStatus.Final)
                                .Select(tf => new TicketFile
                                {
                                    TicketFileId = tf.TicketFileId,
                                    NameFile = tf.NameFile,
                                    Path = tf.PathFile
                                }).ToList(),
                    Binnacles = e.ServiceDeskTicketLogs
                                .Where(bn => bn.IdPrefix == e.IdPrefix && bn.IdTicket == e.IdTicket)
                                .Select(tf => new TicketBinnacle
                                {
                                    IdTicketLog = tf.IdTicketLog,
                                    Comment = tf.Comment,
                                    CreatedBy = tf.Engineer.Name ?? "",
                                    CreatedDate = tf.RegistryDate
                                }).ToList(),
                    Statuses = (List<TicketStatus>)e.ServiceDeskTicketStatusHistories
                                .Where(st => st.IdPrefix == e.IdPrefix && st.IdTicket == e.IdTicket)
                                .Select(st => new TicketStatus
                                {
                                    IdTicketState = st.IdTicketState,
                                    TicketStatusDescription = st.IdTicketStateNavigation.Description ?? "",
                                    ChangedBy = st.ChangedByEngineer.Name ?? "",
                                    ChangedDate = st.ChangeDateTime
                                })
                                .OrderByDescending(x => x.ChangedDate),
                    IsTechnicalVisitRequired = e.IsTechnicalVisitRequired,
                    IsBrigadeDeployed = e.IsBrigadeDeployed,
                    IsChangeNetworkTopology = e.IsChangeNetworkTopology,
                    IdSolutionType = e.IdSolutionType,
                    SolutionType = e.IdSolutionTypeNavigation == null ? "" : e.IdSolutionTypeNavigation.Description ?? "",
                    IdConfirmedArea = e.IdConfirmedArea,
                    ConfirmedArea = e.IdConfirmedAreaNavigation == null ? "" : e.IdConfirmedAreaNavigation.Description ?? "",
                    WorkDone = e.WorkDone,
                    IncidentClosureSummary = e.IncidentClosureSummary,
                    IsTicketFinalizedStatus = e.IdTicketStateNavigation.Description == "Finalizado"
                })
                .FirstOrDefaultAsync();

            return ticket;
        }

        /// <summary>
        /// Guarda registro en la bitacora
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <param name="commentary"></param>
        /// <returns></returns>
        public async Task<bool> SaveTicketLogAsync(int prefixId, int ticketId, int engineerId, string commentary)
        {
            try
            {
                ServiceDeskTicketLog log = new()
                {
                    IdPrefix = prefixId,
                    IdTicket = ticketId,
                    EngineerId = engineerId,
                    Comment = commentary,
                    RegistryDate = DateTime.Now
                };

                await _context.ServiceDeskTicketLogs.AddAsync(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetTicketAsync: {ex.Message}");
                return false; 
            }
        }

        /// <summary>
        /// Obtiene registros de bitacora por ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<List<TicketBinnacle>> GetTicketBinnaclesAsync(int prefixId, int ticketId)
        {
            List<TicketBinnacle> binnacles = await _context.ServiceDeskTicketLogs
                                                .Where(x => x.IdPrefix == prefixId && x.IdTicket == ticketId)
                                                .Select( x => new TicketBinnacle
                                                {
                                                    IdTicketLog = x.IdTicketLog,
                                                    Comment = x.Comment,
                                                    CreatedBy = x.Engineer.Name ?? "",
                                                    CreatedDate = x.RegistryDate
                                                }).ToListAsync();
            return binnacles;
        }

        /// <summary>
        /// Obtener ingenieros por tipo y ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerTypeId"></param>
        /// <returns></returns>
        public List<EngineerToAssign> GetEngineersToAssign(int prefixId, int ticketId, int engineerTypeId)
        {
            var ticketRegion = _context.ServiceDeskTickets
                                .Where(x => x.IdPrefix == prefixId && x.IdTicket == ticketId)
                                .Select(x => x.IdRegion)
                                .FirstOrDefault();

            List<EngineerToAssign> engineers;

            if (engineerTypeId == _context.EngineerTypes?.FirstOrDefault(x => x.TypeName == "Aprovisionamiento")?.EngineerTypeId)
            {
                engineers = _context.Engineers
                                .Where(x => x.EngineerTypeId == engineerTypeId && x.ActiveStatus == -1)
                                .Select(x => new EngineerToAssign
                                {
                                    EngineerId = x.EngineerId,
                                    Name = x.Name ?? "",
                                    UserName = x.UserName,
                                    IsActive = x.ActiveStatus == -1,
                                    EngineerType = x.EngineerType.TypeName,
                                    Region = x.IdRegionNavigation == null ? "" : x.IdRegionNavigation.Description ?? ""
                                }).ToList();
            }
            else if (engineerTypeId == _context.EngineerTypes?.FirstOrDefault(x => x.TypeName == "Mantenimiento")?.EngineerTypeId)
            {
                engineers = _context.Engineers
                                .Where(x => x.EngineerTypeId == engineerTypeId && x.ActiveStatus == -1 && x.IdRegion == ticketRegion)
                                .Select(x => new EngineerToAssign
                                {
                                    EngineerId = x.EngineerId,
                                    Name = x.Name ?? "",
                                    UserName = x.UserName,
                                    IsActive = x.ActiveStatus == -1,
                                    EngineerType = x.EngineerType.TypeName,
                                    Region = x.IdRegionNavigation == null ? "" : x.IdRegionNavigation.Description ?? ""
                                }).ToList();
            }
            else
            {
                engineers = [];
            }

            return engineers;
        }

        /// <summary>
        /// Asignar ticket por ingeniero
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> AssignTicketByEngineerId(int prefixId, int ticketId, int engineerId, string userName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var ticket = await _context.ServiceDeskTickets.FindAsync(prefixId, ticketId);
                var statusAssign = await _context.TicketStates.FirstOrDefaultAsync(x => x.Description == "Asignado");
                var assigner = await _context.Engineers.FirstOrDefaultAsync(x => x.UserName == userName);

                if (ticket != null)
                {
                    ticket.Assignee = engineerId;
                    ticket.Assigner = assigner?.EngineerId;
                    ticket.IdTicketState = statusAssign?.IdTicketState == null ? 0 : statusAssign.IdTicketState;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();

                    var history = new ServiceDeskTicketStatusHistory()
                    {
                        IdPrefix = prefixId,
                        IdTicket = ticketId,
                        IdTicketState = statusAssign?.IdTicketState == null ? 0 : statusAssign.IdTicketState,
                        ChangeDateTime = DateTime.Now,
                        ChangedByEngineerId = engineerId,
                        ChangedByUser = userName
                    };
                    await _context.ServiceDeskTicketStatusHistories.AddAsync(history);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Finalizar ticket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> FinishTicket(FinishTicketRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var ticket = await _context.ServiceDeskTickets.FindAsync(request.PrefixId, request.TicketId);
                var statusFinalize = await _context.TicketStates.FirstOrDefaultAsync(x => x.Description == "Finalizado");
                var changeBy = await _context.Engineers.FirstOrDefaultAsync(x => x.UserName == request.UserName);

                if (ticket != null)
                {
                    ticket.IdTicketState = statusFinalize?.IdTicketState == null ? 0 : statusFinalize.IdTicketState;
                    ticket.IdSolutionType = request.SolutionTypeId;
                    ticket.IsTechnicalVisitRequired = request.IsTechnicalVisitRequired;
                    ticket.IsBrigadeDeployed = request.IsBrigadeDeployed;
                    ticket.IsChangeNetworkTopology = request.IsChangeNetworkTopology;
                    ticket.WorkDone = request.WorkDone;
                    ticket.IdConfirmedArea = request.IdConfirmedArea;
                    ticket.IncidentClosureSummary = request.IncidentClosureSummary;
                    ticket.Finisher = changeBy?.EngineerId ?? 0;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();

                    var history = new ServiceDeskTicketStatusHistory()
                    {
                        IdPrefix = request.PrefixId,
                        IdTicket = request.TicketId,
                        IdTicketState = statusFinalize?.IdTicketState == null ? 0 : statusFinalize.IdTicketState,
                        ChangeDateTime = DateTime.Now,
                        ChangedByEngineerId = changeBy?.EngineerId ?? 0,
                        ChangedByUser = request.UserName
                    };
                    await _context.ServiceDeskTicketStatusHistories.AddAsync(history);
                    await _context.SaveChangesAsync();

                    //Se guardan archivos
                    if (request?.Files != null && request.Files.Any())
                    {
                        foreach (var file in request.Files)
                        {
                            UploadFileTicket(file, ticket.IdPrefix, ticket.IdTicket, (int)TicketFileTypeStatus.Final);
                        }
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtener municipio y departamento por coordenadas ingresadas
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task<(int? IdMunicipality, int? IdState, string Municipality)> GetMunicipalityAndDepartmentByCoordinates(double latitude, double longitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var point = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            var result = await (
                from ms in _context.Municipalityshapes
                where ms.OgrGeometry.Contains(point)
                join m in _context.Municipalities
                    on ms.IdMunicipality equals m.IdMunicipality
                select new
                {
                    m.IdMunicipality,
                    m.IdState,
                    m.Name
                }
            ).FirstOrDefaultAsync();

            return result != null
                ? (result.IdMunicipality, result.IdState, result.Name)
                : (null, null, null);
        }
    }
}
