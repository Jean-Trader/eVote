using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityCandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidates");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Photo).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Status).IsRequired();
            
            builder.HasOne<Party>(c => c.Party)
                  .WithMany(p => p.Candidates)
                  .HasForeignKey(c => c.PartyId)
                  .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
