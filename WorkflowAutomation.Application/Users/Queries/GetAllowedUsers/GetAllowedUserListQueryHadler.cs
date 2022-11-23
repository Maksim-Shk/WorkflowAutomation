using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Users.Queries.GetAllowedUsers;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetUserInfo
{
    public class GetAllowedUserListQueryHadler
        : IRequestHandler<GetAllowedUserListQuery, AllowedUserListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllowedUserListQueryHadler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);


        public async Task<AllowedUserListVm> Handle(GetAllowedUserListQuery request,
            CancellationToken cancellationToken)
        {
            AllowedUserListVm allowedUserListVm = new();
            allowedUserListVm.AllowedUsers = new List<GetAllowedUserListDto>();

            foreach (var User in _dbContext.Users.ToList())
            {
                GetAllowedUserListDto userListDto = new GetAllowedUserListDto();
                userListDto.Id = User.IdUser;

                //createUserInfoDto.Name = _dbContext.Users.First(x => x.IdUser == request.UserId).Name;
                //createUserInfoDto.Surname = _dbContext.Users.First(x => x.IdUser == request.UserId).Surname;
                //createUserInfoDto.Patronymic = _dbContext.Users.First(x => x.IdUser == request.UserId).Patronymic;
                userListDto.Name = User.Name;
                userListDto.Surname = User.Surname;
                userListDto.Patronymic = User.Patronymic;

                // createUserInfoDto.SubdivisionName = _dbContext.Subdivisions
                //     .First(y => y.IdSubordination == _dbContext.UserSubdivisions
                //     .First(x => x.IdUser == request.UserId && x.RemovalDate == null).IdSubdivision).Name;


                var UserSubdivision = await _dbContext.UserSubdivisions
                  .FirstAsync(us => us.IdUser == User.IdUser && us.RemovalDate == null, cancellationToken);
                var Subdivision = await _dbContext.Subdivisions
                    .FirstAsync(p => p.IdSubdivision == UserSubdivision.IdSubdivision, cancellationToken);
                userListDto.SubdivisionName = Subdivision.Name;

                // createUserInfoDto.PositonName = _dbContext.Positions
                //    .First(y => y.IdPosition == _dbContext.UserPositions
                //    .First(x => x.IdUser == request.UserId && x.RemovalDate == null).IdPosition).Name;
                var UserPosition = await _dbContext.UserPositions.FirstAsync(up => up.IdUser == User.IdUser && up.RemovalDate == null, cancellationToken);
                var Positon = await _dbContext.Positions.FirstAsync(p => p.IdPosition == UserPosition.IdPosition, cancellationToken);
                userListDto.PositonName = Positon.Name;

                allowedUserListVm.AllowedUsers.Add(userListDto);

            }
            return allowedUserListVm;
        }

    }
}
