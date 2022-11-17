using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Shared;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Application.Interfaces;

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
            // foreach (var doc in docs)
            // {
            //     LoadAllDocuments ladItem = new LoadAllDocuments();
            //     ladItem.Title = doc.Title;
            //     ladItem.DocumentType = _dbContext.DocumentTypes.Where(x => x.IdDocumentType == doc.IdDocumentType).First().Name;
            //     ladItem.ReceiverUser = _dbContext.Users.Where(x => x.IdUser == doc.IdReceiver).First().Name +=
            //          ladItem.ReceiverUser = _dbContext.Users.Where(x => x.IdUser == doc.IdReceiver).First().Surname;
            // }
            for (int i = 0; i < 20; i++)
            {
                LoadAllDocuments ladItem = new LoadAllDocuments();
                ladItem.Title = "Документ " + i;
                ladItem.DocumentType = "Служебная записка";
                ladItem.ReceiverUser = "Иван " + i + " город Тверь-" + i;
                lad.Add(ladItem);
            }
            return lad;
        }
    }
}