using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipometryAppAPI.Data.EntityMapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //builder.OwnsOne(p => p.BodyDetails)
            //    .HasData(new {
            //        PersonId = 1,
            //        WeightInKg = 75.0,
            //        HeightInCm = 180.0,
            //        WaistInCm = 100.0,
            //        HipInCm = 90.0,
            //        NeckInCm = 40.0
            //    });

            //builder.ComplexProperty(p => p.BodyDetails)
            //    .IsRequired();

            builder.HasKey(p => p.PersonId);
            
            //builder.HasOne(p => p.BodyDetails)
            //    .WithOne(bd => bd.Person)
            //    .HasPrincipalKey<PersonBodyDetails>(bd => bd.Id)
            //    .HasForeignKey<Person>(p => p.BodyDetailsId)
            //    .IsRequired(false);

            builder.HasData(
                new Person
                {
                    PersonId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    WeightInKg = 75.0,
                    HeightInCm = 180.0,
                    WaistInCm = 100,
                    HipInCm = 99,
                    NeckInCm = 40.0,
                    PersonGender = PersonGender.Male,
                }
            );
        }
    }
}
