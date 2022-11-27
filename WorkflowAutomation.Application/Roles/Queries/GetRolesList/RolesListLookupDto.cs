using AutoMapper;
using System;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Roles.Queries.GetRolesList
{
    public class RolesListLookupDto
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}