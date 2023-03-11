using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery
{
    public class GetDocumentTypesQueryHandler
        : IRequestHandler<GetDocumentTypesQuery, DocumentTypeListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDocumentTypesQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DocumentTypeListVm> Handle(GetDocumentTypesQuery request,
            CancellationToken cancellationToken)
        {
            var documentsTypeQuery = await _dbContext.DocumentTypes
                .ProjectTo<DocumentTypeListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new DocumentTypeListVm { DocumentTypes = documentsTypeQuery };
        }
    }
}
