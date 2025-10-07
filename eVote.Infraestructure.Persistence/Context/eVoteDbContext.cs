using eVote.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eVote.Infraestructure.Persistence.Context
{
    public class eVoteDbContext : DbContext
    {
        public DbSet<Party> Parties { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<Candidacy> Candidacies { get; set; }
        public DbSet<AllianceRequest> AllianceRequests { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Vote> Voters { get; set; }
        public DbSet<ElectivePosition> ElectivePositions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<PartyLeader> PartyLeaders { get; set; }


        public eVoteDbContext(DbContextOptions<eVoteDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
