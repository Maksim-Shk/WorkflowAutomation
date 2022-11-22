using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoDto : IMapWith<CreateUserInfoCommand>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public int IdSubdivision { get; set; }
        [Required]
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
