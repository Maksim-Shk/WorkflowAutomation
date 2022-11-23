using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetAllowedUsers
{
    public class AllowedUserListVm
    {
        public IList<GetAllowedUserListDto> AllowedUsers { get; set; }
    }
}
