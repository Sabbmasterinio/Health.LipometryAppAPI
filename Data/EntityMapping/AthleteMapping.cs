using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipometryAppAPI.Data.EntityMapping
{
    public class AthleteMapping : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder.HasData(
                new Athlete
                {
                    AthleteId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    WeightInKg = 75.0,
                    HeightInCm = 180.0,
                    WaistInCm = 100,
                    HipInCm = 99,
                    NeckInCm = 40.0,
                    PersonGender = PersonGender.Male,
                    Sport = "Running"
                }
            );
        }
    }

}
