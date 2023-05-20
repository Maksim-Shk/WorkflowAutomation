using MediatR;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Positions.Commands.CreateNewPositionCommand
{
    public class CreatePositionCommandHandler
         : IRequestHandler<CreatePositionCommand, int>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly ILogger<CreatePositionCommandHandler> _logger;

        public CreatePositionCommandHandler(IDocumentUserDbContext dbContext,
            ILogger<CreatePositionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<int> Handle(CreatePositionCommand request,
    CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var newPosition = new Position
                    {
                        Name = request.Name,
                        ShortName = request.ShortName,
                        IdSubordination = request.IdSubordination
                    };

                    await _dbContext.Positions.AddAsync(newPosition,cancellationToken);
                    await _dbContext.Save(cancellationToken);

                    transaction.Commit();
                    return newPosition.IdPosition;
                }
                catch
                {
                    transaction?.Rollback();
                    throw new Exception();
                }
            }
        }
    }
}
