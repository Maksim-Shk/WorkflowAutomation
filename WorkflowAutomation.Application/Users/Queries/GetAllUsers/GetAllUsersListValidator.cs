using FluentValidation;

namespace WorkflowAutomation.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersListValidator : AbstractValidator<GetAllUsersListQuery>
    {
        public GetAllUsersListValidator()
        {
            RuleFor(getSubdivisionInfoValidator => getSubdivisionInfoValidator.UserId).NotEmpty();
        }
    }
}