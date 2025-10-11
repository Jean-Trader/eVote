using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace eVote.Core.Application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Especifica los ensamblados donde están los perfiles de AutoMapper
            
            services.AddTransient<IAllianceService, AllianceService>();
            services.AddTransient<IAllianceRequestService, AllianceRequestService>();
            services.AddTransient<ICandidacyServices, CandidacyService>();
            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<ICitizenServices, CitizenService>();
            services.AddTransient<IElectionService, ElectionService>();
            services.AddTransient<IElectivePositionService, ElectivePositionService>();
            services.AddTransient<IPartyLeaderService, PartyLeaderService>();
            services.AddTransient<IPartyServices, PartyService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVoteServices, VoteService>();
        }

    }
}
