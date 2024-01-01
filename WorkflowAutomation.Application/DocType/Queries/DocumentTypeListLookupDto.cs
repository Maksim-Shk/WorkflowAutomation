using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;

public class DocumentTypeListLookupDto : IMapWith<DocumentType>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DocumentType, DocumentTypeListLookupDto>()
            .ForMember(subDto => subDto.Id,
                opt => opt.MapFrom(sub => sub.IdDocumentType))
                .ForMember(subDto => subDto.Name,
                opt => opt.MapFrom(sub => sub.Name));
    }
}