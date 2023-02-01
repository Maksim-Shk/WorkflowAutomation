using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
    public class GetSubdivisionInfoValidator : AbstractValidator<GetSubdivisionInfoQuery>
    {
        public GetSubdivisionInfoValidator()
        {
            RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.UserId).NotEmpty();
        }
    }
}