using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Shared;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoadAllDocumentsController : ControllerBase
    {
        private readonly IDocumentUserDbContext _dbContext;

        private readonly ILogger<LoadAllDocumentsController> _logger;

        public LoadAllDocumentsController(ILogger<LoadAllDocumentsController> logger,
            IDocumentUserDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<LoadAllDocuments> Get()
        {
            var docs = _dbContext.Documents.ToList();
            List<LoadAllDocuments> lad = new List<LoadAllDocuments>();
            foreach (var doc in docs)
            {
                LoadAllDocuments ladItem = new LoadAllDocuments();
                ladItem.Title = doc.Title;
                ladItem.DocumentType = _dbContext.DocumentTypes.Where(x => x.IdDocumentType == doc.IdDocumentType).First().Name;
                var str = "";
                str = _dbContext.Users.First(x => x.IdUser == doc.IdReceiver).Name;
                str += _dbContext.Users.First(x => x.IdUser == doc.IdReceiver).Surname;
                ladItem.ReceiverUser = str;
                lad.Add(ladItem);
            }
            return lad;
        }
    }
}