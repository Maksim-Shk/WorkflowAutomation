using System;
using System.Data;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using Document = WorkflowAutomation.Domain.Document;

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

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
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

                    await _dbContext.Documents.AddAsync(document, cancellationToken);
                    await _dbContext.Save(cancellationToken);

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

                    await _dbContext.DocumentStatuses.AddAsync(documentStatus, cancellationToken);
                    await _dbContext.Save(cancellationToken);

                    transaction.Commit();
                    return document.IdDocument;
                }
                catch 
                {
                    //TODO кастомное исключение в Middleware
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
