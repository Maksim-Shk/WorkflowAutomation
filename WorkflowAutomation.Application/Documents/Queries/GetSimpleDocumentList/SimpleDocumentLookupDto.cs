using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList;

public class SimpleDocumentLookupDto : IMapWith<Document>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreateDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Document, SimpleDocumentLookupDto>()
            .ForMember(docDto => docDto.Id,
                opt => opt.MapFrom(doc => doc.IdDocument))
                .ForMember(docDto => docDto.Title,
                opt => opt.MapFrom(doc => doc.Title))
                .ForMember(docDto => docDto.CreateDate,
                opt => opt.MapFrom(doc => doc.CreateDate));
    }
}