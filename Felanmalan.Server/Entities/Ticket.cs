using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public TicketCategory Category { get; set; }  //Enum ligger i mapp Enums
        public TicketStatus Status { get; set; } //Enum ligger i mapp Enums
        public DateTime TimeCreated { get; private set; } = DateTime.UtcNow;
        public DateTime? TimeStarted { get; set; }
    }
}
