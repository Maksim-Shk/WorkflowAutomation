using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Server.Models
{
    public class CreateNewDocumentDto : IMapWith<CreateNewDocumentCommand>
    {
        [Required]
        public string Title { get; set; }
        public int DocumentTypeId { get; set; }
        public Guid ReceiverUserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNewDocumentDto, CreateNewDocumentCommand>()
                .ForMember(docCommand => docCommand.Title,
                    opt => opt.MapFrom(docDto => docDto.Title))
                 .ForMember(docCommand => docCommand.DocumentTypeId,
                    opt => opt.MapFrom(docDto => docDto.DocumentTypeId))
                  .ForMember(docCommand => docCommand.ReceiverUserId,
                    opt => opt.MapFrom(docDto => docDto.ReceiverUserId));
        }
    } 
}
