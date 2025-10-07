using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityCitizenConfiguration : IEntityTypeConfiguration<Citizen>
    {
        public void Configure(EntityTypeBuilder<Citizen> builder) 
        {
            builder.ToTable("Citizens");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.IdentificationNumber).IsRequired().HasMaxLength(20);
           


        }
    }
}
