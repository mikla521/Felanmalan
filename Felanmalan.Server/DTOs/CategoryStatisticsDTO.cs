using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.DTOs;

public class CategoryStatisticsDTO
{
    public TicketCategory Category { get; set; }
    public int Count { get; set; }
}