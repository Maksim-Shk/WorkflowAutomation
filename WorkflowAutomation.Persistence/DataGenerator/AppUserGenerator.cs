//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using WorkflowAutomation.Domain;

//namespace WorkflowAutomation.Persistence.EntityTypeConfigurations
//{
//    public class RandomPersonGenerator
//    {
//        private static readonly string[] Names = { "���������", "�������", "������", "�����", "�����", "�����", "�����", "��������", "�������", "������", "�������", "��������", "��������", "��������", "�������", "�����", "�������", "�������", "����", "����" };
//        private static readonly string[] Surnames = { "������", "������", "�������", "��������", "�������", "��������", "�����", "�������", "�������", "�������", "��������", "�������", "�������", "������", "�������", "��������", "������", "��������", "������", "���������" };
//        private static readonly string[] Patronymics = { "�������������", "����������", "���������", "���������", "���������", "���������", "���������", "������������", "����������", "����������", "����������", "������������", "������������", "�����������", "����������", "���������", "����������", "����������", "��������", "��������" };

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