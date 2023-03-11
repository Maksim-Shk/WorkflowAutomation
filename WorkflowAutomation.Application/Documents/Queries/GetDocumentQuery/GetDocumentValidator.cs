using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetOneDocument
{
    public class GetDocumentQueryValidator : AbstractValidator<GetDocumentQuery>
    {
        public GetDocumentQueryValidator()
        {
            RuleFor(getDocumentListQuery => getDocumentListQuery.DocumentId).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(getDocumentListQuery => getDocumentListQuery.UserId).NotEmpty();
        }
    }
}