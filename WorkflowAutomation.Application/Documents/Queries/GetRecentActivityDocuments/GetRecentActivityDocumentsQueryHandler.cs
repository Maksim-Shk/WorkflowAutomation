using WorkflowAutomation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;

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
        var documents = await _dbContext.Documents.Where(x => x.IdSender == request.UserId ||
                                                      x.IdReceiver == request.UserId).ToListAsync();
        var documentsStatuses = await _dbContext.DocumentStatuses.Include(x => x.IdDocumentNavigation).ToListAsync();

        var documentIds = await _dbContext.Documents.Where(x => x.IdSender == request.UserId || x.IdReceiver == request.UserId)
                                              .Select(x => x.IdDocument).ToListAsync();

        var documentStatuses = await _dbContext.DocumentStatuses
            .Where(ds => documentIds.Contains(ds.IdDocument))
            .ToListAsync();

        var recentDocumentStatuses = documentStatuses
            .OrderByDescending(x => x.AppropriationDate)
            .Take(request.NumberOfEntity);

        List<RecentActivityDocumentLookupDto> RecentDocuments = new List<RecentActivityDocumentLookupDto>();
        foreach (var ds in recentDocumentStatuses)
        {
            var document = await _dbContext.Documents.FirstAsync(doc => doc.IdDocument == ds.IdDocument);
            RecentActivityDocumentLookupDto dto = new RecentActivityDocumentLookupDto();
            if (ds.IdUser == request.UserId)
                dto.Description = "Отправленный документ";
            else dto.Description = "Полученный документ";

            var status = await _dbContext.Statuses.FirstAsync(s => s.IdStatus == ds.IdStatus);
            var documentType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == document.IdDocumentType);

            dto.Content = documentType.Name + " <" + document.Title + "> получил статус <" + status.Name + ">";
            dto.Date = ds.AppropriationDate;
            dto.Id = ds.IdDocument;
            RecentDocuments.Add(dto);
        }

        return new RecentActivityDocumentListVm { RecentDocuments = RecentDocuments };
    }
}
