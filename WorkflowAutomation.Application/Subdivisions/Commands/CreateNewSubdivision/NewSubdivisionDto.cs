using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;

namespace WorkflowAutomation.Application.Subdivisions.Commands.CreateNewSubdivision
{
    public class NewSubdivisionDto : IMapWith<CreateNewSubdivisionCommand>
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public List<SubUsers>? SubdivisionUsers { get; set; }
        public int? SubordinationId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewSubdivisionDto, CreateNewSubdivisionCommand>()
                   .ForMember(subCommand => subCommand.Name,
                    opt => opt.MapFrom(subDto => subDto.Name))
                   .ForMember(subCommand => subCommand.CreateDate,
                    opt => opt.MapFrom(subDto => subDto.CreateDate))
                   .ForMember(subCommand => subCommand.SubdivisionUsers,
                    opt => opt.MapFrom(subDto => subDto.SubdivisionUsers))
                   .ForMember(subCommand => subCommand.SubordinationId,
                    opt => opt.MapFrom(subDto => subDto.SubordinationId));
        }
    }
}
