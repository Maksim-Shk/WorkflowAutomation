using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Users.Queries.GetPositionUsers;

public class PositionUsersQueryHandler
     : IRequestHandler<PositionUsersQuery, PositionUsersListVm>
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;

    public PositionUsersQueryHandler(IDocumentUserDbContext dbContext,
     IMapper mapper) =>
     (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<PositionUsersListVm> Handle(PositionUsersQuery request,
       CancellationToken cancellationToken)
    {
        var userList = new List<PositionUserDto>();
        var posUsers = _dbContext.UserPositions.Where(p => p.IdPosition == request.PositionId).ToList();
        foreach (var posUser in posUsers)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == posUser.IdUser);
            var userSub = await _dbContext.UserSubdivisions.FirstOrDefaultAsync(s=>s.IdUser == posUser.IdUser && s.RemovalDate == null);
            var subdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(p=>p.IdSubdivision == userSub.IdSubdivision);
            var fullname = user.Surname + " " + user.Name + " " + user.Patronymic;
            userList.Add(new PositionUserDto
            {
                IdUser = posUser.IdUser,
                FullName = fullname,
                AppointmentDate = posUser.AppointmentDate,
                SubdivisionName = subdivision.Name
            });
        }
        return new PositionUsersListVm {  Users = userList };
    }
}
