using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Documents.Queries.GetDocumentList;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics
{
    public class GetDocumentsStatisticsQueryHandler
         : IRequestHandler<GetDocumentsStatisticsQuery, DocumentStatisticsListVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDocumentsStatisticsQueryHandler(IDocumentUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DocumentStatisticsListVm> Handle(GetDocumentsStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            var StatisticSets = new List<StatisticSets>();

            var StatusIds = new List<int> { 3, 4, 5, 6 };
            var documents = _dbContext.Documents
                .Include(d=>d.DocumentStatuses)
                .Include(d=>d.IdSenderNavigation)
                .ToList();

            var subdivisions = _dbContext.Subdivisions.Include(x => x.UserSubdivisions).ToList();
            var positions = _dbContext.Positions.Include(x => x.UserPositions).ToList();

            foreach (var statusId in StatusIds)
            {
                var statisticSet = new StatisticSets();

                statisticSet.GroupName = _dbContext.Statuses.First(s=>s.IdStatus == statusId).Name;
                statisticSet.StatisticSet = new();


                foreach (var document in documents.Where(d=>d.DocumentStatuses.Last() == d.DocumentStatuses.FirstOrDefault(s=>s.IdStatus == statusId)).ToList())
                {
                    var status = document.DocumentStatuses.FirstOrDefault(s => s.IdStatus == statusId);
                    var DocumentStatistic = new DocumentStatisticsDto();
                    DocumentStatistic.Title = document.Title;
                    DocumentStatistic.AppointmentDate = status.AppropriationDate;

                    var statuses = document.DocumentStatuses.ToList();
                    var prevStatus = statuses[statuses.IndexOf(status) - 1];
                    DocumentStatistic.StatusTime = status.AppropriationDate - prevStatus.AppropriationDate;

                    var subdivision = subdivisions.FirstOrDefault(s => s.UserSubdivisions.FirstOrDefault(us => us.IdUser == document.IdSender) != null);
                    var position = positions.FirstOrDefault(s => s.UserPositions.FirstOrDefault(us => us.IdUser == document.IdSender) != null);

                    DocumentStatistic.SubdivisionId = subdivision.IdSubdivision;
                    DocumentStatistic.SubdivisionName = subdivision.Name;

                    DocumentStatistic.PositionId = position.IdPosition;
                    DocumentStatistic.PositionName = position.Name;

                    statisticSet.StatisticSet.Add(DocumentStatistic);
                }
                StatisticSets.Add(statisticSet);
            }

            return new DocumentStatisticsListVm { StaticticSet = StatisticSets };
        }
    }
}
