using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Users.Queries.GetPositionUsers;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Positions.Queries.GetPositionList
{
    public class PositionListLookupDto : IMapWith<Position>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdSubordination { get; set; }
        public bool IsRender { get; set; } = false;
        public List<PositionUserDto> positionUsers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Position, PositionListLookupDto>()
                .ForMember(postDto => postDto.Id,
                    opt => opt.MapFrom(pos => pos.IdPosition))
                    .ForMember(postDto => postDto.Name,
                    opt => opt.MapFrom(pos => pos.Name))
                    .ForMember(postDto => postDto.IdSubordination,
                    opt => opt.MapFrom(pos => pos.IdSubordination));
        }
    }
}
