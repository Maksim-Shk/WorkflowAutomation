using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoDto : IMapWith<CreateUserInfoCommand>
    {
        [Required(ErrorMessage = "������� ���")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "��� ������ ���� �� ����� 2-� �������� � �� ����� 50-��")]
        public string Name { get; set; } = null;

        [Required(ErrorMessage = "������� �������")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "������� ������ ���� �� ����� 2-� �������� � �� ����� 50-��")]
        public string Surname { get; set; } = null;

        [Required(ErrorMessage = "������� ��������")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "�������� ������ ���� �� ����� 2-� �������� � �� ����� 50-��")]
        public string Patronymic { get; set; } = null;
        [Required(ErrorMessage = "������� �������������")]
        public int IdSubdivision { get; set; }
        [Required(ErrorMessage = "������� ���������")]
        public int IdPositon { get; set; } 

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserInfoDto, CreateUserInfoCommand>()
                   .ForMember(userCommand => userCommand.Name,
                    opt => opt.MapFrom(userDto => userDto.Name))
                   .ForMember(userCommand => userCommand.Surname,
                    opt => opt.MapFrom(userDto => userDto.Surname))
                   .ForMember(userCommand => userCommand.Patronymic,
                    opt => opt.MapFrom(userDto => userDto.Patronymic))
                   .ForMember(userCommand => userCommand.IdSubdivision,
                    opt => opt.MapFrom(userDto => userDto.IdSubdivision))
                   .ForMember(userCommand => userCommand.IdPositon,
                    opt => opt.MapFrom(userDto => userDto.IdPositon));
        }
    }
}
