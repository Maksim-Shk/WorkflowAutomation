using System.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Documents;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Users.Queries.GetAllowedUsers;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo
{
    public class GetFullUserInfoQueryHandler
        : IRequestHandler<GetFullUserInfoQuery, FullUserInfoDto>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        List<Subdivision> SubdivisionsList;

        public GetFullUserInfoQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper, IDocumentRepository documentRepository) =>
            (_dbContext, _mapper, _documentRepository) = (dbContext, mapper, documentRepository);

        private class PositionAndSubdivision
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public DateTime? DismissalDate { get; set; }
            public DateTime EmploymentDate { get; set; }
            public string WorkTime { get; set; }
        }

        public async Task<FullUserInfoDto> Handle(GetFullUserInfoQuery request,
            CancellationToken cancellationToken)
        {

            FullUserInfoDto dto = new();
            dto.UserSubdivisionHistory = new();

            // var allowedUsers = _documentRepository.GetAllowedUsers(request.UserId);

            var User = _dbContext.Users.FirstOrDefault(u => u.IdUser == request.RequestedUserId);

            dto.Name = User.Name;
            dto.Surname = User.Surname;
            dto.Patronymic = User.Patronymic;

            //TODO: FirstAsync не соотвествует связи многие ко многим!
            var UserSubdivision = await _dbContext.UserSubdivisions
                .FirstAsync(us => us.IdUser == User.IdUser && us.RemovalDate == null, cancellationToken);
            //TODO: FirstAsync не соотвествует связи многие ко многим!
            var Subdivision = await _dbContext.Subdivisions
                .FirstAsync(p => p.IdSubdivision == UserSubdivision.IdSubdivision, cancellationToken);
            dto.SubdivisionName = Subdivision.Name;
            dto.SubdivisionId = Subdivision.IdSubdivision;

            var UserPosition = await _dbContext.UserPositions.FirstAsync(up => up.IdUser == User.IdUser && up.RemovalDate == null, cancellationToken);
            var Positon = await _dbContext.Positions.FirstAsync(p => p.IdPosition == UserPosition.IdPosition, cancellationToken);
            dto.PositonName = Positon.Name;
            dto.PositonId = Positon.IdPosition;

            dto.LastOnlineDate = User.LastOnline;
            dto.RegistrationDate = User.RegisterDate;

            dto.UserSubdivisionHistory = null;

            var userSubdivisions = await _dbContext.UserSubdivisions.Where(us => us.IdUser == request.RequestedUserId).ToListAsync();
            var userPosition = await _dbContext.UserPositions.Where(up => up.IdUser == request.RequestedUserId).ToListAsync();

            List<PositionAndSubdivision> PositionAndSubdivisionList = new List<PositionAndSubdivision>();

            foreach (var us in userSubdivisions)
            {
                var subdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(s => s.IdSubdivision == us.IdSubdivision);
                dto.UserSubdivisionHistory = new List<UserSubdivisionHistory>();
                PositionAndSubdivision pas = new PositionAndSubdivision
                {
                    Type = "Subdivision",
                    Name = subdivision.Name,
                    EmploymentDate = us.AppointmentDate,
                    DismissalDate = us.RemovalDate
                };
                // if (pas.DismissalDate == null)
                // {
                //     var time = DateTime.Now - pas;
                // }
                //     pas.WorkTime = 
                PositionAndSubdivisionList.Add(pas);
            }
            foreach (var up in userPosition)
            {
                var position = await _dbContext.Positions.FirstOrDefaultAsync(s => s.IdPosition == up.IdPosition);
                PositionAndSubdivision pas = new PositionAndSubdivision
                {
                    Type = "Position",
                    Name = position.Name,
                    EmploymentDate = up.AppointmentDate,
                    DismissalDate = up.RemovalDate
                };
                PositionAndSubdivisionList.Add(pas);
            }
            // foreach (var us in userSubdivisions)
            // {
            //     var subdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(s => s.IdSubdivision == us.IdSubdivision);
            //     PositionAndSubdivision positionAndSubdivision = new PositionAndSubdivision();
            //     var position = await _dbContext.Positions.FirstOrDefault(p=>p.IdPosition = )
            //     UserSubdivisionHistory usHistory = new();
            //     usHistory.SubdivisionId = us.IdSubdivision;
            //     
            // }
            PositionAndSubdivisionList.OrderBy(pas => pas.EmploymentDate).ToList();
            foreach (var pas in PositionAndSubdivisionList) { 

                UserSubdivisionHistory positionAndSubdivision = new UserSubdivisionHistory();
                positionAndSubdivision.SubdivisionId = 1;
                positionAndSubdivision.SubdivisionName = "Подразделение";
                positionAndSubdivision.DismissalDate = DateTime.Now;
                positionAndSubdivision.EmploymentDate = DateTime.Now;
                positionAndSubdivision.WorkingTime = (pas.EmploymentDate.Subtract(DateTime.Now)).ToString();
                positionAndSubdivision.PositionName = "Должность";
                positionAndSubdivision.PositonId = 1;
                // UserSubdivisionHistory positionAndSubdivision = new UserSubdivisionHistory();
                // if (pas == "Subdivision")
                // {
                //
                // }
                // dto.UserSubdivisionHistory.Add();

                dto.UserSubdivisionHistory.Add(positionAndSubdivision);
            }
            return dto;
        }
    }
}

