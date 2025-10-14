using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Services
{
    public class PoliticalDashboard : IDashboard
    {
        IPartyLeaderService _partyLeaderService;
        ICandidateService _candidateService;
        IAllianceService _allianceService;
        IAllianceRequestService _allianceRequestService;
        ICandidacyServices _candidacyServices;
        public PoliticalDashboard(ICandidateService candidateService, IAllianceService allianceService, IPartyLeaderService
            partyLeaderService,IAllianceRequestService request, ICandidacyServices candidacyServices)
        {
            _candidateService = candidateService;
            _allianceService = allianceService;
            _partyLeaderService = partyLeaderService;
            _allianceRequestService = request;
            _candidacyServices = candidacyServices;
        }

        public async Task<PoliticalHomeDto> GetDashboard(int id) 
        {
            PoliticalHomeDto politicalHome = new PoliticalHomeDto();

            var partyLeader = _partyLeaderService.GetAllWithDetails().Where(p => p.Id == id).FirstOrDefault();
            if (partyLeader == null)
            {
                politicalHome.PartyName = "";
                politicalHome.PartyAcronym = "";
                politicalHome.PartyLogoPath = "";
            }
            else 
            {

                politicalHome.PartyName = partyLeader.Party.Name;
                politicalHome.PartyAcronym = partyLeader.Party.Acronym;
                politicalHome.PartyLogoPath = partyLeader.Party.Logo;
            }

            var candidates = await _candidateService.GetAllAsync();
            var candidatesActives = candidates.Where(c => c.Status == true).Count();
            var candidatesInactives = candidates.Where(c => c.Status == false).Count();
            var A = await _allianceService.GetAllAsync();
            var allianceCount = A.Count();
            var allianceRequestResult = await _allianceRequestService.GetAllAsync();
            var alliances = allianceRequestResult.Where(c => c.Status == "Pendiente").Count();
            var candidacy = _candidacyServices.GetAllWithDetails();
            var candidatesWhitPosition = candidacy.Select(c => c.Candidate).Count();

            politicalHome.ActiveCandidates = candidatesActives;
            politicalHome.InactiveCandidates = candidatesInactives;
            politicalHome.PoliticalAlliances = allianceCount;
            politicalHome.PendingAllianceRequests = alliances;
            politicalHome.CandidatesAssigned = candidatesWhitPosition;
            
              return politicalHome;
        }
    }
}
