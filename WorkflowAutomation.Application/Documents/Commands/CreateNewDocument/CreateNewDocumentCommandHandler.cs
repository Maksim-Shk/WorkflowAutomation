using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentCommandHandler
        : IRequestHandler<CreateNewDocumentCommand, int>
    {
        private readonly IDocumentUserDbContext _dbContext;

        public CreateNewDocumentCommandHandler(IDocumentUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<int> Handle(CreateNewDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var document = new Document
            {
                IdDocumentType = request.DocumentTypeId,
                Title = request.Title,
                CreateDate = DateTime.Now,
                UpdateDate = null,
                RemoveDate = null,
                IdSender = request.UserId,
                IdReceiver = request.ReceiverUserId
            };

            var documentStatus = new DocumentStatus
            {
                IdDocument = document.IdDocument,
                IdStatus = _dbContext.Statuses.Where(x => x.Name == "Зарегистрировано").First().IdStatus,
                // IdStatus = request.StatusId, //Возможно по-умолчанию "В работе"
                AppropriationDate = document.CreateDate,
                //??????
                IdUser = request.UserId,

            };

            // TODO : Добавить DocumentContent


            await _dbContext.Documents.AddAsync(document, cancellationToken);
            await _dbContext.DocumentStatuses.AddAsync(documentStatus, cancellationToken);
            await _dbContext.Save(cancellationToken);

            return document.IdDocument;
        }
    }
}
