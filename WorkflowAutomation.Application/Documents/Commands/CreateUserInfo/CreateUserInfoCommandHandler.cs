using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

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
