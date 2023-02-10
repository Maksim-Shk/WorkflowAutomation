using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Shared;
using System.Text;
using MediatR;
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

    //[HttpGet]
    //public async Task<ActionResult<Stream>> DownLoadFile()
    //{
    //    string path = @"..\Server\Development\unsafe_uploads\vykhfjhd.a4r";
    //
    //    return new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    //}
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