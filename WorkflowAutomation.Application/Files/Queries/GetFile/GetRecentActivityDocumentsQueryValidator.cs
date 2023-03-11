using FluentValidation;

namespace WorkflowAutomation.Application.Files.Queries.GetFile
{
    public class GetSimpleDocumentListQueryValidator : AbstractValidator<GetFileQuery>
    {
        public GetSimpleDocumentListQueryValidator()
        {
            RuleFor(GetSimpleDocumentListQueryValidator => GetSimpleDocumentListQueryValidator.UserId).NotEmpty();
            RuleFor(GetSimpleDocumentListQueryValidator => GetSimpleDocumentListQueryValidator.FileId).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
