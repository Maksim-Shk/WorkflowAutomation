using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo;

public class GetRolesListQueryHandler
   : IRequestHandler<GetSubdivisionInfoQuery, SubdivisionInfoDto>
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetRolesListQueryHandler(IDocumentUserDbContext dbContext,
        IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<SubdivisionInfoDto> Handle(GetSubdivisionInfoQuery request,
        CancellationToken cancellationToken)
    {
        SubdivisionInfoDto dto = new SubdivisionInfoDto();

        var subdivision = _dbContext.Subdivisions.FirstOrDefault(x => x.IdSubdivision == request.SubdivisionId);
        dto.Name = subdivision.Name;
        if (subdivision.CreationDate != null)
        {
            dto.CreateDate = (DateTime)subdivision.CreationDate;
        }
        else dto.CreateDate = DateTime.MinValue;

        //ID и название подразделения, которому подчиняется это подразделение
        dto.SubordinationId = subdivision.IdSubordination;
        var greaterSubdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(s => s.IdSubdivision == subdivision.IdSubordination);
        if (greaterSubdivision != null)
            dto.SubordinationName = greaterSubdivision.Name;
        else dto.SubordinationName = null;

        dto.Users = new List<SubdivisionUser>();
        var SubdivisionUsers = await _dbContext.UserSubdivisions.Where(x => x.IdSubdivision == request.SubdivisionId).ToListAsync();
        var users = SubdivisionUsers.Select(su => su.IdUser).Intersect(_dbContext.Users.Select(u => u.IdUser)).ToList();

        var userAndUsersubdivision = _dbContext.Users    // your starting point - table in the "from" statement
           .Join(_dbContext.UserSubdivisions, // the source table of the inner join
              u => u.IdUser,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
              us => us.IdUser,   // Select the foreign key (the second part of the "on" clause)
              (u, us) => new { User = u, UserSubdivision = us }) // selection
           .Where(userAndUsersubdivision => userAndUsersubdivision.UserSubdivision.IdSubdivision == request.SubdivisionId).ToList();//  where statement

        foreach (var item in userAndUsersubdivision)
        {
            SubdivisionUser subdivisionUser = new SubdivisionUser();
            subdivisionUser.Name = item.User.Surname + " " + item.User.Name + " " + item.User.Patronymic;
            subdivisionUser.Id = item.User.IdUser;
            subdivisionUser.AppointmentDate = SubdivisionUsers.First(u=>u.IdUser==item.User.IdUser).AppointmentDate;
            subdivisionUser.RemovalDate = SubdivisionUsers.First(u => u.IdUser == item.User.IdUser).RemovalDate;
            var userPosition = _dbContext.UserPositions.FirstOrDefault(up => up.IdUser == subdivisionUser.Id);
            var position = await _dbContext.Positions.FirstAsync(p => p.IdPosition == userPosition.IdPosition);
          // var userAndUserPosition = _dbContext.Users
          //    .Join(_dbContext.UserPositions,
          //       u => u.IdUser,
          //       up => up.IdUser,
          //       (u, up) => new { User = u, UserPosition = up })
          //    .Where(userAndUserPosition => userAndUserPosition.UserPosition.IdUser == item.User.IdUser && userAndUserPosition.UserPosition.RemovalDate == null);
          //
          // // subdivisionUser.Position = _dbContext.Positions.FirstOrDefault(p => p.IdPosition == userAndUserPosition.FirstOrDefault(uup => uup.UserPosition.RemovalDate == null).UserPosition.IdPosition).Name;
          //
          // var userPosition = userAndUserPosition.First();
          // var position = _dbContext.Positions.FirstOrDefault(p=>p.IdPosition == userPosition.UserPosition.IdPosition).Name;
            subdivisionUser.Position = position.Name;
            subdivisionUser.PositionId = position.IdPosition;

            dto.Users.Add(subdivisionUser);
        }
        //  foreach (var user in users)
        //  {
        //      SubdivisionUser subdivisionUser = new();
        //      subdivisionUser.Id = user;
        //      subdivisionUser.Name = await _dbContext.Subdivisions(s=>s.IdUser == user);
        //  }
        return dto;

    }
}