using eVote.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Services
{
    public class ValidateElection : IValidateElection
    {
        protected readonly IElectionService _electionService;
        public ValidateElection(IElectionService electionService)
        {
            _electionService = electionService;
        }

        public bool ValidateExistActiveElection()
        {
            var elections = _electionService.GetAllWithDetails();
            if (elections.Any(x => x.Status == "Active"))
            {
                return true;
            }
            return false;
        }
    }
}
