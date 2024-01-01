//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using WorkflowAutomation.Domain;

//namespace WorkflowAutomation.Persistence.EntityTypeConfigurations
//{
//    public class RandomPersonGenerator
//    {
//        private static readonly string[] Names = { "Александр", "Алексей", "Андрей", "Антон", "Артем", "Борис", "Вадим", "Валентин", "Василий", "Виктор", "Виталий", "Владимир", "Вячеслав", "Геннадий", "Георгий", "Денис", "Дмитрий", "Евгений", "Егор", "Иван" };
//        private static readonly string[] Surnames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Васильев", "Попов", "Федоров", "Морозов", "Новиков", "Алексеев", "Лебедев", "Соколов", "Козлов", "Поляков", "Николаев", "Зайцев", "Соловьев", "Волков", "Ломоносов" };
//        private static readonly string[] Patronymics = { "Александрович", "Алексеевич", "Андреевич", "Антонович", "Артемович", "Борисович", "Вадимович", "Валентинович", "Васильевич", "Викторович", "Витальевич", "Владимирович", "Вячеславович", "Геннадиевич", "Георгиевич", "Денисович", "Дмитриевич", "Евгеньевич", "Егорович", "Иванович" };

//        private static readonly Random Random = new Random();

//        public RandomPersonGenerator()
//        {
//            Name = Names[Random.Next(Names.Length)];
//            Surname = Surnames[Random.Next(Surnames.Length)];
//            Patronymic = Patronymics[Random.Next(Patronymics.Length)];
//        }

//        public List<AspNetUser> GenerateAspNetUsersSimpleUsers(int count)
//        {
//            for (int i = 0; i<count; i++)
//            {
//                var username = "TestUser" + (i + 1) + "@mail.ru";
//                var AspNetUser = new AspNetUser
//                {
//                    Id = Guid.NewGuid().ToString("D"),
//                    UserName = username,
//                    NormalizedUserName = username.ToUpper(),
//                    Email = username,
//                    NormalizedEmail = username.ToUpper(),
//                    EmailConfirmed = true,
//                    PasswordHash = 
//                }
//            }

//            return new List<AppUser>();
//        }
//    }
//}