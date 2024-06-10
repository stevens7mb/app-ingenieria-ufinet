using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.ServiceDesk;

namespace app_ingenieria_ufinet.Repositories.ServiceDesk
{
    public interface IServiceDeskRepository
    {
        /// <summary>
        /// Get Generate tickets
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTableResponse<ServiceDeskTicketViewModel>> TicketsPaginateAsync(DataTableRequest request);

        /// <summary>
        /// Create ticket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CreateTicketResponse CreateTicket(CreateTicketRequest request);

        /// <summary>
        /// Obtiene información del ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<Ticket?> GetTicketAsync(int prefixId, int ticketId);

        /// <summary>
        /// Guarda registro en la bitacora
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <param name="commentary"></param>
        /// <returns></returns>
        Task<bool> SaveTicketLogAsync(int prefixId, int ticketId, int engineerId, string commentary);

        /// <summary>
        /// Obtiene registros de bitacora por ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<List<TicketBinnacle>> GetTicketBinnaclesAsync(int prefixId, int ticketId);

        /// <summary>
        /// Obtener ingenieros por tipo y ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerTypeId"></param>
        /// <returns></returns>
        public List<EngineerToAssign> GetEngineersToAssign(int prefixId, int ticketId, int engineerTypeId);

        /// <summary>
        /// Asignar ticket por ingeniero
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> AssignTicketByEngineerId(int prefixId, int ticketId, int engineerId, string userName);

        /// <summary>
        /// Finalizar ticket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> FinishTicket(FinishTicketRequest request);
    }
}
