using LipometryAppAPI.Data.EntityMapping;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Data
{
    public class LipometryContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();
        public DbSet<Athlete> Athletes => Set<Athlete>();

        public LipometryContext(DbContextOptions<LipometryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new AthleteMapping());

            modelBuilder.Entity<Person>()
                .ToTable("People")
                .HasDiscriminator<string>("PersonType")
                .HasValue<Person>("Person")
                .HasValue<Athlete>("Athlete");
        }
    }
}
