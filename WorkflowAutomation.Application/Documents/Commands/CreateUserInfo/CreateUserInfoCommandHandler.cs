using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoCommandHandler
        : IRequestHandler<CreateUserInfoCommand, string>
    {
        private readonly IDocumentUserDbContext _dbContext;

        public CreateUserInfoCommandHandler(IDocumentUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<string> Handle(CreateUserInfoCommand request,
            CancellationToken cancellationToken)
        {
            if (_dbContext.Users.Contains(_dbContext.Users.FirstOrDefault(user => user.IdUser == request.UserId)))
            {
                return null;
            }
            try
            {
                var user = new AppUser
                {
                    IdUser = request.UserId,
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    RegisterDate = DateTime.Now,
                    LastOnline = DateTime.Now,
                    RemovalDate = null
                };

                var userSubdivision = new UserSubdivision
                {
                    //TODO: Сделать id добавившего пользователя
                    IdUser = request.UserId,
                    IdSubdivision = request.IdSubdivision,
                    AppointmentDate = DateTime.Now,
                    RemovalDate = null
                };

                var userPosition = new UserPosition
                {
                    //TODO: Сделать id добавившего пользователя
                    IdUser = request.UserId,
                    IdPosition = request.IdPositon,
                    AppointmentDate = DateTime.Now,
                    RemovalDate = null
                }; 
                //var aspNetUser = await _dbContext.AspNetUsers.FirstAsync(x=>x.Id == request.UserId, cancellationToken);
                //var role = await _dbContext.AspNetRoles.FirstAsync(x => x.Id == "97da9e73-7077-4bde-84ef-9332d9e93083", cancellationToken);
                //aspNetUser.Roles.Add(role);
                await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.UserSubdivisions.AddAsync(userSubdivision, cancellationToken);
                await _dbContext.UserPositions.AddAsync(userPosition, cancellationToken);
                await _dbContext.Save(cancellationToken);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return request.UserId;
        }
    }
}
