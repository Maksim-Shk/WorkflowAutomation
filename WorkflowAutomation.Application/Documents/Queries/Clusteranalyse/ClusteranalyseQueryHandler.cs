using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Documents.Queries.Clusteranalyse
{
    public class GetPositionListQueryHandler
        : IRequestHandler<ClusteranalyseQuery, ClusteranalyseVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPositionListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        //TODO ���������������� ������������� ���������� ������
        public async Task<ClusteranalyseVm> Handle(ClusteranalyseQuery request,
            CancellationToken cancellationToken)
        {
            var documents = await _dbContext.Documents.ToListAsync(cancellationToken);

            List<DateTime> times = new List<DateTime>();

            foreach (var document in documents)
            {
                
            }

            //var positionsQuery = await _dbContext.Positions
            //    .ProjectTo<PositionListLookupDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);

            return new ClusteranalyseVm { };
        }
    }
}
