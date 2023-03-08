using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

using System.Data;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Statuses.Commands.ChangeStatus
{
    public class ChangeStatusCommandHandler :
          IRequestHandler<ChangeStatusCommand>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private HubConnection? _hubConnection;
        public ChangeStatusCommandHandler(IDocumentUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(ChangeStatusCommand request,
                   CancellationToken cancellationToken)
        {
            _hubConnection = new HubConnectionBuilder()
         .WithUrl(request.Uri, options =>
         {
             options.AccessTokenProvider = () => Task.FromResult(request.JwtToken);
         }).WithAutomaticReconnect()
           .Build();
            await _hubConnection.StartAsync();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    //последний статус документа
                    //var documentStatus = _dbContext.DocumentStatuses
                    //.Where(x => x.IdStatus == request.StatusId && x.IdDocument == request.DocumentId).ToList()
                    //.Max(x => x.AppropriationDate);
                    var newDocumentStatus = new DocumentStatus
                    {
                        IdDocument = request.DocumentId,
                        IdStatus = request.StatusId,
                        IdUser = request.UserId,
                        AppropriationDate = DateTime.Now,
                    };
                    await _dbContext.DocumentStatuses.AddAsync(newDocumentStatus);
                    await _dbContext.Save(cancellationToken);
                    var document = await _dbContext.Documents.FirstAsync(d => d.IdDocument == request.DocumentId);
                    var status = await _dbContext.Statuses.FirstAsync(status => status.IdStatus == request.StatusId);
                    await _hubConnection.SendAsync("SendNotification", request.UserId, "Успешно!", "Документу <" + document.Title + "> присвоен статус <" + status.Name + ">.");
                    await _hubConnection.DisposeAsync();
                    transaction.Commit();
                    return Unit.Value;
                }
                catch
                {
                    try
                    {
                        await _hubConnection.SendAsync("SendNotification", request.UserId, "Ошибка!", "Статус документа не изменён.");
                        await _hubConnection.DisposeAsync();
                        transaction.Rollback();
                        return Unit.Value;
                    }
                    catch
                    {
                        //TODO: сделать исключение
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}