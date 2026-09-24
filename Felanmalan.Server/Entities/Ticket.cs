using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TicketCategory Category { get; set; }  //Enum ligger i mapp Enums
        public TicketStatus Status { get; set; } //Enum ligger i mapp Enums
        public DateTime TimeCreated { get; } = DateTime.UtcNow;
        public DateTime? TimeStarted { get; set; }
    }
}
