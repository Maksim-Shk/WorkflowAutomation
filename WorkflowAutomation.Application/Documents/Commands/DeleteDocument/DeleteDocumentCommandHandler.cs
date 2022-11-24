using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Common.Exceptions;
using WorkflowAutomation.Domain;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument
{
    public class DeleteNoteCommandHandler
        : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentUserDbContext _dbContext;

        public DeleteNoteCommandHandler(IDocumentUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var document = await _dbContext.Documents
                .FindAsync(new object[] { request.DocumentId }, cancellationToken);

            if (document == null)
            {
                throw new NotFoundException(nameof(Document), request.DocumentId);
            }

            document.RemoveDate = DateTime.Now;

            if (_dbContext.Statuses.FirstOrDefault(x => x.Name == "Удалено") == null) {
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

            await _dbContext.Save(cancellationToken);

            return Unit.Value;
        }
    }
}
