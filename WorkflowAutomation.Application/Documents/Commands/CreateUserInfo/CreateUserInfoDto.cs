using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoDto : IMapWith<CreateUserInfoCommand>
    {
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть не менее 2-х символов и не более 50-ти")]
        [RegularExpression(@"^([а-яА-я \.\&\'\-]+)$", ErrorMessage = "В имнени должна быть ипользована только кириллица")]
        public string Name { get; set; } = null;

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должно быть не менее 2-х символов и не более 50-ти")]
        [RegularExpression(@"^([а-яА-я \.\&\'\-]+)$", ErrorMessage = "В фамилии должна быть ипользована только кириллица")]
        public string Surname { get; set; } = null;

        [Required(ErrorMessage = "Введите отчество")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Отчество должно быть не менее 2-х символов и не более 50-ти")]
        [RegularExpression(@"^([а-яА-я \.\&\'\-]+)$", ErrorMessage = "В отчестве должна быть ипользована только кириллица")]
        public string Patronymic { get; set; } = null;
        [Required(ErrorMessage = "Введите подразделение")]
        public int IdSubdivision { get; set; }
        [Required(ErrorMessage = "Введите должность")]
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
