using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

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
                dto.RemoveDate = doc.RemoveDate;

                var docType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == doc.IdDocumentType);
                dto.DocumentType = docType.Name;

                var sender = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdSender);
                dto.SenderInfo.UserInfo = sender.Name + " " + sender.Surname + " " + sender.Patronymic;
                dto.SenderInfo.UserId = sender.IdUser;

                var reciever = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdReceiver);
                dto.RecieverInfo.UserInfo = reciever.Name + " " + reciever.Surname + " " + reciever.Patronymic;
                dto.RecieverInfo.UserId= reciever.IdUser;

                listLookupDtos.Add(dto);
            }

            return new DocumentListVm { Documents = listLookupDtos };

        }
    }
}
