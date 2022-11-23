using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoDto : IMapWith<CreateUserInfoCommand>
    {
        [Required(ErrorMessage = "¬ведите им€")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "»м€ должно быть не менее 2-х символов и не более 50-ти")]
        public string Name { get; set; } = null;

        [Required(ErrorMessage = "¬ведите фамилию")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "‘амили€ должно быть не менее 2-х символов и не более 50-ти")]
        public string Surname { get; set; } = null;

        [Required(ErrorMessage = "¬ведите отчество")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "ќтчество должно быть не менее 2-х символов и не более 50-ти")]
        public string Patronymic { get; set; } = null;
        [Required(ErrorMessage = "¬ведите подразделение")]
        public int IdSubdivision { get; set; }
        [Required(ErrorMessage = "¬ведите должность")]
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
