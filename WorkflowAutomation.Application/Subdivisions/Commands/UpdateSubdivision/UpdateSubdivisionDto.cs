using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

public class UpdateSubdivisionDto : IMapWith<UpdateSubdivisionCommand>
{
    /// <summary>
    /// ID измененного подразделения
    /// </summary>
    public int SubdivisionId { get; set; }
    /// <summary>
    /// Название подразделения
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Дата создания подразделения
    /// </summary>
    // задается автоматически при создании, но может быть изменена
    public DateTime? CreateDate { get; set; }
    /// <summary>
    /// ID подразделения, к которому будет перепривязано текущее подразделение
    /// </summary>
    public int? SubordinationId { get; set; }
    /// <summary>
    /// Список пользователей с изменениями
    /// </summary>
    public List<UpdatesSubUsers>? UpdatedSubdivisionUsers { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSubdivisionDto, UpdateSubdivisionCommand>()
               .ForMember(subCommand => subCommand.SubdivisionId,
                opt => opt.MapFrom(subDto => subDto.SubdivisionId))
               .ForMember(subCommand => subCommand.Name,
                opt => opt.MapFrom(subDto => subDto.Name))
               .ForMember(subCommand => subCommand.CreateDate,
                opt => opt.MapFrom(subDto => subDto.CreateDate))
               .ForMember(subCommand => subCommand.SubordinationId,
                opt => opt.MapFrom(subDto => subDto.SubordinationId))
               .ForMember(subCommand => subCommand.UpdatedSubdivisionUsers,
                opt => opt.MapFrom(subDto => subDto.UpdatedSubdivisionUsers));
    }
}
