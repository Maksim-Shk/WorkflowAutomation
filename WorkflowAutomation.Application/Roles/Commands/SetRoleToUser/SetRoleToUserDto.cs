using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Roles.Commands.SetRoleToUser;

public class SetRoleToUseDto : IMapWith<SetRoleToUserCommand>
{
    public string UserId { get; set; }
    public string RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleToUseDto, SetRoleToUserCommand>()
               .ForMember(roleCommand => roleCommand.UserId,
                opt => opt.MapFrom(roleDto => roleDto.UserId))
               .ForMember(roleCommand => roleCommand.RoleId,
                opt => opt.MapFrom(roleDto => roleDto.RoleId));
    }
}