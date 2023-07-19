using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace WorkflowAutomation.Application.Positions.Queries.GetPositionList
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
