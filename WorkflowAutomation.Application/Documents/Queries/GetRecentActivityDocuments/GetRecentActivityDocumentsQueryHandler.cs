using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Application.Interfaces;
using Document = WorkflowAutomation.Domain.Document;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments
{
    public class GetRecentActivityDocumentsQueryHandler 
        : IRequestHandler<GetRecentActivityDocumentsQuery, RecentActivityDocumentListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetRecentActivityDocumentsQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<RecentActivityDocumentListVm> Handle(GetRecentActivityDocumentsQuery request,
            CancellationToken cancellationToken)
        {
            List<RecentActivityDocumentLookupDto> RecentDocuments = new List<RecentActivityDocumentLookupDto>();
            var documents = await _dbContext.Documents.Where(x => x.IdSender == request.UserId ||
                                                          x.IdReceiver == request.UserId).ToListAsync();
            var documentsStatus = await _dbContext.DocumentStatuses.Include(x => x.IdDocumentNavigation).ToListAsync();

            List<RecentActivityDocumentLookupDto> DtoDocs = new List<RecentActivityDocumentLookupDto>();
            for (int i=0; i < 5; i++)
            {
                var docStat = documentsStatus.First(y => y.AppropriationDate == documentsStatus.Max(x => x.AppropriationDate));
                RecentActivityDocumentLookupDto dto = new RecentActivityDocumentLookupDto();
                dto.LastActivityDate = docStat.AppropriationDate;
                dto.Titile = documents.First(x => x.IdDocument == docStat.IdDocument).Title;
                documentsStatus.Remove(docStat);
                RecentDocuments.Add(dto);
            }
            return new RecentActivityDocumentListVm { RecentDocuments = RecentDocuments };
        }
    }
}
