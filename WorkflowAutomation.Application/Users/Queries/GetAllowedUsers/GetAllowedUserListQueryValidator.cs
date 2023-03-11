using FluentValidation;

namespace WorkflowAutomation.Application.Users.Queries.GetAllowedUsers
{
    public class GetAllowedUserListQueryValidator : AbstractValidator<GetAllowedUserListQuery>
    {
        public GetAllowedUserListQueryValidator()
        {
            RuleFor(getRolesListQueryValidator => getRolesListQueryValidator.UserId).NotEmpty();
        }
    }
}