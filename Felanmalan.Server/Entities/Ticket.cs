namespace Felanmalan.Server.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime TimeCreated { get; } = DateTime.Now;
        public DateTime TimeStarted { get; set; }
    }
}


//Exempel: ID, beskrivning, kategori, status, skapad tidpunkt, påbörjad tidpunkt.