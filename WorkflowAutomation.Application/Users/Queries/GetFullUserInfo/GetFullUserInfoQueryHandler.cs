using System.Data;
using System.Globalization;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Documents;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo;

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
        public int Id { get; set; }
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
        dto.UserSubdivisionHistory = new List<UserSubdivisionHistory>();

        // var allowedUsers = _documentRepository.GetAllowedUsers(request.UserId);

        var User = _dbContext.Users.FirstOrDefault(u => u.IdUser == request.RequestedUserId);

        dto.Name = User.Name;
        dto.Surname = User.Surname;
        dto.Patronymic = User.Patronymic;

        //TODO: FirstAsync не соотвествует св€зи многие ко многим!
        var UserSubdivision = await _dbContext.UserSubdivisions
            .FirstAsync(us => us.IdUser == User.IdUser && us.RemovalDate == null, cancellationToken);
        //TODO: FirstAsync не соотвествует св€зи многие ко многим!
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

        //TODO присвоить роль пользовател€
        dto.RoleName = "Ќазвание роли пользовател€";

        var userSubdivisions = await _dbContext.UserSubdivisions.Where(us => us.IdUser == request.RequestedUserId).ToListAsync();
        var userPosition = await _dbContext.UserPositions.Where(up => up.IdUser == request.RequestedUserId).ToListAsync();

        List<PositionAndSubdivision> PositionAndSubdivisionList = new List<PositionAndSubdivision>();

        foreach (var us in userSubdivisions)
        {
            var subdivision = await _dbContext.Subdivisions.FirstOrDefaultAsync(s => s.IdSubdivision == us.IdSubdivision);
            dto.UserSubdivisionHistory = new List<UserSubdivisionHistory>();
            PositionAndSubdivision pas = new PositionAndSubdivision
            {
                Id = us.IdSubdivision,
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
                Id = up.IdPosition,
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
        foreach (var pas in PositionAndSubdivisionList)
        {

            UserSubdivisionHistory positionAndSubdivision = new UserSubdivisionHistory();
            if (pas.Type == "Subdivision")
            {
                positionAndSubdivision.Type = HistoryType.Subdivision;
                positionAndSubdivision.SubdivisionId = pas.Id;
                positionAndSubdivision.SubdivisionName = pas.Name;
                positionAndSubdivision.EmploymentDate = pas.EmploymentDate;
                positionAndSubdivision.DismissalDate = pas.DismissalDate;
                var userpositions = _dbContext.UserPositions.FirstOrDefault(userPos => userPos.IdUser == request.RequestedUserId
                                                                   && userPos.AppointmentDate.Date <= pas.EmploymentDate.Date
                                                                   && (userPos.RemovalDate >= pas.EmploymentDate || userPos.RemovalDate == null));
                // остыль дл€ некорректно добавленых данных на тесте
                if (userpositions == null)
                {
                    var bufUserPos = _dbContext.UserPositions.FirstOrDefault(userPos => userPos.IdUser == request.RequestedUserId);
                    positionAndSubdivision.PositionName = _dbContext.Positions.FirstOrDefault(pos => pos.IdPosition == bufUserPos.IdPosition).Name;
                    positionAndSubdivision.PositonId = bufUserPos.IdPosition;
                }
                //нормальный сценарий работы
                else
                {
                    positionAndSubdivision.PositionName = _dbContext.Positions.FirstOrDefault(pos => pos.IdPosition == userpositions.IdPosition).Name;
                    positionAndSubdivision.PositonId = userpositions.IdPosition;
                }
            }
            else
            {
                positionAndSubdivision.Type = HistoryType.Position;
                positionAndSubdivision.PositonId = pas.Id;
                positionAndSubdivision.PositionName = pas.Name;
                positionAndSubdivision.EmploymentDate = pas.EmploymentDate;
                positionAndSubdivision.DismissalDate = pas.DismissalDate;
                var usersubdivision = _dbContext.UserSubdivisions.FirstOrDefault(userSub => userSub.IdUser == request.RequestedUserId
                                                                   && userSub.AppointmentDate.Date <= pas.EmploymentDate.Date
                                                                   && (userSub.RemovalDate >= pas.EmploymentDate || userSub.RemovalDate == null));

                // остыль дл€ некорректно добавленых данных на тесте
                if (usersubdivision == null)
                {
                    var bufUserSub = _dbContext.UserSubdivisions.FirstOrDefault(userSub => userSub.IdUser == request.RequestedUserId);
                    positionAndSubdivision.SubdivisionName = _dbContext.Subdivisions.FirstOrDefault(sub => sub.IdSubdivision == bufUserSub.IdSubdivision).Name;
                    positionAndSubdivision.SubdivisionId = bufUserSub.IdSubdivision;
                }
                //нормальный сценарий работы
                else
                {
                    positionAndSubdivision.SubdivisionName = _dbContext.Subdivisions.FirstOrDefault(sub => sub.IdSubdivision == usersubdivision.IdSubdivision).Name;
                    positionAndSubdivision.SubdivisionId = usersubdivision.IdSubdivision;
                }
            }


            CultureInfo russian = new CultureInfo("ru-RU");
            if (pas.DismissalDate != null)
            {
                var time = (((DateTime)pas.DismissalDate).Subtract(pas.EmploymentDate));
                //.ToString("yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                positionAndSubdivision.WorkingTime = ConvertDateForUser.ToReadableString(time);
            }
            else
            {
                var time = (DateTime.Now.Subtract(pas.EmploymentDate));
                positionAndSubdivision.WorkingTime = ConvertDateForUser.ToReadableString(time);
            }
            // UserSubdivisionHistory positionAndSubdivision = new UserSubdivisionHistory();
            // if (pas == "Subdivision")
            // {
            //
            // }
            // dto.UserSubdivisionHistory.Add();
            int a = 2 + 2;
            dto.UserSubdivisionHistory.Add(positionAndSubdivision);
            int ass = 2 + 2;
        }
        dto.UserSubdivisionHistory = dto.UserSubdivisionHistory.OrderBy(x => x.EmploymentDate).ToList();
        //Distinct с трем€ пол€ми SubdivisionName и PositionName
        //  if (dto.UserSubdivisionHistory != null)
        //      dto.UserSubdivisionHistory = dto.UserSubdivisionHistory
        //         .GroupBy(m => new { m.SubdivisionName, m.PositionName, })
        //         .Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
        //         .ToList();
        return dto;
    }
}

