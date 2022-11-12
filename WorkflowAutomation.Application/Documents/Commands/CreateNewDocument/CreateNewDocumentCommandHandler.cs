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
            var route = new Route
            {
                Name = "Маршрут «" + request.Title + "»",
                CreateDate = DateTime.Now,
                CompleteDate = null,
            };

            var document = new Document
            {
                IdDocumentType = request.DocumentTypeId,
                Title = request.Title,
                IdRoute = route.IdRoute,
                IdStatus = _dbContext.Statuses.Where(x => x.Name == "В работе").First().IdStatus,
                // IdStatus = request.StatusId, //Возможно по-умолчанию "В работе"
                CreateDate = DateTime.Now,
                UpdateDate = null,
                RemoveDate = null
            };

            var documentUser = new DocumentUser
            {
                IdSender = request.UserId,
                IdDocument = document.IdDocument,
                IdReceiver = request.ReceiverUser,
                PreviousDocumentUser = null
            };

            await _dbContext.Routes.AddAsync(route, cancellationToken);
            await _dbContext.Documents.AddAsync(document, cancellationToken);
            await _dbContext.DocumentUsers.AddAsync(documentUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return document.IdDocument;
        }
    }
}
