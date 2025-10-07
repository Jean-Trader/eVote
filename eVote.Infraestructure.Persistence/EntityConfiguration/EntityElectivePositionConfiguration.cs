using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityElectivePositionConfiguration : IEntityTypeConfiguration<ElectivePosition>
    {
        public void Configure(EntityTypeBuilder<ElectivePosition> builder)
        {
            builder.ToTable("ElectivePositions");
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Name).IsRequired().HasMaxLength(100);
            builder.Property(ep => ep.Description).IsRequired().HasMaxLength(250);
            builder.Property(ep => ep.Status).IsRequired().HasDefaultValue(true);
        }
    }
}
