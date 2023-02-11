using System;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewSubdivisionCommandHandler
        : IRequestHandler<CreateNewSubdivisionCommand, int>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly ILogger<CreateNewSubdivisionCommandHandler> _logger;

        public CreateNewSubdivisionCommandHandler(IDocumentUserDbContext dbContext,
            ILogger<CreateNewSubdivisionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(CreateNewSubdivisionCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Subdivision subdivision = new()
                    {
                        Name = request.Name,
                        IdSubordination = request.SubordinationId
                    };
                    //subdivision.CreateDate = request.CreateDate;
                    await _dbContext.Subdivisions.AddAsync(subdivision);
                    await _dbContext.Save(cancellationToken);

                    if (request.SubdivisionUsers != null)
                    {
                        List<UserSubdivision> subdivisions = new();
                        foreach (var subdivisionUser in request.SubdivisionUsers)
                        {
                            UserSubdivision userSubdivision = new()
                            {
                                IdSubdivision = subdivision.IdSubdivision,
                                IdUser = subdivisionUser.UserId,
                                AppointmentDate = subdivisionUser.AppointmentDate,
                                RemovalDate = subdivisionUser.RemovalDate
                            };
                            subdivisions.Add(userSubdivision);
                        }
                        await _dbContext.UserSubdivisions.AddRangeAsync(subdivisions);
                        await _dbContext.Save(cancellationToken);
                    }
                    transaction.Commit();
                    return subdivision.IdSubdivision;
                }
                catch
                {
                    //TODO: вынести в настройки  кастомное исключение в Middleware
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
