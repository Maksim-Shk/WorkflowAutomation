using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetSimpleDocumentListQueryValidator : AbstractValidator<GetSimpleDocumentListQuery>
    {
        public GetSimpleDocumentListQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty.ToString());
        }
    }
}
