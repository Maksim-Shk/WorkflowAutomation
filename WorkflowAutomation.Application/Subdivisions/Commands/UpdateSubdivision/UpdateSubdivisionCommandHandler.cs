using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

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
                                    SbdUser.RemovalDate = ((DateTime)user.RemovalDate).ToUniversalTime();
                                    _dbContext.UserSubdivisions.Update(SbdUser);
                                }
                                if (user.AppointmentDate != null)
                                {
                                    SbdUser.AppointmentDate =  ((DateTime)user.AppointmentDate).ToUniversalTime();
                                    _dbContext.UserSubdivisions.Update(SbdUser);
                                }
                                await _dbContext.Save(cancellationToken);
                            }

                            var posUser = await _dbContext.UserPositions.FirstOrDefaultAsync(pu => pu.IdUser == user.UserId);
                            if (posUser != null)
                            {
                                if (user.NewPositionId != null)
                                {
                                    DateTime transitDate = DateTime.Now;
                                    posUser.RemovalDate = transitDate;
                                    UserPosition userPosition = new()
                                    {
                                        IdUser = user.UserId,
                                        RemovalDate = null,
                                        IdPosition = (int)user.NewPositionId,
                                        AppointmentDate = transitDate
                                    };
                                    _dbContext.UserPositions.Update(posUser);
                                    await _dbContext.UserPositions.AddAsync(userPosition, cancellationToken);
                                }
                            }
                        }
                    }
                    await _dbContext.Save(cancellationToken);
                }
                else
                {
                    transaction.Rollback();
                    //TODO: ������� � ��������� ����������
                    throw new InvalidOperationException();
                }

                transaction.Commit();
                return subdivision.IdSubdivision;
            }
            catch
            {
                transaction.Rollback();
                //TODO: ������� � ��������� ����������
                throw new InvalidOperationException();
            }
        }
    }
}
