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
    public class GetDocumentListQueryHandler 
        : IRequestHandler<GetDocumentListQuery, DocumentListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDocumentListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DocumentListVm> Handle(GetDocumentListQuery request,
            CancellationToken cancellationToken)
        {
            List<GetDocumentListLookupDto> listLookupDtos = new List<GetDocumentListLookupDto>();

            var AllowedDocuments = await _dbContext.Documents.ToListAsync();

            foreach (var doc in AllowedDocuments)
            {
                GetDocumentListLookupDto dto = new GetDocumentListLookupDto();
                dto.Id = doc.IdDocument;
                dto.Title = doc.Title;
                dto.CreateDate = doc.CreateDate;

                var docType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == doc.IdDocumentType);
                dto.DocumentType = docType.Name;

                var sender = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdSender);
                dto.SenderInfo = sender.Name + " " + sender.Surname + " " + sender.Patronymic;

                var reciever = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdReceiver);
                dto.SenderInfo = reciever.Name + " " + reciever.Surname + " " + reciever.Patronymic;

                listLookupDtos.Add(dto);
            }

            return new DocumentListVm { Documents = listLookupDtos };

        }
    }
}
