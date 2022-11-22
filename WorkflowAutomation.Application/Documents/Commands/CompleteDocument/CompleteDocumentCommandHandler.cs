using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Common.Exceptions;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CompleteDocument
{
  // public class CompleteDocumentCommandHandler
  //     :IRequestHandler<CompleteDocumentCommand>
  // {
  //     private readonly IDocumentUserDbContext _dbContext;
  //     public CompleteDocumentCommandHandler(IDocumentUserDbContext dbContext) =>
  //        _dbContext = dbContext;
  //
  //     public async Task<Unit> Handle(CompleteDocumentCommand request,
  //        CancellationToken cancellationToken)
  //     {
  //
  //
  //         var doc = 
  //             await _dbContext.Documents.FirstOrDefaultAsync(document => 
  //                 document.IdDocument == request.DocumentId,cancellationToken);
  //
  //         if (doc == null)
  //             throw new NotFoundException(nameof(Document), request.DocumentId);
  //
  //         var route = 
  //             await _dbContext.Routes.FirstOrDefaultAsync(route => 
  //             route.IdRoute == doc.IdRoute);
  //
  //         if (route == null)
  //             throw new NotFoundException(nameof(Route), request.DocumentId);
  //
  //         route.CompleteDate = DateTime.Now;
  //
  //       // _dbContext.Routes
  //       //     .Where(route=>route.IdRoute == _dbContext.Documents
  //       //     .Where(document=>document.IdDocument==request.DocumentId)
  //       //     .First().IdDocument)
  //       //     .First().CompleteDate = DateTime.Now;
  //
  //       _dbContext.Documents
  //           .Where(document => document.IdDocument == request.DocumentId)
  //           .First().IdStatus =_dbContext.Statuses
  //           .Where(status => status.Name == "Выполнено")
  //           .First().IdStatus;
  //
  //         await _dbContext.Save(cancellationToken);
  //
  //         return Unit.Value;
  //     }
  // }
}
