using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Shared
{
    public class CreateUserInfoDto 
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
    }
}
