using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents;

public class DocumentRepository : IDocumentRepository
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;
    List<Subdivision> SubdivisionsList;

    public DocumentRepository(IDocumentUserDbContext dbContext,
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
    public async Task<List<AppUser>> GetAllowedUsers(string UserId)
    {
        SubdivisionsList = await _dbContext.Subdivisions.ToListAsync();
        //Подразделение пользователя из запроса
        var requstSubdivision = await _dbContext.Subdivisions
            .FirstOrDefaultAsync(subdivision => subdivision.IdSubdivision == _dbContext.UserSubdivisions
            .FirstOrDefault(user => user.IdUser == UserId).IdSubdivision);

        //Подразделения, ниже стоящие в иерархии подразделения пользователя
        List<Subdivision> allowedSubdivision = new List<Subdivision>();
        allowedSubdivision = GoDownRecursive(requstSubdivision.IdSubdivision);
        allowedSubdivision.Add(_dbContext.Subdivisions.FirstOrDefault(x => x.IdSubdivision == requstSubdivision.IdSubdivision));
        allowedSubdivision.Distinct();
        // Все пользователи найденных подразделений - здесь могут иметься повторы
        List<AppUser> bufallowedUsers = new List<AppUser>();
        foreach (var subdivision in allowedSubdivision)
        {
            //все записи в таблице UserSubdivisions (многие ко многим), соответсвующие текущему подразделению
            var userSubdivisions = await _dbContext.UserSubdivisions
              .Where(userSubdivision => userSubdivision.IdSubdivision == subdivision.IdSubdivision).ToListAsync();

            //добавление пользователей из полученных записей в смежной таблице
            foreach (var userSubdivision in userSubdivisions)
            {
                bufallowedUsers.Add(await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == userSubdivision.IdUser));
            }
        }
        // Все пользователи найденных подразделений без повторов
        var allowedUsers = bufallowedUsers.GroupBy(x => x.IdUser).Select(x => x.First()).ToList();
        // Удаление запрашиваемого пользователя из выходной коллекции
        allowedUsers.Remove(allowedUsers.FirstOrDefault(user => user.IdUser == UserId));

        return allowedUsers;
    }
}
