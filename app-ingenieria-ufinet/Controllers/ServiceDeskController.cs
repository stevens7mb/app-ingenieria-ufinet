using app_ingenieria_ufinet.Models.Commons.DataTablePaginate;
using app_ingenieria_ufinet.Models.ServiceDesk;
using app_ingenieria_ufinet.Repositories.Common;
using app_ingenieria_ufinet.Repositories.ServiceDesk;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace app_ingenieria_ufinet.Controllers
{
    public class ServiceDeskController(IServiceDeskRepository serviceRepository, ICommonRepository commonRepository) : Controller
    {
        private readonly IServiceDeskRepository _serviceDeskRepository = serviceRepository;
        private readonly ICommonRepository _commonRepository = commonRepository;

        public IActionResult ServiceDeskMain()
        {
            return View();
        }

        public IActionResult ServiceDeskTickets()
        {
            return View();
        }

        [HttpPost]
        public Task<DataTableResponse<ServiceDeskTicketViewModel>> GetServiceDeskTickets(DataTableRequest request)
        {
            request.Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
            request.Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            request.Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            request.Search = new DataTableSearch()
            {
                Value = Request.Form["search[value]"].FirstOrDefault() ?? string.Empty,
            };
            request.Order = [
            new DataTableOrder()
            {
                Dir = Request.Form["order[0][dir]"].FirstOrDefault() ?? string.Empty,
                Column = Convert.ToInt32(Request.Form["order[0][column]"].FirstOrDefault())
            }];

            return _serviceDeskRepository.TicketsPaginateAsync(request);
        }

        [HttpPost]
        public JsonResult CreateTicket(CreateTicketRequest createTicketRequest)
        {
            var userNameClaim = ((User.Identity as ClaimsIdentity)?.FindFirst("IdUsuario")?.Value) ?? "";
            createTicketRequest.UserCreate = userNameClaim;

            try
            {
                // Crea ticket
                CreateTicketResponse response = _serviceDeskRepository.CreateTicket(createTicketRequest);

                return Json(new { success = true, message = "Ticket Creado correctamente " + response.PrefixDesc + "-" + response.TicketId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al crear ticket: " + ex.Message });
            }
        }

        /// <summary>
        /// Controlador obtener ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/GetTicketAsync")]
        [HttpGet]
        public async Task<JsonResult> GetTicketAsync(int prefixId, int ticketId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                // Obtener el ticket de manera asincrónica
                Ticket? ticket = await _serviceDeskRepository.GetTicketAsync(prefixId, ticketId);

                if (ticket != null)
                {
                    return Json(new { success = true, message = "Ticket obtenido exitosamente", data = ticket });
                }
                else
                {
                    return Json(new { success = false, message = "No se encontró el ticket" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al cargar el ticket: " + ex.Message });
            }
        }

        /// <summary>
        /// Controlador obtener ticket
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/SaveBinnacleLogAsync")]
        [HttpPost]
        public async Task<JsonResult> SaveBinnacleLogAsync(int prefixId, int ticketId, string commentary)
        {
            try
            {
                bool result = false;

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                var userNameClaim = ((User.Identity as ClaimsIdentity)?.FindFirst("IdUsuario")?.Value) ?? "";

                int? engineerId = _commonRepository.GetEngineerIdByUser(userNameClaim);

                if (engineerId != null)
                {
                    // Obtener el ticket de manera asincrónica
                    result = await _serviceDeskRepository.SaveTicketLogAsync(prefixId, ticketId, (int)engineerId, commentary);
                }

                if (result)
                {
                    return Json(new { success = true, message = "Registro guardado exitosamente en la bitácora" });
                }
                else
                {
                    return Json(new { success = false, message = "Ha ocurrido un error al guardar la bitácora, error en servicio" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al guardar la bitácora" + ex.Message });
            }
        }

        /// <summary>
        /// Obtener bitacora
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/GetTicketBinnaclesAsync")]
        [HttpGet]
        public async Task<JsonResult> GetTicketBinnacles(int prefixId, int ticketId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                var result = await _serviceDeskRepository.GetTicketBinnaclesAsync(prefixId, ticketId);

                return Json(new { success = true, message = "Bitacora obtenida exitosamente", data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al obtener la bitacora" + ex.Message });
            }
        }

        /// <summary>
        /// Obtener bitacora
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/GetEngineersToAssign")]
        [HttpGet]
        public JsonResult GetEngineersToAssign(int prefixId, int ticketId, int engineerTypeId) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                var result = _serviceDeskRepository.GetEngineersToAssign(prefixId, ticketId, engineerTypeId);

                return Json(new { success = true, message = "Ingenieros obtenidos exitosamente", data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al obtener ingenieros" + ex.Message });
            }
        }

        /// <summary>
        /// Asignar ticket por id de ingeniero
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/AssignTicketByEngineerId")]
        [HttpPost]
        public JsonResult AssignTicketByEngineerId(int prefixId, int ticketId, int engineerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                var userNameClaim = ((User.Identity as ClaimsIdentity)?.FindFirst("IdUsuario")?.Value) ?? "";

                var result = _serviceDeskRepository.AssignTicketByEngineerId(prefixId, ticketId, engineerId, userNameClaim);

                string message = "Ticket Asignado" + (result.Result ? " correctamente" : " con error");

                return Json(new { success = result.Result, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al asignar el ticket" + ex.Message });
            }
        }

        /// <summary>
        /// Asignar ticket por id de ingeniero
        /// </summary>
        /// <param name="prefixId"></param>
        /// <param name="ticketId"></param>
        /// <param name="engineerId"></param>
        /// <returns></returns>
        [Route("ServiceDesk/FinishTicket")]
        [HttpPost]
        public JsonResult FinishTicket(FinishTicketRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Datos de entrada no válidos" });
                }

                var userNameClaim = ((User.Identity as ClaimsIdentity)?.FindFirst("IdUsuario")?.Value) ?? "";
                request.UserName = userNameClaim;

                var result = _serviceDeskRepository.FinishTicket(request);

                string message = "Ticket Finalizado" + (result.Result ? " correctamente" : " con error");

                return Json(new { success = result.Result, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ha ocurrido un error al finalizar ticket" + ex.Message });
            }
        }
    }
}
