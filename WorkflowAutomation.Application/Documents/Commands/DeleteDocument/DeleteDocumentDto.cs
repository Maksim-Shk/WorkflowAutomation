using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentDto : IMapWith<DeleteDocumentCommand>
    {
        public int DocumentId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteDocumentDto, DeleteDocumentCommand>()
                   .ForMember(DeleteDocumentCommand => DeleteDocumentCommand.DocumentId,
                    opt => opt.MapFrom(DeleteDocumentDto => DeleteDocumentDto.DocumentId));
        }
    }
}