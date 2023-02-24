using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using WorkflowAutomation.Application.Documents;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Files.Queries.GetFile
{
    public class GetFileQueryHandler
        : IRequestHandler<GetFileQuery, FileDto>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        public GetFileQueryHandler(IDocumentUserDbContext dbContext,
          IMapper mapper, IDocumentRepository documentRepository) =>
          (_dbContext, _mapper, _documentRepository) = (dbContext, mapper, documentRepository);


        public async Task<FileDto> Handle(GetFileQuery request,
           CancellationToken cancellationToken)
        {
            var allowedUsers = await _documentRepository.GetAllowedUsers(request.UserId);
            allowedUsers.Add(_dbContext.Users.FirstOrDefault(u=>u.IdUser==request.UserId));
            FileDto dto = new();
            var file = await _dbContext.DocumentContents.FirstOrDefaultAsync(file => file.IdDocumentContent == request.FileId);
            var doc = await _dbContext.Documents.FirstOrDefaultAsync(doc => doc.IdDocument == file.IdDocument);

            string BasePath = @"..\Server\Development\unsafe_uploads\";

            //TODO сделать экспешн
            if (file != null && (allowedUsers.FirstOrDefault(allowedUser => allowedUser.IdUser == doc.IdSender) != null 
                || doc.IdSender == request.UserId || doc.IdReceiver == request.UserId))
            {
                dto.Path = BasePath + file.Path;
             //   dto.FileStream = new FileStream(dto.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            return dto;
        }
    }
}