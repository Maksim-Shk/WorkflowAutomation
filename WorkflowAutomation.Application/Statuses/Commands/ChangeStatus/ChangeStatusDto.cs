using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Statuses.Commands.ChangeStatus;

public class ChangeStatusDto : IMapWith<ChangeStatusCommand>
{
   // public string? UserId { get; set; }
    public int DocumentId { get; set; }
    public int StatusId { get; set; }
    public string? JwtToken { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChangeStatusDto, ChangeStatusCommand>()
             //  .ForMember(statusCommand => statusCommand.UserId,
             //   opt => opt.MapFrom(statusDto => statusDto.UserId))
               .ForMember(statusCommand => statusCommand.DocumentId,
                opt => opt.MapFrom(statusDto => statusDto.DocumentId))
               .ForMember(statusCommand => statusCommand.StatusId,
                opt => opt.MapFrom(statusDto => statusDto.StatusId))
               .ForMember(statusCommand => statusCommand.JwtToken,
                opt => opt.MapFrom(statusDto => statusDto.JwtToken));
    }
}