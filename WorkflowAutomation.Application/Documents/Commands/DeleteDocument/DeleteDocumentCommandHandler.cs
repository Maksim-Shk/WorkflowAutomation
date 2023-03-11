using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Common.Exceptions;
using WorkflowAutomation.Domain;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument
{
    public class DeleteNoteCommandHandler
        : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IDocumentRepository _documentRepository;
        public DeleteNoteCommandHandler(IDocumentUserDbContext dbContext, IDocumentRepository documentRepository) =>
            (_dbContext, _documentRepository) = (dbContext, documentRepository);

        public async Task<Unit> Handle(DeleteDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var allowedUsers = await _documentRepository.GetAllowedUsers(request.UserId);
            var document = await _dbContext.Documents
                .FindAsync(new object[] { request.DocumentId }, cancellationToken);

            //проверка доступа
            if (document == null && (allowedUsers.FirstOrDefault(allowedUser => allowedUser.IdUser == document.IdSender) != null || document.IdSender == request.UserId))
            {
                //TODO сделать исключение
                throw new NotFoundException(nameof(Document), request.DocumentId);
            }

            document.RemoveDate = DateTime.Now;

            if (_dbContext.Statuses.FirstOrDefault(x => x.Name == "Удалено") == null)
            {
                var st = new Status { Name = "Удалено" };
                _dbContext.Statuses.Add(st);
                _dbContext.Save(cancellationToken);
            }

            Status status = await _dbContext.Statuses.FirstAsync(y => y.Name == "Удалено");

            var documentStatus = new DocumentStatus
            {
                IdDocument = document.IdDocument,
                IdStatus = status.IdStatus,
                IdUser = request.UserId,
                AppropriationDate = DateTime.Now,
            };
            await _dbContext.DocumentStatuses.AddAsync(documentStatus);
            await _dbContext.Save(cancellationToken);

            return Unit.Value;
        }
    }
}
