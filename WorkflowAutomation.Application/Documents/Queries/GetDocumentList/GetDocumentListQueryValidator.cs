using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetDocumentListQueryValidator : AbstractValidator<GetDocumentListQuery>
    {
        public GetDocumentListQueryValidator()
        {
            RuleFor(getDocumentListQuery => getDocumentListQuery.UserId).NotEmpty();
        }
    }
}