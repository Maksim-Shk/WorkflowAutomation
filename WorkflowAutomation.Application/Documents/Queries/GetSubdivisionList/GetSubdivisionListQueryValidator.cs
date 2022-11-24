using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList
{
    public class GetSimpleDocumentListQueryValidator : AbstractValidator<GetSubdivisionListQuery>
    {
        public GetSimpleDocumentListQueryValidator()
        {
            RuleFor(getSimpleDocumentListQueryValidator => getSimpleDocumentListQueryValidator.UserId).NotEmpty();
        }
    }
}
