using System.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Users.Queries.GetAllowedUsers;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetUserInfo;

public class GetAllUsersListQueryHadler
    : IRequestHandler<GetAllowedUserListQuery, AllowedUserListVm>
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;
    List<Subdivision> SubdivisionsList;

    public GetAllUsersListQueryHadler(IDocumentUserDbContext dbContext,
        IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    List<Subdivision> GoDownRecursive(int SubdivisionId)
    {
        var res = new List<Subdivision>();
        foreach (var childSubdivision in SubdivisionsList.Where(c => c.IdSubordination == SubdivisionId))
        {
            res.Add(childSubdivision);
            res.AddRange(GoDownRecursive(childSubdivision.IdSubdivision));
        }
        return res;
    }
    public async Task<AllowedUserListVm> Handle(GetAllowedUserListQuery request,
        CancellationToken cancellationToken)
    {
        SubdivisionsList = await _dbContext.Subdivisions.ToListAsync();
        //������������� ������������ �� �������
        var requstSubdivision = await _dbContext.Subdivisions
            .FirstOrDefaultAsync(subdivision => subdivision.IdSubdivision == _dbContext.UserSubdivisions
            .FirstOrDefault(user => user.IdUser == request.UserId).IdSubdivision);

        //�������������, ���� ������� � �������� ������������� ������������
        List<Subdivision> allowedSubdivision = new List<Subdivision>();
        allowedSubdivision = GoDownRecursive(requstSubdivision.IdSubdivision);

        // ��� ������������ ��������� ������������� - ����� ����� ������� �������
        List<AppUser> bufallowedUsers = new List<AppUser>();
        foreach (var subdivision in allowedSubdivision)
        {
            //��� ������ � ������� UserSubdivisions (������ �� ������), �������������� �������� �������������
            var userSubdivisions = await _dbContext.UserSubdivisions
              .Where(userSubdivision => userSubdivision.IdSubdivision == subdivision.IdSubdivision).ToListAsync();

            //���������� ������������� �� ���������� ������� � ������� �������
            foreach (var userSubdivision in userSubdivisions)
            {
                bufallowedUsers.Add(await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == userSubdivision.IdUser));
            }

        }
        // ��� ������������ ��������� ������������� ��� ��������
        var allowedUsers = bufallowedUsers.GroupBy(x => x.IdUser).Select(x => x.First()).ToList();
        allowedUsers.Remove(allowedUsers.FirstOrDefault(user => user.IdUser == request.UserId));

        AllowedUserListVm allowedUserListVm = new();
        allowedUserListVm.AllowedUsers = new List<GetAllUsersListDto>();

        foreach (var User in allowedUsers)
        {
            GetAllUsersListDto userListDto = new GetAllUsersListDto();
            userListDto.Id = User.IdUser;
            userListDto.Name = User.Name;
            userListDto.Surname = User.Surname;
            userListDto.Patronymic = User.Patronymic;

            //TODO: FirstAsync �� ������������ ����� ������ �� ������!
            var UserSubdivision = await _dbContext.UserSubdivisions
                .FirstAsync(us => us.IdUser == User.IdUser && us.RemovalDate == null, cancellationToken);
            //TODO: FirstAsync �� ������������ ����� ������ �� ������!
            var Subdivision = await _dbContext.Subdivisions
                .FirstAsync(p => p.IdSubdivision == UserSubdivision.IdSubdivision, cancellationToken);
            userListDto.SubdivisionName = Subdivision.Name;

            var UserPosition = await _dbContext.UserPositions.FirstAsync(up => up.IdUser == User.IdUser && up.RemovalDate == null, cancellationToken);
            var Positon = await _dbContext.Positions.FirstAsync(p => p.IdPosition == UserPosition.IdPosition, cancellationToken);
            userListDto.PositonName = Positon.Name;

            allowedUserListVm.AllowedUsers.Add(userListDto);

        }
        return allowedUserListVm;
    }
}
