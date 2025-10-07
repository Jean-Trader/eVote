
using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityVoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Votes");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.CitizenId).IsRequired();
            builder.Property(v => v.CandidateId).IsRequired();
            builder.Property(v => v.ElectivePositionId).IsRequired();
            builder.Property(v => v.ElectionId).IsRequired();
            builder.Property(v => v.VoteDate).IsRequired().HasDefaultValueSql("GETDATE()");



            builder.HasOne<ElectivePosition>(v => v.ElectivePosition)
                .WithMany()
                .HasForeignKey(v => v.ElectivePositionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Election>(v => v.Election)
                .WithMany()
                .HasForeignKey(v => v.ElectionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Candidate>(v => v.Candidate)
                .WithMany()
                .HasForeignKey(v => v.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Citizen>(v => v.Citizen)
                .WithMany()
                .HasForeignKey(v => v.CitizenId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

