using Accord.IO;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.DataGeneration.Commands
{
    public class GenerateDataCommandHandler : IRequestHandler<GenerateDataCommand>
    {
        private readonly IDocumentUserDbContext _dbContext;
        private readonly ILogger<GenerateDataCommandHandler> _logger;

        private HubConnection? _hubConnection;
        public GenerateDataCommandHandler(IDocumentUserDbContext dbContext, ILogger<GenerateDataCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(GenerateDataCommand request, CancellationToken cancellationToken)
        {
            var randomData = new RandomData();
            //тут должна быть очистка бд

            //тут должны быть заполнение типов документов, статусов, ролей

            var documentTypesRange = _dbContext.DocumentTypes.ToList().Select(dt=>dt.IdDocumentType).ToList();

            //var statusRange = _dbContext.Statuses.ToList().Select(s => s.IdStatus).ToList();

            var subdivisionsIds = new List<int> { 26,30,31,5,8,11,15 }; // _dbContext.Subdivisions.ToList().Select(sub => sub.IdSubdivision).ToList();

            var positionsIds = _dbContext.Positions.ToList().Select(sub => sub.IdPosition).ToList();

            var UserIds = new List<string>();
            foreach (var username in request.UsersNames)
            {
                UserIds.Add(_dbContext.AspNetUsers.FirstOrDefault(x => x.UserName == username).Id);
            }

            //if (UserIds.Count != request.UserCount)
            //{
            //    throw new Exception();
            //}
            _dbContext.AspNetUsers.Load();
            foreach (var userId in UserIds)
            {
                var user = new AppUser();
                var name = randomData.GetRandomName();
                var surnname =  randomData.GetRandomSurname(name);
                var patronomyc = randomData.GetRandomPatronomyc(name);

                user.IdUser = userId;
                user.Name = name;
                user.Surname = surnname;
                user.Patronymic = patronomyc;
                user.RegisterDate = DateTime.Now.AddYears(-1);
                user.LastOnline = DateTime.Now;
                user.RemovalDate = null;

                _dbContext.Users.Add(user);
            }
            await _dbContext.Save(cancellationToken);


            foreach (var userId in UserIds)
            {
                var rand = new Random();
                var userSubdivision = new UserSubdivision();
                userSubdivision.IdUser = userId;
                userSubdivision.AppointmentDate = _dbContext.Users.FirstOrDefault(u => u.IdUser == userId).RegisterDate;
                userSubdivision.RemovalDate = null;
                userSubdivision.IdSubdivision = subdivisionsIds[rand.Next(0, subdivisionsIds.Count)];
                _dbContext.UserSubdivisions.Add(userSubdivision);
            }

            foreach (var userId in UserIds)
            {
                var rand = new Random();
                var userPosition = new UserPosition();
                userPosition.IdUser = userId;
                userPosition.AppointmentDate = _dbContext.Users.FirstOrDefault(u => u.IdUser == userId).RegisterDate;
                userPosition.RemovalDate = null;
                userPosition.IdPosition = positionsIds[rand.Next(0, positionsIds.Count)];
                _dbContext.UserPositions.Add(userPosition);
            }
            await _dbContext.Save(cancellationToken);


            foreach (var userId in UserIds)
            {
                for (int i = 0; i < request.DocumentPerUserCount; i++)
                {
                    //новый документ
                    var sender = await _dbContext.Users.FirstOrDefaultAsync(x => x.IdUser == userId);
                    var rand = new Random();
                    var document = new Document();
                    document.IdDocumentType = documentTypesRange[rand.Next(0, documentTypesRange.Count)];
                    document.Title = "Документ (" + sender.Name + " " + sender.Patronymic + ")";
                    document.CreateDate = sender.RegisterDate;
                    document.RemoveDate = null;
                    document.UpdateDate = null;
                    List<string> randomSender = new();
                    foreach (var item in UserIds.Where(x => x != userId))
                    {
                        randomSender.Add(item);
                    }
                    document.IdSender = userId;
                    document.IdReceiver = randomSender[rand.Next(0, randomSender.Count)];
                    _dbContext.Documents.Add(document);

                    await _dbContext.Save(cancellationToken);

                    var documentContent = new DocumentContent();
                    documentContent.IdDocument = document.IdDocument;
                    documentContent.Path = "fvke2dv3.uju";
                    documentContent.Name = "ИндЗадания (1) (4).docx";
                    _dbContext.DocumentContents.Add(documentContent);


                    //статус документа - Зарегистрирован
                    var documentStatus = new DocumentStatus();
                    documentStatus.IdDocument = document.IdDocument;
                    documentStatus.IdStatus = _dbContext.Statuses.FirstOrDefault(s => s.Name == "Зарегистрирован").IdStatus;
                    documentStatus.AppropriationDate = document.CreateDate;
                    documentStatus.IdUser = userId;
                    _dbContext.DocumentStatuses.Add(documentStatus);

                    await _dbContext.Save(cancellationToken);


                    var timeSpanIntervals = randomData.GenerateTimeSpans(document.CreateDate, request.EndStatusDate, 4);

                    for (int j = 0; j < timeSpanIntervals.Length; j++)
                    {
                        var docStatus = new DocumentStatus();
                        docStatus.IdDocument = document.IdDocument;
                        if (j == 0) 
                        {
                            docStatus.IdStatus = 3;//_dbContext.Statuses.FirstOrDefault(s => s.Name == "Включен в план работ").IdStatus;
                        }
                        if (j == 1)
                        {
                            docStatus.IdStatus = 4;// _dbContext.Statuses.FirstOrDefault(s => s.Name == "В процессе выполнения").IdStatus;
                        }
                        if (j == 2)
                        {
                            docStatus.IdStatus = 5;// _dbContext.Statuses.FirstOrDefault(s => s.Name == "Выполнен").IdStatus;
                        }
                        //else //неактуально
                        //{
                        //    docStatus.IdStatus = _dbContext.Statuses.FirstOrDefault(s => s.Name == "Удален").IdStatus;
                        //}
                        DateTime AppDate = document.CreateDate;
                        for (int t = 0; t <= j; t++)
                        {
                            AppDate += timeSpanIntervals[t];
                        }
                        docStatus.AppropriationDate = AppDate;
                        if (j != 2)
                        {
                            docStatus.IdUser = randomSender[rand.Next(0, randomSender.Count)];
                        }
                        else
                        {
                            docStatus.IdUser = userId;
                        }
                        _dbContext.DocumentStatuses.Add(docStatus);
                        await _dbContext.Save(cancellationToken);
                    }
                }

                await _dbContext.Save(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
