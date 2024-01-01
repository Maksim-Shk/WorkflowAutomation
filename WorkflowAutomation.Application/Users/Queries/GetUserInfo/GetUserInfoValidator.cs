using FluentValidation;

namespace WorkflowAutomation.Application.Users.Queries.GetUserInfo;

public class GetAllUsersListValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetAllUsersListValidator()
    {
        RuleFor(getSubdivisionInfoValidator => getSubdivisionInfoValidator.UserId).NotNull();
    }
}
