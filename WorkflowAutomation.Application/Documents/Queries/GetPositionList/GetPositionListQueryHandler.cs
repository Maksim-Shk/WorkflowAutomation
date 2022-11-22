using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetPositionList
{
    public class GetPositionListQueryHandler
        : IRequestHandler<GetPositionListQuery, PositionListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPositionListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PositionListVm> Handle(GetPositionListQuery request,
            CancellationToken cancellationToken)
        {
            var positionsQuery = await _dbContext.Positions
                .ProjectTo<PositionListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PositionListVm { Positions = positionsQuery };
        }
    }
}
