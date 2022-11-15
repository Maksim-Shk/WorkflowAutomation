using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CreateСontinuingDocument
{
  // public class CreateСontinuingDocumentCommandHandler
  //      : IRequestHandler<CreateСontinuingDocumentCommand, int>
  // {
  //     private readonly IDocumentUserDbContext _dbContext;
  //
  //     public CreateСontinuingDocumentCommandHandler(IDocumentUserDbContext dbContext) =>
  //         _dbContext = dbContext;
  //
  //     public async Task<int> Handle(CreateСontinuingDocumentCommand request,
  //         CancellationToken cancellationToken)
  //     {
  //         var document = new Document
  //         {
  //             IdDocumentType = request.DocumentTypeId,
  //             Title = request.Title,
  //             IdRoute = _dbContext.Routes.Where(x=>x.IdRoute ==
  //                       _dbContext.Documents.Where(document=>document.IdDocument==request.PreviousDocumentUserId)
  //                       .First().IdRoute)
  //                       .First().IdRoute,
  //             IdStatus = _dbContext.Statuses
  //                        .Where(x => x.Name == "В работе")
  //                        .First().IdStatus,
  //             // IdStatus = request.StatusId, //Возможно по-умолчанию "В работе"
  //             CreateDate = DateTime.Now,
  //             UpdateDate = null,
  //             RemoveDate = null
  //         };
  //
  //         var documentUser = new DocumentUser
  //         {
  //             IdSender = request.UserId,
  //             IdDocument = document.IdDocument,
  //             IdReceiver = request.ReceiverUser,
  //             PreviousDocumentUser = request.PreviousDocumentUserId
  //         };
  //
  //         await _dbContext.Documents.AddAsync(document, cancellationToken);
  //         await _dbContext.DocumentUsers.AddAsync(documentUser, cancellationToken);
  //         await _dbContext.SaveChangesAsync(cancellationToken);
  //
  //         return document.IdDocument;
  //     }
  // }
}
