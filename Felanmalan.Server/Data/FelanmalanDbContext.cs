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
        public DbSet<User> User { get; set; }
        public DbSet<Changes> Changes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Changes>()
                .HasOne(change => change.Ticket)
                .WithMany()
                .HasForeignKey(change => change.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
