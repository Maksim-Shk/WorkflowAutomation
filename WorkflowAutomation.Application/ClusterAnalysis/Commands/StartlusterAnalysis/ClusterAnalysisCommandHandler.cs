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

            // —оздаем объект дл€ генерации случайных чисел
            var random = new RNGCryptoServiceProvider();
            var rnd = new Random();
            // создать дату начала
            DateTime startDate = DateTime.Now.AddHours(3);

            // создать дату окончани€, ограниченную 3 дн€ми
            DateTime endDate = startDate.AddDays(3);
            // получить случайный интервал времени между началом и концом
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
            //    //_logger.LogInformation($"ƒокумент {d.Title}: —татус {d.Name} - ƒата {d.AppropriationDate}");
            //    var a = d.Document.Title;
            //}

            //ƒокументы и статусы (Id статусов приход€т из request.StatusesIds)
            var documentStatuses = _dbContext.Documents
                .Include(x => x.DocumentStatuses
                .Where(x => request.StatusesIds
                .Contains(x.IdStatus)))
                .ToList()
                .Where(g=>g.DocumentStatuses.Count() == request.StatusesIds.Count)
                .ToList();
            
            //характеристики документов в формате дл€ проведелени€ кластеризации
            double[][] observations = new double[documentStatuses.Count][];

            for (int i = 0; i < documentStatuses.Count; i++)
            {
                //массив дл€ получени€ дат из статусов документов
                List<double> dates = new List<double>();

                //все статусы кроме 2 = "«арегистрирован"
                var statuses = documentStatuses[i].DocumentStatuses.Where(s => s.IdStatus != 2).ToList();
                foreach (var status in statuses)
                {
                    dates.Add((status.AppropriationDate - documentStatuses[i].DocumentStatuses.First(s => s.IdStatus == 2).AppropriationDate).TotalHours);
                }
                dates.Add(documentStatuses[i].IdDocumentType);
                // TimeSpan randomInterval1 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));
                // TimeSpan randomInterval2 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));
                // TimeSpan randomInterval3 = new TimeSpan((long)(rnd.NextDouble() * timeSpan.Ticks));

                // добавить случайный интервал к начальной дате


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
            // «адаем количество кластеров
            int k = request.ClusterCount;


            // »нициализируем алгоритм k-средних
            var kmeans = new KMeans(k)
            {
                //Distance = Distance.Euclidean()
            };

            // ¬ыполн€ем кластеризацию
            var clusters = kmeans.Learn(observations);

            // ѕолучаем метки кластеров дл€ каждого наблюдени€
            int[] labels = clusters.Decide(observations);

            // ¬ыводим результаты
            for (int i = 0; i < observations.Length; i++)
            {
                _logger.LogInformation($"ƒокумент {documents[i].Title}: Cluster {labels[i]}");
            }

            return Unit.Value;
        }
    }
}