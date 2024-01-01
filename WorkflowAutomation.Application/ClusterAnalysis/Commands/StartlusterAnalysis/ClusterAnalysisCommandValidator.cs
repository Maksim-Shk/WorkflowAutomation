using FluentValidation;
using WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;

public class ClusterAnalysisCommandValidator : AbstractValidator<ClusterAnalysisCommand>
{
    public ClusterAnalysisCommandValidator()
    {
        RuleFor(getDocumentTypesQuery => getDocumentTypesQuery.ClusterCount)
            .NotNull()
            .GreaterThan(0);
    }
}