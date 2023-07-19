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
using Accord.Statistics.Filters;
using Accord.Statistics.Kernels;
using Accord.Math.Transforms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using WorkflowAutomation.Domain;
using Accord.Math.Distances;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{


    public class BufOriginal
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class BufNorm
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ClusterAnalysisCommandHandler
        : IRequestHandler<ClusterAnalysisCommand, OutputClustersVm>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClusterAnalysisCommandHandler> _logger;

        public ClusterAnalysisCommandHandler(IDocumentUserDbContext dbContext,
             IMapper mapper, ILogger<ClusterAnalysisCommandHandler> logger) =>
             (_dbContext, _mapper, _logger) = (dbContext, mapper, logger);


        public async Task<OutputClustersVm> Handle(ClusterAnalysisCommand request,
           CancellationToken cancellationToken)
        {
            Accord.Math.Random.Generator.Seed = 0;

            var documents = await _dbContext.Documents.ToListAsync(cancellationToken);

            //Документы и статусы (Id статусов приходят из request.StatusesIds)
            var documentStatuses = _dbContext.Documents
                .Include(x => x.DocumentStatuses
                .Where(x => request.StatusesIds
                .Contains(x.IdStatus)))
                .ToList()
                .Where(g=>g.DocumentStatuses.Count() == request.StatusesIds.Count)
                .ToList();
            
            //характеристики документов в формате для проведеления кластеризации
            double[][] observations = new double[documentStatuses.Count][];




            for (int i = 0; i < documentStatuses.Count; i++)
            {
                //массив для получения дат из статусов документов
                List<double> dates = new List<double>();

                //все статусы кроме 2 = "Зарегистрирован"
                var statuses = documentStatuses[i].DocumentStatuses.Where(s => s.IdStatus != 2).ToList();

                foreach (var status in statuses)
                {
                    dates.Add((status.AppropriationDate - documentStatuses[i].DocumentStatuses.First(s => s.IdStatus == 2).AppropriationDate).TotalMinutes);
                }
                //------------------------         dates.Add(documentStatuses[i].IdDocumentType);
                observations[i] = new double[statuses.Count];
                //------------------------    observations[i] = new double[statuses.Count + 1];
                //------------------------for (int j = 0; j < statuses.Count + 1; j++)
                for (int j = 0; j < statuses.Count; j++)
                {
                    observations[i][j] = dates[j];
                }
            }

            double[][] originalObservations = new double[observations.Length][];
            for (int i = 0; i < observations.Length; i++)
            {
                originalObservations[i] = new double[observations[i].Length];
                Array.Copy(observations[i], originalObservations[i], observations[i].Length);
            }

            var bufOriginalList = new List<BufOriginal>();
            foreach (var item in originalObservations)
            {
                bufOriginalList.Add(new BufOriginal { X = item[0], Y = item[1] });
            }

            //понижаем размерность

            // Let's create an analysis with centering (covariance method)
            // but no standardization (correlation method) and whitening:
            var pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = true
            };
            for (int i = 0; i < observations.Length; i++)
            {
                for (int j = 0; j < observations[i].Length; j++)
                {

                    observations[i][j] = Math.Round(observations[i][j], 4);
                }
            }
            // Now we can learn the linear projection from the data
            MultivariateLinearRegression transform = pca.Learn(observations);

            // Finally, we can project all the data
            // Or just its first components by setting
            // NumberOfOutputs to the desired components:
            pca.NumberOfOutputs = 2;

            // And then calling transform again:
            double[][] coords = pca.Transform(observations);



            //минимальное и максимальное значения
            double[] minValues = new double[observations[0].Length];
            double[] maxValues = new double[observations[0].Length];
            for (int j = 0; j < observations[0].Length; j++)
            {
                minValues[j] = observations[0][j];
                maxValues[j] = observations[0][j];
            }

            for (int i = 0; i < observations.Length; i++)
            {
                for (int j = 0; j < observations[i].Length; j++)
                {
                    if (observations[i][j] < minValues[j])
                    {
                        minValues[j] = observations[i][j];
                    }
                    if (observations[i][j] > maxValues[j])
                    {
                        maxValues[j] = observations[i][j];
                    }
                }
            }
          
            //нормализация от 0 до 1
            for (int i = 0; i < observations.Length; i++)
            {
                for (int j = 0; j < observations[i].Length; j++)
                {
                    observations[i][j] = (observations[i][j] - minValues[j]) / (maxValues[j] - minValues[j]);
                }
            }

            var bufNormList = new List<BufNorm>();
            foreach (var item in observations)
            {
                bufNormList.Add(new BufNorm { X = item[0], Y = item[1] });
            }


            // Задаем количество кластеров
            int k = request.ClusterCount;

            // Инициализируем алгоритм k-средних
            var kmeans = new KMeans(k)
            {
                //Distance = new SquareEuclidean(),
                //Tolerance = 0.05
            };

            // Выполняем кластеризацию
            var clusters = kmeans.Learn(coords);

            // Получаем метки кластеров для каждого наблюдения
            int[] labels = clusters.Decide(coords);

            // Выводим результаты
            for (int i = 0; i < coords.Length; i++)
            {
                _logger.LogInformation($"Документ {documents[i].Title}: Cluster {labels[i]}");
            }


            var outputClustersDtoList = new List<OutputClustersDto>();

            for (int i = 0; i < coords.Length; i++)
            {
                var document = _dbContext.Documents.First(d => d.IdDocument == documentStatuses[i].IdDocument);
                var statuses = new List<ClusterStatus>();

                //observations[i].Length - 1 - только статусы, без типа документа
                for (int j = 0; j < coords[i].Length; j++) //(var requestStatusId in request.StatusesIds.Where(x => x != 2))
                {
                    var status = new ClusterStatus
                    {
                        StatusId = request.StatusesIds[j],
                        StatusName = _dbContext.Statuses.First(s => s.IdStatus == request.StatusesIds[j]).Name,
                        StatusValue = originalObservations[i][j],
                        StatusNormaliseValue = coords[i][j],
                    };
                    statuses.Add(status);
                }
                outputClustersDtoList.Add(new OutputClustersDto
                {
                    DocumentId = documentStatuses[i].IdDocument,
                    DocumentName = document.Title,
                    ClusterId = labels[i],
                    ClusterName = "ТУТ ПУСТО",
                    DocumentType = _dbContext.DocumentTypes.First(dt => dt.IdDocumentType == document.IdDocumentType).Name,
                    Statuses = statuses,
                    X = (int)(coords[i][0] * 10000),
                    Y = (int)(coords[i][1] * 10000)
                });
            }
            //TestMethod();


            return new OutputClustersVm { DocumentClusters = outputClustersDtoList };
        }

        public void TestMethod()
        {
            // Create the aforementioned sample table
            DataTable table = new DataTable("Sample data");
            table.Columns.Add("Age", typeof(double));
            table.Columns.Add("Label", typeof(string));

            //            age   label
            table.Rows.Add(10, "child");
            table.Rows.Add(07, "child");
            table.Rows.Add(04, "child");
            table.Rows.Add(21, "adult");
            table.Rows.Add(27, "adult");
            table.Rows.Add(12, "child");
            table.Rows.Add(79, "elder");
            table.Rows.Add(40, "adult");
            table.Rows.Add(30, "adult");

            // The filter will ignore non-real (continuous) data
            Normalization normalization = new Normalization(table);

            double mean = normalization["Age"].Mean;              // 25.55
            double sdev = normalization["Age"].StandardDeviation; // 23.29

            // Now we can process another table at once:
            DataTable result = normalization.Apply(table);

            foreach (DataRow item in table.Rows)
            {
                _logger.LogInformation("age" + item["Age"].ToString());
            }

            _logger.LogInformation("--------------------------------------------------------------------");

            foreach (DataRow item in result.Rows)
            {
                _logger.LogInformation("age" + item["Age"].ToString());
            }

            // The result will be a table with the same columns, but
            // in which any column named "Age" will have been normalized
            // using the previously detected mean and standard deviation:

        }
    }
}