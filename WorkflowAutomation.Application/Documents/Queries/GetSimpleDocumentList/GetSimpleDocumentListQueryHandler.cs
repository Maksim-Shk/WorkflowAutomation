using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetNoteListQueryHandler
        : IRequestHandler<GetSimpleDocumentListQuery, SimpleDocumentListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SimpleDocumentListVm> Handle(GetSimpleDocumentListQuery request,
            CancellationToken cancellationToken)
        {
            // List<DocumentUser> b = _dbContext.DocumentUsers.Where(x => x.IdReceiver == request.UserId)
            //     .Include(y => y.IdDocumentNavigation).ToList();
            // foreach (var item in b)
            // {
            //     item.IdDocumentNavigation.Title = "sa";
            // }
            //
            // var List = _dbContext.DocumentUsers.Where(x => x.IdReceiver == request.UserId)
            //     .Join(_dbContext.Documents,
            //           y => y.IdDocument,
            //           doc => doc.IdDocument,
            //           (du, d) => new Document
            //           {
            //               IdDocument = d.IdDocument,
            //               Title = d.Title,
            //               CreateDate = d.CreateDate,
            //               UpdateDate = d.UpdateDate,
            //               RemoveDate = d.RemoveDate,
            //               IdStatus = d.IdStatus,
            //               IdRoute = d.IdRoute,
            //           }).ToList();
            //

            var documentsQuery = await _dbContext.Documents
                .Where(x => x.IdReceiver == request.UserId || x.IdSender == request.UserId)
                //.Include(y => y.IdDocumentNavigation)
                .ProjectTo<SimpleDocumentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SimpleDocumentListVm { Documents = documentsQuery };
        }
    }
}
