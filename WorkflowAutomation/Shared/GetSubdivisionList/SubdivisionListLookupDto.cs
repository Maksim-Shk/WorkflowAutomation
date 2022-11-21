using AutoMapper;
using System;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Shared
{
    public class SubdivisionListLookupDto : IMapWith<Subdivision>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdSubordination { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subdivision, SubdivisionListLookupDto>()
                .ForMember(subDto => subDto.Id,
                    opt => opt.MapFrom(sub => sub.IdSubdivision))
                    .ForMember(subDto => subDto.Name,
                    opt => opt.MapFrom(sub => sub.Name))
                    .ForMember(subDto => subDto.IdSubordination,
                    opt => opt.MapFrom(sub => sub.IdSubordination));
        }
    }
}