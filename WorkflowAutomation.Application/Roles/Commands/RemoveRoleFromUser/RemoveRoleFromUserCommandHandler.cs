using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Roles.Commands.RemoveRoleFromUser;

public class RemoveRoleFromUserCommandHandler :
      IRequestHandler<RemoveRoleFromUserCommand>
{
    private readonly IDocumentUserDbContext _dbContext;

    public RemoveRoleFromUserCommandHandler(IDocumentUserDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(RemoveRoleFromUserCommand request,
               CancellationToken cancellationToken)
    {
        //var Role = await _dbContext.AspNetRoles.FirstAsync(x=>x.Id== request.RoleId, cancellationToken);
        var User = await _dbContext.AspNetUsers.Include(x=>x.Roles).FirstAsync(x => x.Id == request.UserId, cancellationToken);
        var role = User.Roles.First(r => r.Id == request.RoleId);
        User.Roles.Remove(role);
        //User.Roles = _dbContext.AspNetUsers.First(x => x.Id == request.UserId).Roles.ToList();
        //Role.Users.Add(User);
        //User.Roles.Add(Role);
        //int countUserRoles = _dbContext.AspNetUsers.First(x => x.Id == request.UserId).Roles.Count();
        //int countRoleUsers = _dbContext.AspNetRoles.First(y=>y.Id==request.RoleId).Users.Count();
        //var UserRole = _dbContext.
        await _dbContext.Save(cancellationToken);
        return Unit.Value;
    }
}