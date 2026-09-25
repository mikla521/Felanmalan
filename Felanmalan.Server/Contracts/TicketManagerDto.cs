using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Contracts
{
    public class TicketManagerDto
    {
        public int Id { get; set; }
        public DateTime? TimeStarted { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
