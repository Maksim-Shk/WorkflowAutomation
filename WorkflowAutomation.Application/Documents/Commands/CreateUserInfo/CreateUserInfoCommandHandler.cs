using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class UserInfoCommandHandler
        : IRequestHandler<UserInfoCommand, string>
    {
        private readonly IDocumentUserDbContext _dbContext;

        public UserInfoCommandHandler(IDocumentUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<int> Handle(UserInfoCommand request,
            CancellationToken cancellationToken)
        {
            var User = new AppUser
            {
                IdUser = request.UserId,
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                RegisterDate = DateTime.Now,
                RemovalDate = null,
                LastOnline = DateTime.Now
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

            await _dbContext.Users.AddAsync(User, cancellationToken);
            await _dbContext.UserSubdivisions.AddAsync(userSubdivision, cancellationToken);
            await _dbContext.UserPositions.AddAsync(userPosition, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return document.IdDocument;
        }
    }
}
