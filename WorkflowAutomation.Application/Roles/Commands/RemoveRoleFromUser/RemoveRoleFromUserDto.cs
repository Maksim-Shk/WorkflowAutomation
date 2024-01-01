using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Roles.Commands.RemoveRoleFromUser;

public class RemoveRoleFromUserDto : IMapWith<RemoveRoleFromUserCommand>
{
    public string UserId { get; set; }
    public string RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RemoveRoleFromUserDto, RemoveRoleFromUserCommand>()
               .ForMember(roleCommand => roleCommand.UserId,
                opt => opt.MapFrom(roleDto => roleDto.UserId))
               .ForMember(roleCommand => roleCommand.RoleId,
                opt => opt.MapFrom(roleDto => roleDto.RoleId));
    }
}