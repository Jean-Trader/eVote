using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityPartyLeaderConfiguration : IEntityTypeConfiguration<PartyLeader>
    {
        public void Configure(EntityTypeBuilder<PartyLeader> builder)
        {
            builder.ToTable("PartyLeaders");
            builder.HasKey(pl => pl.Id);
            
            builder.Property(pl => pl.PartyId).IsRequired();
            builder.Property(pl => pl.UserId).IsRequired();


            builder.HasOne<Party>(pl => pl.Party)
                   .WithMany()
                   .HasForeignKey(pl => pl.PartyId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>(pl => pl.User)
                   .WithMany()
                   .HasForeignKey(pl => pl.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
    

    
}
