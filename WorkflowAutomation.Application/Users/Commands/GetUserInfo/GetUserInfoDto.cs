using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Commands.GetUserInfo
{
    public class GetUserInfoDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string SubdivisionName { get; set; }
        public string PositonName { get; set; }
    }
}
