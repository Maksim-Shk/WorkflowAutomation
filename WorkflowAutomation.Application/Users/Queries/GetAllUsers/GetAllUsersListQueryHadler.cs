using System.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersListQueryHadler
        : IRequestHandler<GetAllUsersListQuery, AllUsersListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        List<Subdivision> SubdivisionsList;

        public GetAllUsersListQueryHadler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<AllUsersListVm> Handle(GetAllUsersListQuery request,
            CancellationToken cancellationToken)
        {
            AllUsersListVm allUsersListVm = new();
            allUsersListVm.AllUsers = new List<GetAllUsersListDto>();

            foreach (var User in _dbContext.Users.Where(user => user.IdUser != request.UserId).ToList())
            {
                GetAllUsersListDto userListDto = new GetAllUsersListDto();
                userListDto.Id = User.IdUser;
                userListDto.Name = User.Name;
                userListDto.Surname = User.Surname;
                if (User.Patronymic != null)
                    userListDto.Patronymic = User.Patronymic;

                //TODO: FirstAsync не соотвествует связи многие ко многим!
                var UserSubdivision = await _dbContext.UserSubdivisions
                    .FirstOrDefaultAsync(us => us.IdUser == User.IdUser && us.RemovalDate == null, cancellationToken);
                //TODO: FirstAsync не соотвествует связи многие ко многим!
                if (UserSubdivision != null)
                {
                    var Subdivision = await _dbContext.Subdivisions
                    .FirstOrDefaultAsync(p => p.IdSubdivision == UserSubdivision.IdSubdivision, cancellationToken);
                    userListDto.SubdivisionName = Subdivision.Name;
                }
                else userListDto.SubdivisionName = "Подразделение не заполнено";
                var UserPosition = await _dbContext.UserPositions.FirstOrDefaultAsync(up => up.IdUser == User.IdUser && up.RemovalDate == null, cancellationToken);
                if (UserPosition != null)
                {
                    var Positon = await _dbContext.Positions.FirstOrDefaultAsync(p => p.IdPosition == UserPosition.IdPosition, cancellationToken);
                    userListDto.PositonName = Positon.Name;
                }
                else userListDto.PositonName = "Должность не заполнена";
                allUsersListVm.AllUsers.Add(userListDto);

            }
            return allUsersListVm;
        }

    }
}
