using MediatR;

namespace WorkflowAutomation.Application.Files.Queries.GetFile;

public class GetFileQuery : IRequest<FileDto>
{
    public int FileId { get; set; }
    public string UserId { get; set; }
}
