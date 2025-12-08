using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipometryAppAPI.Data.EntityMapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.PersonId);


            //builder.HasData(
            //    new Person
            //    {
            //        PersonId = 1,
            //        FirstName = "John",
            //        LastName = "Doe",
            //        DateOfBirth = new DateOnly(1990, 1, 1),
            //        WeightInKg = 75.0,
            //        HeightInCm = 180.0,
            //        WaistInCm = 100,
            //        HipInCm = 99,
            //        NeckInCm = 40.0,
            //        PersonGender = PersonGender.Male,
            //    }
            //);
        }
    
    }
}
