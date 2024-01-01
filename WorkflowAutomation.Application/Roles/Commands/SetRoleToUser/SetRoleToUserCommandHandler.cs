using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Roles.Commands.SetRoleToUser;

public class SetRoleToUserCommandCommandHandler :
      IRequestHandler<SetRoleToUserCommand>
{
    private readonly IDocumentUserDbContext _dbContext;

    public SetRoleToUserCommandCommandHandler(IDocumentUserDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(SetRoleToUserCommand request,
               CancellationToken cancellationToken)
    {
        var Role = await _dbContext.AspNetRoles.FirstAsync(x=>x.Id== request.RoleId);
        //var Users = await _dbContext.AspNetUsers.ToListAsync(cancellationToken);
        var User = await _dbContext.AspNetUsers.FirstAsync(x => x.Id == request.UserId);

        //User.Roles = _dbContext.AspNetUsers.First(x => x.Id == request.UserId).Roles.ToList();
        Role.Users.Add(User);
        User.Roles.Add(Role);
        //int countUserRoles = _dbContext.AspNetUsers.First(x => x.Id == request.UserId).Roles.Count();
        //int countRoleUsers = _dbContext.AspNetRoles.First(y=>y.Id==request.RoleId).Users.Count();
        //var UserRole = _dbContext.
        await _dbContext.Save(cancellationToken);
        return Unit.Value;
    }
}