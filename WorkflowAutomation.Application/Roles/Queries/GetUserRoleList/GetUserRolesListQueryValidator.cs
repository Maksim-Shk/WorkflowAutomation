using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Roles.Queries.GetUserRolesList
{
    public class GetRolesListQueryValidator : AbstractValidator<GetUserRolesListQuery>
    {
        public GetRolesListQueryValidator()
        {
            RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.UserId).NotEmpty();
        }
    }
}