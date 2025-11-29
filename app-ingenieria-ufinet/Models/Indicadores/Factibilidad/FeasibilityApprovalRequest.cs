public class FeasibilityApprovalRequest
{
    public int FeasibilityId { get; set; }
    public string Ticket { get; set; } = "";
    public bool Approve { get; set; }
    public string Comment { get; set; } = "";
}
