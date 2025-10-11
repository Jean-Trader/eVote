using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.DTOs.AllianceRequest;
using eVote.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Interfaces
{
    public interface IAllianceRequestService : IGenericsRepository<AllianceRequestDto>
    {
        List<AllianceRequestDto> GetAllWithDetails();
    }
}
