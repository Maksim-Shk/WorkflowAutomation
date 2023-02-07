using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetOneDocument
{
    public class GetDocumentQueryHandler
        : IRequestHandler<GetDocumentQuery, DocumentDto>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        public GetDocumentQueryHandler(IDocumentUserDbContext dbContext,
          IMapper mapper, IDocumentRepository documentRepository) =>
          (_dbContext, _mapper, _documentRepository) = (dbContext, mapper, documentRepository);


        public async Task<DocumentDto> Handle(GetDocumentQuery request,
           CancellationToken cancellationToken)
        {
            var allowedUsers = await _documentRepository.GetAllowedUsers(request.UserId);
            DocumentDto dto = new();
            dto.Files = new();
            var doc = _dbContext.Documents.FirstOrDefault(doc => doc.IdDocument == request.DocumentId);
            //TODO сделать экспешн
            if (doc != null && (allowedUsers.FirstOrDefault(allowedUser => allowedUser.IdUser == doc.IdSender) != null || doc.IdSender == request.UserId))
            {
                dto.Id = doc.IdDocument;
                dto.Title = doc.Title;
                dto.CreateDate = doc.CreateDate;
                dto.RemoveDate = doc.RemoveDate;

                var docType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == doc.IdDocumentType);
                dto.DocumentType = docType.Name;

                var sender = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdSender);
               // dto.SenderInfo = new();
                dto.SenderInfo = sender.Name + " " + sender.Surname + " " + sender.Patronymic;
                dto.SenderId = sender.IdUser;

                var reciever = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdReceiver);
               // dto.RecieverInfo = new();
                dto.RecieverInfo = reciever.Name + " " + reciever.Surname + " " + reciever.Patronymic;
                dto.RecieverId = reciever.IdUser;

               // dto.Files = new List<DocFile>();
                //var files = _dbContext.DocumentContents.Where(file=>file.IdDocument == request.DocumentId).ToList();
                //foreach (var file in files)
                //    dto.Files.Add(new DocFile { Id = file.IdDocumentContent, Title = file.Name });
            }
            return dto;
        }
    }
}