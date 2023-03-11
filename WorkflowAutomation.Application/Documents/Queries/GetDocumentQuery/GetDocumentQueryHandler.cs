using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;


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
            dto.Statuses = new();
            var doc = _dbContext.Documents.FirstOrDefault(doc => doc.IdDocument == request.DocumentId);
            //TODO сделать экспешн
            if (doc != null && (allowedUsers.FirstOrDefault(allowedUser => allowedUser.IdUser == doc.IdSender) != null || 
                (doc.IdSender == request.UserId) || (doc.IdReceiver == request.UserId)))
            {
                dto.Id = doc.IdDocument;
                dto.Title = doc.Title;
                dto.CreateDate = doc.CreateDate;
                dto.RemoveDate = doc.RemoveDate;

                var docType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == doc.IdDocumentType);
                dto.DocumentType = docType.Name;

                var sender = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdSender);
                dto.SenderInfo = sender.Name + " " + sender.Surname + " " + sender.Patronymic;
                dto.SenderId = sender.IdUser;

                var reciever = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdReceiver);
                dto.RecieverInfo = reciever.Name + " " + reciever.Surname + " " + reciever.Patronymic;
                dto.RecieverId = reciever.IdUser;

                var documentStatuses = await _dbContext.DocumentStatuses.Where(ds => ds.IdDocument == request.DocumentId).ToListAsync();
                var statuses = await  _dbContext.Statuses.ToListAsync();
                
                if (documentStatuses != null)
                    dto.Statuses = documentStatuses.Select(ds => new DocStatus
                    {
                        Id = ds.IdStatus,
                        Name = statuses.First(s => s.IdStatus == ds.IdStatus).Name,
                        Date = ds.AppropriationDate
                    }).ToList();

                dto.DocumentFiles = new List<DocFile>();
                var files = _dbContext.DocumentContents.Where(file => file.IdDocument == request.DocumentId).ToList();

                if (files.Count > 0 ) 
                {
                    foreach (var file in files)
                    {
                      
                        var docFile = new DocFile
                        {
                            Name = file.Name,
                            Id = file.IdDocumentContent,
                           // Filestream создается в контроллере
                           // File = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                        };
                        dto.DocumentFiles.Add(docFile);
                    }
                }
            }
            return dto;
        }
    }
}