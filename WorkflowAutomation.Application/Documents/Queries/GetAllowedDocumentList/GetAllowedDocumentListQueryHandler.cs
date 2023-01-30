using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorkflowAutomation.Domain.Database;

namespace WorkflowAutomation.Application.Documents.Queries.GetAllowedDocumentList
{
    public class GetAllowedDocumentListQueryHandler
   : IRequestHandler<GetAllowedDocumentListQuery, AllowedDocumentListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        List<Subdivision> SubdivisionsList;
        public GetAllowedDocumentListQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        List<Subdivision> GoDownRecursive(int SubdivisionId)
        {
            var res = new List<Subdivision>();
            res.Add(SubdivisionsList.FirstOrDefault(subdivision => subdivision.IdSubdivision == SubdivisionId));
            foreach (var childSubdivision in SubdivisionsList.Where(c => c.IdSubordination == SubdivisionId))
            {
                res.Add(childSubdivision);
                res.AddRange(GoDownRecursive(childSubdivision.IdSubdivision));
            }
            return res;
        }
        public async IAsyncEnumerable<List<Document>> GetAllowedDocumentsAsync(AppUser allowedUser)
        {
            var doc = await _dbContext.Documents.Where(doc => doc.IdSender == allowedUser.IdUser).ToListAsync();
            yield return doc;

        }

        public async Task<AllowedDocumentListVm> Handle(GetAllowedDocumentListQuery request,
            CancellationToken cancellationToken)
        {
            SubdivisionsList = await _dbContext.Subdivisions.ToListAsync();
            //Подразделение пользователя из запроса
            var requstSubdivision = await _dbContext.Subdivisions
                .FirstOrDefaultAsync(subdivision => subdivision.IdSubdivision == _dbContext.UserSubdivisions
                .FirstOrDefault(user => user.IdUser == request.UserId).IdSubdivision);

            //Подразделения, ниже стоящие в иерархии подразделения пользователя
            List<Subdivision> allowedSubdivision = new List<Subdivision>();
            allowedSubdivision = GoDownRecursive(requstSubdivision.IdSubdivision);

            // List<AllowedSubdivisions> allowedSubdivisions2 = new List<AllowedSubdivisions>();

            // allowedSubdivisions2 = _dbContext.GetAllowedSubdivisions(requstSubdivision.IdSubdivision);

            // Все пользователи найденных подразделений - здесь могут иметься повторы
            List<AppUser> bufallowedUsers = new List<AppUser>();
            foreach (var subdivision in allowedSubdivision)
            {
                //все записи в таблице UserSubdivisions (многие ко многим), соответсвующие текущему подразделению
                var userSubdivisions = await _dbContext.UserSubdivisions
                  .Where(userSubdivision => userSubdivision.IdSubdivision == subdivision.IdSubdivision).ToListAsync();

                //добавление пользователей из полученных записей в смежной таблице
                foreach (var userSubdivision in userSubdivisions)
                {
                    bufallowedUsers.Add(await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == userSubdivision.IdUser));
                }

            }
            // Все пользователи найденных подразделений без повторов
            var allowedUsers = bufallowedUsers.GroupBy(x => x.IdUser).Select(x => x.First()).ToList();


            List<Document> allowedDocuments = new List<Document>();

            foreach (var user in allowedUsers)
            {
                allowedDocuments.AddRange(_dbContext.Documents.Where(doc => doc.IdSender == user.IdUser));
            }
            //параллельный foreach
            // foreach (var user in allowedUsers)
            // {
            //     IAsyncEnumerable<List<Document>> allowedDocuments = GetAllowedDocumentsAsync(user);
            //    var list = await allowedDocuments.ToListAsync();
            //     ALLOWED_DOCUMENTS.AddRange(allowedDocuments);
            // }
            // await foreach (var allowedUser in allowedUsers.ToLis) {
            //     await allowedDocuments.()
            // }
            //  var AllowedDocuments = _dbContext.Documents.SelectMany(x=>x.IdSender == allowedUsers.)

            List<GetAllowedDocumentListLookupDto> listLookupDtos = new List<GetAllowedDocumentListLookupDto>();
            foreach (var doc in allowedDocuments)
            {
                GetAllowedDocumentListLookupDto dto = new GetAllowedDocumentListLookupDto();
                dto.Id = doc.IdDocument;
                dto.Title = doc.Title;
                dto.CreateDate = doc.CreateDate;
                dto.RemoveDate = doc.RemoveDate;

                var docType = await _dbContext.DocumentTypes.FirstAsync(t => t.IdDocumentType == doc.IdDocumentType);
                dto.DocumentType = docType.Name;

                //var sender = await allowedUsers.FirstAsync(t => t.IdUser == doc.IdSender);
                var sender = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdSender);
                dto.SenderInfo.UserInfo = sender.Name + " " + sender.Surname + " " + sender.Patronymic;
                dto.SenderInfo.UserId= sender.IdUser;

                var reciever = await _dbContext.Users.FirstAsync(t => t.IdUser == doc.IdReceiver);
                dto.RecieverInfo.UserInfo = reciever.Name + " " + reciever.Surname + " " + reciever.Patronymic;
                dto.RecieverInfo.UserId= reciever.IdUser;

                listLookupDtos.Add(dto);
            }

            return new AllowedDocumentListVm { AllowedDocuments = listLookupDtos };

        }
    }
}
