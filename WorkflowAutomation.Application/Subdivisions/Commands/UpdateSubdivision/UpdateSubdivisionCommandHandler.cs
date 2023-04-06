using System;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdateSubdivisionCommandHandler
        : IRequestHandler<UpdateSubdivisionCommand, int>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly ILogger<UpdateSubdivisionCommandHandler> _logger;

        public UpdateSubdivisionCommandHandler(IDocumentUserDbContext dbContext,
            ILogger<UpdateSubdivisionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateSubdivisionCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var subdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(s =>s.IdSubdivision == request.SubdivisionId);
                    if (subdivision != null)
                    {
                        if (request.Name != null)
                            subdivision.Name = request.Name;
                        if (request.CreateDate != null)
                            subdivision.CreationDate = request.CreateDate;
                        if (request.SubordinationId != null)
                            subdivision.IdSubordination = request.SubordinationId;

                        if (request.UpdatedSubdivisionUsers != null)
                        {
                            foreach (var user in request.UpdatedSubdivisionUsers)
                            {
                                var SbdUser = await _dbContext.UserSubdivisions.FirstOrDefaultAsync(su => su.IdUser == user.UserId);
                                if (SbdUser != null)
                                {
                                    if (user.NewSubdivisionId != null)
                                    {
                                        DateTime transitDate = DateTime.Now;
                                        SbdUser.RemovalDate = transitDate;
                                        UserSubdivision userSubdivision = new()
                                        {
                                            IdUser = user.UserId,
                                            RemovalDate = null,
                                            IdSubdivision = (int)user.NewSubdivisionId,
                                            AppointmentDate = transitDate
                                        };
                                        _dbContext.UserSubdivisions.Update(SbdUser);
                                        await _dbContext.UserSubdivisions.AddAsync(userSubdivision, cancellationToken);
                                    }
                                    else if (user.RemovalDate != null)
                                    {
                                        SbdUser.RemovalDate = (DateTime)user.RemovalDate;
                                        _dbContext.UserSubdivisions.Update(SbdUser);
                                    }
                                    if (user.AppointmentDate != null)
                                    {
                                        SbdUser.AppointmentDate = (DateTime)user.AppointmentDate;
                                        _dbContext.UserSubdivisions.Update(SbdUser);
                                    }
                                }
                            }
                        }
                        await _dbContext.Save(cancellationToken);
                    }
                    else
                    {
                        transaction.Rollback();
                        //TODO: вынести в кастомное исключение
                        throw new InvalidOperationException();
                    }

                    transaction.Commit();
                    return subdivision.IdSubdivision;
                }
                catch
                {
                    //TODO: вынести в кастомное исключение
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
