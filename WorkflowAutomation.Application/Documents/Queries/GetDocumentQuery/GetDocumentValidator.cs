using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocument
{
    public class GetDocumentQueryValidator : AbstractValidator<GetDocumentQuery>
    {
        public GetDocumentQueryValidator()
        {
            RuleFor(getDocumentListQuery => getDocumentListQuery.DocumentId).NotEmpty();
        }
    }
}