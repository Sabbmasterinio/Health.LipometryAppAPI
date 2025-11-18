using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipometryAppAPI.Data.EntityMapping
{
    public class PersonBodyDetailsMapping : IEntityTypeConfiguration<PersonBodyDetails>
    {
        public void Configure(EntityTypeBuilder<PersonBodyDetails> builder)
        {
            builder.HasData(
                new PersonBodyDetails
                {
                    Id = 1,
                    WaistInCm = 100.0,
                    HipInCm = 90.0,
                    NeckInCm = 40.0
                }
            );
        }
    }
}
