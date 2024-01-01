using MediatR;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionUsersInfo;

public class SubdivisionUsersInfoQueryHandler :
    IRequestHandler<SubdivisionUsersInfoQuery, SubdivisionUsersInfoVm>
{
    private readonly IDocumentUserDbContext _dbContext;

    public SubdivisionUsersInfoQueryHandler(IDocumentUserDbContext dbContext) =>
            (_dbContext) = (dbContext);
    public async Task<SubdivisionUsersInfoVm> Handle(SubdivisionUsersInfoQuery request,
        CancellationToken cancellationToken)
    {
        var outputClustersDto = new List<SubdivisionUsersInfoDto>();

        return new SubdivisionUsersInfoVm { Users = outputClustersDto };
    }
}