using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList
{
    public class GetSubdivisionListQueryHandler
        : IRequestHandler<GetSubdivisionListQuery, SubdivisionListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSubdivisionListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubdivisionListVm> Handle(GetSubdivisionListQuery request,
            CancellationToken cancellationToken)
        {
            var subdivisionsQuery = await _dbContext.Subdivisions
                .ProjectTo<SubdivisionListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SubdivisionListVm { Subdivisions = subdivisionsQuery };
        }
    }
}
