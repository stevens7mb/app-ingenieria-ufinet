namespace app_ingenieria_ufinet.Models.ServiceDesk
{
    public class ServiceDeskTicketViewModel
    {
        public required int PrefixId { get; set; }
        public required int TicketId { get; set; }
        public required string TicketCode { get; set; }
        public required string TicketName { get; set; }  
        public string? Creator { get; set; }
        public DateTime? CreatedDateTime {  get; set; }
        public string? Assigner { get; set; }
        public string? IncidentTypeDescription { get; set; } 
        public string? Assignee { get; set; }
        public string? TicketStatus {  get; set; }
    }
}