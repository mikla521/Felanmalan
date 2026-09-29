namespace Felanmalan.Server.Contracts;

public class ChangeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Change { get; set; } = string.Empty;
    public int TicketId { get; set; }
}
