using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Contracts;

public class TicketListItemDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public TicketCategory Category { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime TimeCreated { get; set; }
}
