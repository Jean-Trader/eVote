using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityAllianceRequestConfiguration : IEntityTypeConfiguration<AllianceRequest>
    {
        public void Configure(EntityTypeBuilder<AllianceRequest> builder)
        {
            builder.ToTable("AllianceRequests");
            builder.HasKey(ar => ar.Id);


            builder.Property(ar => ar.RequestingPartyId).IsRequired();
            builder.Property(ar => ar.ReceivingPartyId).IsRequired();
            builder.Property(ar => ar.RequestDate).IsRequired();
            builder.Property(ar => ar.Status).IsRequired();


            builder.HasOne<Party>(ar => ar.RequestingParty)
                   .WithMany()
                   .HasForeignKey(ar => ar.RequestingPartyId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Party>(ar => ar.ReceivingParty)
                   .WithMany()
                   .HasForeignKey(ar => ar.ReceivingPartyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
    
}
