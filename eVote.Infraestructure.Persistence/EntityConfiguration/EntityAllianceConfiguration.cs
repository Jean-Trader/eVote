using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace eVote.Infrastructure.Persistence.EntityConfiguration
{
    public class EntityAllianceConfiguration : IEntityTypeConfiguration<Alliance>
    {
        public void Configure(EntityTypeBuilder<Alliance> builder)
        {
            builder.ToTable("Alliances");
            builder.HasKey(a => a.Id);



            builder.Property(a => a.AcceptedDate).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Status).IsRequired();


            builder.HasOne<Party>(a => a.Party1)
               .WithMany()
               .HasForeignKey(a => a.Party1Id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Party>(a => a.Party2)
                .WithMany()
                .HasForeignKey(a => a.Party2Id)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
