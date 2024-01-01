using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;

public class CreateNewDocumentDto : IMapWith<CreateNewDocumentCommand>
{
    public List<IFormFile> FilesToUpload { get; set; }

    //[Required(ErrorMessage = "Введите тему документа")]
    //[StringLength(50, ErrorMessage = "Тема документа должно быть не более 256-ти символов")]
    [Required]
    public string Title { get; set; }
    [Required]
    public int DocumentTypeId { get; set; }
    [Required]
    public string ReceiverUserId { get; set; }

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
