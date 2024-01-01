using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Roles.Queries.GetUserRolesList;

public class GetUserRolesListQueryHandler
    : IRequestHandler<GetUserRolesListQuery, UserRolesListVm>
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserRolesListQueryHandler(IDocumentUserDbContext dbContext,
        IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<UserRolesListVm> Handle(GetUserRolesListQuery request,
        CancellationToken cancellationToken)
    {
        List<UserRolesListLookupDto> listLookupDtos = new List<UserRolesListLookupDto>();

        var roles = await _dbContext.AspNetRoles.Include(r=>r.Users).ToListAsync();

        var user = await _dbContext.AspNetUsers.FirstOrDefaultAsync(u=>u.Id == request.UserId);
        if (user != null)
        {
            var userRoles = roles.Where(r => r.Users.Contains(user)).ToList();
            foreach (var role in userRoles)
            {
                UserRolesListLookupDto dto = new UserRolesListLookupDto
                {
                    RoleId = role.Id,
                    Name = role.Name
                };
                listLookupDtos.Add(dto);
            }
        }

        return new UserRolesListVm { Roles = listLookupDtos };
    }
}
