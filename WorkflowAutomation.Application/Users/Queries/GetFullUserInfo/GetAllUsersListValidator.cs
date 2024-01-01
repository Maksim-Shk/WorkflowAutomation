using FluentValidation;

namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo;

public class GetSubdivisionInfoValidator : AbstractValidator<GetFullUserInfoQuery>
{
    public GetSubdivisionInfoValidator()
    {
        RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.RequestingUserId).NotNull();
        RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.RequestedUserId).NotNull();
    }
}