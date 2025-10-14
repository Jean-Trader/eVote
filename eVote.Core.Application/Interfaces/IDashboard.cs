using eVote.Core.Application.DTOs.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Interfaces
{
    public interface IDashboard
    {
        Task<PoliticalHomeDto>GetDashboard(int id);
    }
}
