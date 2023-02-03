using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentDto : IMapWith<CreateNewDocumentCommand>
    {
        //[Required(ErrorMessage = "Введите тему документа")]
        //[StringLength(50, ErrorMessage = "Тема документа должно быть не более 256-ти символов")]     
        [Required]
        [EmailAddress]
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
}
