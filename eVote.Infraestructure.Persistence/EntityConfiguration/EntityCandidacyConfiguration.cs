using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityCandidacyConfiguration : IEntityTypeConfiguration<Candidacy>
    {
        public void Configure(EntityTypeBuilder<Candidacy> builder)
        {
            builder.ToTable("Candidacies");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CandidateId).IsRequired();
            builder.Property(c => c.ElectivePositionId).IsRequired();
            builder.Property(c => c.RegistrationDate).IsRequired();
            builder.Property(c => c.PartyId).IsRequired();
            


            builder.HasOne<Candidate>(c => c.Candidate)
                   .WithMany()
                   .HasForeignKey(c => c.CandidateId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ElectivePosition>(c => c.ElectivePosition)
                   .WithMany()
                   .HasForeignKey(c => c.ElectivePositionId)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
