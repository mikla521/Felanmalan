namespace Felanmalan.Server.Entities
{
    public class Changes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Change { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
