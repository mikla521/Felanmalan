using Felanmalan.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Felanmalan.Server.Data
{
    public class FelanmalanDbContext : DbContext
    {
        public FelanmalanDbContext(DbContextOptions<FelanmalanDbContext> options) : base(options)
        {
        }
        public DbSet<Ticket> Ticket { get; set; }
    }
}
