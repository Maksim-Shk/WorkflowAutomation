using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

public class UpdateSubdivisionDto : IMapWith<UpdateSubdivisionCommand>
{
    /// <summary>
    /// ID ����������� �������������
    /// </summary>
    public int SubdivisionId { get; set; }
    /// <summary>
    /// �������� �������������
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// ���� �������� �������������
    /// </summary>
    // �������� ������������� ��� ��������, �� ����� ���� ��������
    public DateTime? CreateDate { get; set; }
    /// <summary>
    /// ID �������������, � �������� ����� ������������� ������� �������������
    /// </summary>
    public int? SubordinationId { get; set; }
    /// <summary>
    /// ������ ������������� � �����������
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
