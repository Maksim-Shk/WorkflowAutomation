using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Commands.GetUserInfo
{
    public class GetUserInfoCommandHandler
        : IRequestHandler<GetUserInfoCommand, GetUserInfoDto>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserInfoCommandHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GetUserInfoDto> Handle(GetUserInfoCommand request,
            CancellationToken cancellationToken)
        {
            GetUserInfoDto createUserInfoDto = new GetUserInfoDto();
            createUserInfoDto.Id = request.UserId;

            //createUserInfoDto.Name = _dbContext.Users.First(x => x.IdUser == request.UserId).Name;
            //createUserInfoDto.Surname = _dbContext.Users.First(x => x.IdUser == request.UserId).Surname;
            //createUserInfoDto.Patronymic = _dbContext.Users.First(x => x.IdUser == request.UserId).Patronymic;
            var User = await _dbContext.Users.FirstAsync(x => x.IdUser == request.UserId, cancellationToken);
            createUserInfoDto.Name = User.Name;
            createUserInfoDto.Surname = User.Surname;
            createUserInfoDto.Patronymic = User.Patronymic;

           //createUserInfoDto.SubdivisionName = _dbContext.Subdivisions
           //    .First(y => y.IdSubordination == _dbContext.UserSubdivisions
           //    .First(x => x.IdUser == request.UserId && x.RemovalDate == null).IdSubdivision).Name;
            var UserSubdivision = await _dbContext.UserSubdivisions.FirstAsync(us => us.IdUser == request.UserId && us.RemovalDate == null, cancellationToken);
            var Subdivision = await _dbContext.Subdivisions.FirstAsync(p => p.IdSubdivision == UserSubdivision.IdSubdivision, cancellationToken);
            createUserInfoDto.SubdivisionName = Subdivision.Name;

            // createUserInfoDto.PositonName = _dbContext.Positions
            //    .First(y => y.IdPosition == _dbContext.UserPositions
            //    .First(x => x.IdUser == request.UserId && x.RemovalDate == null).IdPosition).Name;
            var UserPosition = await _dbContext.UserPositions.FirstAsync(up => up.IdUser == request.UserId && up.RemovalDate == null, cancellationToken);
            var Positon = await _dbContext.Positions.FirstAsync(p => p.IdPosition == UserPosition.IdPosition, cancellationToken);
            createUserInfoDto.PositonName = Positon.Name;

            return createUserInfoDto;
        }

    }
}
