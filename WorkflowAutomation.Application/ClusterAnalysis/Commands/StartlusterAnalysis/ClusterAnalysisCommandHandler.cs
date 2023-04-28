using MediatR;
using System.Threading.Tasks;
using System.Threading;
using WorkflowAutomation.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using Accord.MachineLearning;
using Accord.Math.Random;
using System.Security.Cryptography;
using Accord.Math;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{
    public class ClusterAnalysisCommandHandler
        : IRequestHandler<ClusterAnalysisCommand>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClusterAnalysisCommandHandler> _logger;

        public ClusterAnalysisCommandHandler(IDocumentUserDbContext dbContext,
             IMapper mapper, ILogger<ClusterAnalysisCommandHandler> logger) =>
             (_dbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<Unit> Handle(ClusterAnalysisCommand request,
           CancellationToken cancellationToken)
        {
            Accord.Math.Random.Generator.Seed = 0;

            //            double[][] observations =
            //{
            //    new double[] { -5, -2, -1 },
            //    new double[] { -5, -5, -6 },
            //    new double[] {  2,  1,  1 },
            //    new double[] {  1,  1,  2 },
            //    new double[] {  1,  2,  2 },
            //    new double[] {  3,  1,  2 },
            //    new double[] { 11,  5,  4 },
            //    new double[] { 15,  5,  6 },
            //    new double[] { 10,  5,  6 },
            //};

            // ������� ������ ��� ��������� ��������� �����
            var random = new RNGCryptoServiceProvider();
            var rnd = new Random();
            // ������� ���� ������
            DateTime startDate = DateTime.Now.AddHours(3);

            // ������� ���� ���������, ������������ 3 �����
            DateTime endDate = startDate.AddDays(3);
            // �������� ��������� �������� ������� ����� ������� � ������
            TimeSpan timeSpan = endDate - startDate;


            var documents = await _dbContext.Documents.ToListAsync(cancellationToken);




            //var result = _dbContext.Documents
            //    .Join(
            //        _dbContext.DocumentStatuses,
            //        d => d.IdDocument,
            //        ds => ds.IdDocument,
            //        (d, ds) => new { Document = d, DocumentStatus = ds }
            //        );

            //var result2 = result
            //    .Include(x => x.DocumentStatus.IdStatusNavigation);

            //int Count = result2.Count();

            //var result3 = result2
            //    .Select(x => new { x.Document.Title, x.DocumentStatus.IdStatusNavigation.Name, x.DocumentStatus.AppropriationDate });

            //foreach (var d in result2){
            //    //_logger.LogInformation($"�������� {d.Title}: ������ {d.Name} - ���� {d.AppropriationDate}");
            //    var a = d.Document.Title;
            //}

            //��������� � ������� (Id �������� �������� �� request.StatusesIds)
            var documentStatuses = _dbContext.Documents
                .Include(x => x.DocumentStatuses
                .Where(x => request.StatusesIds
                .Contains(x.IdStatus)))
                .ToList()
                .Where(g=>g.DocumentStatuses.Count() == request.StatusesIds.Count)
                .ToList();
            
            //�������������� ���������� � ������� ��� ������������ �������������
            double[][] observations = new double[documentStatuses.Count][];

            for (int i = 0; i < documentStatuses.Count; i++)
            {
                //������ ��� ��������� ��� �� �������� ����������
                List<double> dates = new List<double>();

                //��� ������� ����� 2 = "���������������"
                var statuses = documentStatuses[i].DocumentStatuses.Where(s => s.IdStatus != 2).ToList();
                foreach (var status in statuses)
                {
                    dates.Add((status.AppropriationDate - documentStatuses[i].DocumentStatuses.First(s => s.IdStatus == 2).AppropriationDate).TotalHours);
                }
                dates.Add(documentStatuses[i].IdDocumentType);
                // TimeSpan randomInterval1 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));
                // TimeSpan randomInterval2 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));
                // TimeSpan randomInterval3 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));

                // �������� ��������� �������� � ��������� ����


                //   var FirstStatus = startDate + randomInterval1;
                //   var SecondStatus = startDate + randomInterval2;
                //   var ThirdStatus = startDate + randomInterval3;

                observations[i] = new double[statuses.Count + 1];
                for (int j = 0; j < statuses.Count + 1; j++)
                {
                    observations[i][j] = dates[j];

                }
              //  observations[i][0] = randomInterval1.TotalMinutes;
              //  observations[i][1] = randomInterval2.TotalMinutes;
              //  observations[i][2] = randomInterval3.TotalMinutes;
            }
            // ������ ���������� ���������
            int k = request.ClusterCount;


            // �������������� �������� k-�������
            var kmeans = new KMeans(k)
            {
                //Distance = Distance.Euclidean()
            };

            // ��������� �������������
            var clusters = kmeans.Learn(observations);

            // �������� ����� ��������� ��� ������� ����������
            int[] labels = clusters.Decide(observations);

            // ������� ����������
            for (int i = 0; i < observations.Length; i++)
            {
                _logger.LogInformation($"�������� {documents[i].Title}: Cluster {labels[i]}");
            }

            return Unit.Value;
        }
    }
}