using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Files.Queries.GetFile;
using WorkflowAutomation.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FileDownloadController : BaseController
{
    private readonly IWebHostEnvironment env;
    private readonly ILogger<FilesaveController> logger;

    public FileDownloadController(IWebHostEnvironment env,
        ILogger<FilesaveController> logger)
    {
        this.env = env;
        this.logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Stream>> DownLoadFile(int id)
    {
        // string path = @"..\Server\Development\unsafe_uploads\vykhfjhd.a4r";
        var query = new GetFileQuery
        {
            FileId = id,
            UserId = UserId
            //  DirectoryPath = uploadDirectory
        };
        var dto = await Mediator.Send(query);
        return new FileStream(dto.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    }
}