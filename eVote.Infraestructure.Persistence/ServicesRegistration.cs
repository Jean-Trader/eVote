using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using eVote.Core.Domain.Interfaces;
using eVote.Infraestructure.Persistence.Context;
using eVote.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVote.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<eVoteDbContext>(opt =>
            opt.UseSqlServer(connectionString,
            m => m.MigrationsAssembly(typeof(eVoteDbContext).Assembly.FullName))
            , ServiceLifetime.Transient);

            services.AddScoped<DbContext, eVoteDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAllianceRepository, AllianceRepository>();
            services.AddTransient<IAllianceRequestRepository, AllianceRequestRepository>();
            services.AddTransient<ICandidacyRepository, CandidacyRepository>();
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<ICitizenRepository, CitizenRepository>();
            services.AddTransient<IElectionRepository, ElectionRepository>();
            services.AddTransient<IElectivePositionRepository, ElectivePositionRepository>();
            services.AddTransient<IPartyLeaderRepository, PartyLeaderRepository>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IValidateElection, ValidateElection>();
            



        }

    }
}
