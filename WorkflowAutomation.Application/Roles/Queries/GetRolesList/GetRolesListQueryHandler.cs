using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Roles.Queries.GetRolesList;

public class GetRolesListQueryHandler
    : IRequestHandler<GetRolesListQuery, RolesListVm>
{
    private readonly IDocumentUserDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetRolesListQueryHandler(IDocumentUserDbContext dbContext,
        IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<RolesListVm> Handle(GetRolesListQuery request,
        CancellationToken cancellationToken)
    {
        List<RolesListLookupDto> listLookupDtos = new List<RolesListLookupDto>();

        var roles = await _dbContext.AspNetRoles.Include(r=>r.Users).ToListAsync();

        foreach (var role in roles)
        {
            RolesListLookupDto dto = new RolesListLookupDto();
            dto.RoleId = role.Id;
            dto.Name = role.Name;
            dto.Count = role.Users.Count;
            listLookupDtos.Add(dto);
        }

        return new RolesListVm { Roles = listLookupDtos };

    }
}
