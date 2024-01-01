using FluentValidation;

namespace WorkflowAutomation.Application.Positions.Queries.GetPositionList;

public class GetPositionListValidator : AbstractValidator<GetPositionListQuery>
{
    public GetPositionListValidator()
    {
        RuleFor(getPositionListValidator => getPositionListValidator.UserId).NotEmpty();
    }
}
