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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=LipometryDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //    //Not proper logging
        //    optionsBuilder.LogTo(Console.WriteLine);
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new AthleteMapping());
            modelBuilder.Entity<Person>()
                .ToTable("People")
                .HasDiscriminator<string>("TypeDiscriminator")
                .HasValue<Person>("Person")
                .HasValue<Athlete>("Athlete");
        }
    }
}
