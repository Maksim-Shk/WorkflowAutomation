using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentStatisticBySubdivision
{
    public class GetDocumentStatisticBySubdivisionQueryHandler :
        IRequestHandler<GetDocumentStatisticBySubdivisionQuery, DocumentStatisticBySubdivisionListVm>
    {

        private readonly IDocumentUserDbContext _dbContext;

        public GetDocumentStatisticBySubdivisionQueryHandler(IDocumentUserDbContext dbContext) =>
            (_dbContext) = (dbContext);

        public async Task<DocumentStatisticBySubdivisionListVm> Handle(GetDocumentStatisticBySubdivisionQuery request,
            CancellationToken cancellationToken)
        {
            var StatisticSets = new List<StatisticSubdivisionSet>();

            var documents = _dbContext.Documents
                .Include(s => s.IdSenderNavigation)
                .ThenInclude(us => us.UserSubdivisions)
                .ThenInclude(s => s.IdSubdivisionNavigation)
                .Include(s => s.IdSenderNavigation)
                .ThenInclude(up => up.UserPositions)
                .ThenInclude(p => p.IdPositionNavigation)
                .Include(ds => ds.DocumentStatuses)
                .ThenInclude(s => s.IdStatusNavigation)
                .ToList();

            var Subdivisions = new List<Subdivision>();

            foreach (var document in documents.Where(d => d.IdSenderNavigation.UserSubdivisions.Count != 0).ToList())
            {
                foreach (var userSubdivision in document.IdSenderNavigation.UserSubdivisions)
                {
                    Subdivisions.Add(userSubdivision.IdSubdivisionNavigation);
                }
            }
            Subdivisions = Subdivisions.Distinct().ToList();

            foreach (var subdivision in Subdivisions)
            {
                var statisticSubdivisionSet = new StatisticSubdivisionSet();
                statisticSubdivisionSet.GroupName = subdivision.Name;
                statisticSubdivisionSet.StatisticSet = new();
                foreach (var document in documents
                        .Where(d => d.IdSenderNavigation.UserSubdivisions
                        .FirstOrDefault(us=>us.IdSubdivision == subdivision.IdSubdivision) !=null)
                        .ToList())
                {
                    var documentBySubdivisionDto = new DocumentBySubdivisionDto();
                    var subdiv = document.IdSenderNavigation.UserSubdivisions.MaxBy(us => us.AppointmentDate).IdSubdivisionNavigation;
                    documentBySubdivisionDto.SubdivisionName = subdiv.Name;
                    documentBySubdivisionDto.SubdivisionId = subdiv.IdSubdivision;

                    var position = document.IdSenderNavigation.UserPositions.MaxBy(us => us.AppointmentDate).IdPositionNavigation;

                    documentBySubdivisionDto.PositionName = position.Name;
                    documentBySubdivisionDto.PositionId = position.IdPosition;

                    documentBySubdivisionDto.Title = document.Title;
                    documentBySubdivisionDto.AppointmentDate = document.CreateDate;
                    documentBySubdivisionDto.AllStatusTime =
                        document.DocumentStatuses.Last().AppropriationDate - document.DocumentStatuses.First().AppropriationDate;

                    documentBySubdivisionDto.CurrentStatusId = document.DocumentStatuses.Last().IdStatus;
                    documentBySubdivisionDto.CurrentStatusName = document.DocumentStatuses.Last().IdStatusNavigation.Name;

                    statisticSubdivisionSet.StatisticSet.Add(documentBySubdivisionDto);
                }
                StatisticSets.Add(statisticSubdivisionSet);
            }



            return new DocumentStatisticBySubdivisionListVm { StatisticSubdivisionSet = StatisticSets };
        }
    }
}
