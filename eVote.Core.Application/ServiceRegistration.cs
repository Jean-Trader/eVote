using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace eVote.Core.Application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            
            
            services.AddScoped<IAllianceService, AllianceService>();
            services.AddScoped<IAllianceRequestService, AllianceRequestService>();
            services.AddScoped<ICandidacyServices, CandidacyService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICitizenServices, CitizenService>();
            services.AddScoped<IElectionService, ElectionService>();
            services.AddScoped<IElectivePositionService, ElectivePositionService>();
            services.AddScoped<IPartyLeaderService, PartyLeaderService>();
            services.AddScoped<IPartyServices, PartyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVoteServices, VoteService>();
        }

    }
}
