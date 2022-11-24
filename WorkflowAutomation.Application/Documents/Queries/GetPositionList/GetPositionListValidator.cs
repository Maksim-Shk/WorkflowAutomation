using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetPositionList
{
    public class GetPositionListValidator : AbstractValidator<GetPositionListQuery>
    {
        public GetPositionListValidator()
        {
            RuleFor(getPositionListValidator => getPositionListValidator.UserId).NotEmpty();
        }
    }
}
