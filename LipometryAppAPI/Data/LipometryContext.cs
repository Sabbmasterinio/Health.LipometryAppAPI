using LipometryAppAPI.Data.EntityMapping;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Data
{
    public class LipometryContext : DbContext
    {
        #region Public Members
        public DbSet<Person> People => Set<Person>();
        public DbSet<Athlete> Athletes => Set<Athlete>();
        #endregion

        #region Constructors
        public LipometryContext(DbContextOptions<LipometryContext> options)
            : base(options)
        {
        }
        #endregion

        #region Overriden Methods of DbContext
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
        #endregion
    }
}
