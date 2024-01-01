using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Positions.Commands.CreateNewPositionCommand;

public class CreatePositionDto : IMapWith<CreatePositionCommand>
{
    public string Name { get; set; }
    public int? IdSubordination { get; set; }
    public string? ShortName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePositionDto, CreatePositionCommand>()
               .ForMember(subCommand => subCommand.Name,
                opt => opt.MapFrom(subDto => subDto.Name))
               .ForMember(subCommand => subCommand.IdSubordination,
                opt => opt.MapFrom(subDto => subDto.IdSubordination))
               .ForMember(subCommand => subCommand.ShortName,
                opt => opt.MapFrom(subDto => subDto.ShortName));
    }
}
