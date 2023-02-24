using System;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using Document = WorkflowAutomation.Domain.Document;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentCommandHandler
        : IRequestHandler<CreateNewDocumentCommand, int>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly ILogger<CreateNewDocumentCommandHandler> _logger;

        public CreateNewDocumentCommandHandler(IDocumentUserDbContext dbContext, ILogger<CreateNewDocumentCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(CreateNewDocumentCommand request,
            CancellationToken cancellationToken)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var document = new Document
                    {
                        IdDocumentType = request.DocumentTypeId,
                        Title = request.Title,
                        CreateDate = DateTime.Now,
                        UpdateDate = null,
                        RemoveDate = null,
                        IdSender = request.UserId,
                        IdReceiver = request.ReceiverUserId
                    };

                    await _dbContext.Documents.AddAsync(document, cancellationToken);
                    await _dbContext.Save(cancellationToken);

                    var documentStatus = new DocumentStatus
                    {
                        IdDocument = document.IdDocument,
                        // IdStatus = request.StatusId,
                        IdStatus = _dbContext.Statuses.Where(x => x.Name == "Зарегистрировано").First().IdStatus,
                        AppropriationDate = document.CreateDate,
                        IdUser = request.UserId,
                    };

                    await _dbContext.DocumentStatuses.AddAsync(documentStatus, cancellationToken);
                    await _dbContext.Save(cancellationToken);

                    //TODO: вынести в appsettings 
                    var maxAllowedFiles = request.MaxAllowedFiles;
                    long maxFileSize = request.MaxFileSize;
                    var filesProcessed = 0;
                    var resourcePath = new Uri($"{request.resourcePath}");
                    List<UploadResult> uploadResults = new();

                    foreach (var file in request.Files)
                    {
                        var uploadResult = new UploadResult();
                        string trustedFileNameForFileStorage;
                        var untrustedFileName = file.FileName;
                        uploadResult.FileName = untrustedFileName;
                        var trustedFileNameForDisplay =
                            WebUtility.HtmlEncode(untrustedFileName);

                        if (filesProcessed < maxAllowedFiles)
                        {
                            if (file.Length == 0)
                            {
                                _logger.LogInformation("{FileName} length is 0 (Err: 1)",
                                    trustedFileNameForDisplay);
                                uploadResult.ErrorCode = 1;
                            }
                            else if (file.Length > maxFileSize)
                            {
                                _logger.LogInformation("{FileName} of {Length} bytes is " +
                                    "larger than the limit of {Limit} bytes (Err: 2)",
                                    trustedFileNameForDisplay, file.Length, maxFileSize);
                                uploadResult.ErrorCode = 2;
                            }
                            else
                            {
                                try
                                {
                                    trustedFileNameForFileStorage = Path.GetRandomFileName();
                                    var path = Path.Combine(request.ContentRootPath,
                                        request.EnvironmentName, "unsafe_uploads",
                                        trustedFileNameForFileStorage);

                                    await using FileStream fs = new(path, FileMode.Create);
                                    await file.CopyToAsync(fs);

                                    _logger.LogInformation("{FileName} saved at {Path}",
                                        trustedFileNameForDisplay, path);
                                    uploadResult.Uploaded = true;
                                    uploadResult.StoredFileName = trustedFileNameForFileStorage;
                                }
                                catch (IOException ex)
                                {
                                    _logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                                        trustedFileNameForDisplay, ex.Message);
                                    uploadResult.ErrorCode = 3;
                                }
                            }

                            filesProcessed++;
                        }
                        else
                        {
                            _logger.LogInformation("{FileName} not uploaded because the " +
                                "request exceeded the allowed {Count} of files (Err: 4)",
                                trustedFileNameForDisplay, maxAllowedFiles);
                            uploadResult.ErrorCode = 4;
                        }

                        uploadResults.Add(uploadResult);
                    }

                    foreach (var uploadResult in uploadResults)
                    {
                        DocumentContent documentContent = new();
                        documentContent.IdDocument = document.IdDocument;
                        documentContent.Name = uploadResult.FileName;
                        documentContent.Path = uploadResult.StoredFileName;
                        _dbContext.DocumentContents.Add(documentContent);

                        await _dbContext.Save(cancellationToken);
                    }
                    //await _dbContext.Save(cancellationToken);

                    transaction.Commit();
                    return document.IdDocument;
                }
                catch 
                {
                    //TODO: вынести в настройки кастомное исключение в Middleware
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
