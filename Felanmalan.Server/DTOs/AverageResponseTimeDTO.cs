using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.DTOs;

public class AverageResponseTimeDTO
{
    public TicketCategory Category { get; set; }
    public double AverageResponseTimeInMinutes   { get; set; }
}
