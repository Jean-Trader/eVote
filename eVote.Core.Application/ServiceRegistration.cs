using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eVote.Core.Application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

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
            services.AddTransient<IDefaultUser, DefaultUser>();
        }

    }
}
