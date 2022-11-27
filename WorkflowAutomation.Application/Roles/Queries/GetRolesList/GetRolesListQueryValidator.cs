using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Roles.Queries.GetRolesList
{
    public class GetRolesListQueryValidator : AbstractValidator<GetRolesListQuery>
    {
        public GetRolesListQueryValidator()
        {
            RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.UserId).NotEmpty();
        }
    }
}