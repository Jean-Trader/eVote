using eVote.Core.Application.DTOs.CommonEntity;

namespace eVote.Core.Application.DTOs.ElectivePosition
{
 public class ElectivePositionDto : BaseEntityDto<int>
 {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool Status { get; set; } = true;
 }
}
