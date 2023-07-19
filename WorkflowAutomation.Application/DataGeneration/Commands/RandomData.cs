using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Application.DataGeneration.Commands
{
    public class RandomData
    {
        public string GetRandomName()
        {
            Random rand = new();
            string[] names = new string[]{
    "Александр",
    "Андрей",
    "Анна",
    "Артем",
    "Борис",
    "Валентина",
    "Василий",
    "Виктор",
    "Галина",
    "Дмитрий",
    "Евгений",
    "Елена",
    "Иван",
    "Ирина",
    "Константин",
    "Лариса",
    "Леонид",
    "Максим",
    "Марина",
    "Михаил",
    "Надежда",
    "Наталья",
    "Оксана",
    "Олег",
    "Ольга",
    "Павел",
    "Роман",
    "Светлана",
    "Сергей",
    "Татьяна",
    "Юлия",
    "Яна",
    "Алексей",
    "Алина",
    "Алиса",
    "Анатолий",
    "Анжелика",
    "Антон",
    "Арина",
    "Богдан",
    "Вадим",
    "Валерий",
    "Вероника",
    "Виктория",
    "Влад",
    "Владимир",
    "Георгий",
    "Григорий",
    "Даниил",
    "Дарья"
            };
            return names[rand.Next(0, names.Length)];
        }

        public string GetRandomSurname(string name)
        {
            Random rand = new();
            string[] surnames = new string[]{
    "Иванов",
    "Петров",
    "Сидоров",
    "Смирнов",
    "Кузнецов",
    "Васильев",
    "Зайцев",
    "Попов",
    "Михайлов",
    "Федоров",
    "Соколов",
    "Яковлев",
    "Новиков",
    "Морозов",
    "Волков",
    "Алексеев",
    "Лебедев",
    "Семенов",
    "Егоров",
    "Павлов",
    "Козлов",
    "Степанов",
    "Николаев",
    "Орлов",
    "Андреев",
    "Макаров",
    "Никитин",
    "Захаров",
    "Зверев",
    "Филиппов",
    "Колесников",
    "Медведев",
    "Борисов",
    "Дмитриев",
    "Ефимов",
    "Гришин",
    "Тихонов",
    "Белов",
    "Кудрявцев",
    "Быков",
    "Герасимов",
    "Аксенов",
    "Гусев",
    "Рябов",
    "Кондратьев",
    "Лазарев",
    "Воронцов",
    "Климов",
    "Федосеев",
    "Мартынов",
    "Куликов"
};
            string lastChar = name.Substring(name.Length - 1);
            string surname = surnames[rand.Next(0, surnames.Length)];
            if (lastChar == "а" || lastChar == "я")
            {
                surname += "а";
            }
            return surname;
        }

        public string GetRandomPatronomyc(string name)
        {

            Random rand = new();
            string[,] patronomycs = new string[30, 2]
{
    {"Александрович", "Александровна"},
    {"Алексеевич", "Алексеевна"},
    {"Анатольевич", "Анатольевна"},
    {"Андреевич", "Андреевна"},
    {"Антонович", "Антоновна"},
    {"Аркадьевич", "Аркадьевна"},
    {"Борисович", "Борисовна"},
    {"Вадимович", "Вадимовна"},
    {"Валентинович", "Валентиновна"},
    {"Валерьевич", "Валерьевна"},
    {"Васильевич", "Васильевна"},
    {"Викторович", "Викторовна"},
    {"Витальевич", "Витальевна"},
    {"Владимирович", "Владимировна"},
    {"Владиславович", "Владиславовна"},
    {"Всеволодович", "Всеволодовна"},
    {"Вячеславович", "Вячеславовна"},
    {"Геннадьевич", "Геннадьевна"},
    {"Георгиевич", "Георгиевна"},
    {"Глебович", "Глебовна"},
    {"Дмитриевич", "Дмитриевна"},
    {"Евгеньевич", "Евгеньевна"},
    {"Иванович", "Ивановна"},
    {"Игоревич", "Игоревна"},
    {"Константинович", "Константиновна"},
    {"Максимович", "Максимовна"},
    {"Михайлович", "Михайловна"},
    {"Николаевич", "Николаевна"},
    {"Олегович", "Олеговна"},
    {"Павлович", "Павловна"}
};
            string patronomyc = "Максимович";
            try
            {
                string lastChar = name.Substring(name.Length - 1);
                if (lastChar == "а" || lastChar == "я")
                {
                    patronomyc = patronomycs[rand.Next(1, patronomycs.Length - 1), 1];
                }
                else
                    patronomyc = patronomycs[rand.Next(1, patronomycs.Length - 1), 0];
            }
            catch
            {
                return "Максимович";
            }
            return patronomyc;
        }

        public DateTime RandomDay()
        {
            DateTime start = DateTime.Now.AddDays(-200);
            Random gen = new();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            DateTime randomData = start.AddDays(gen.Next(range));
            return randomData;
        }

        public TimeSpan[] GenerateTimeSpans(DateTime dateStart, DateTime dateEnd, int intervalCount)
        {
            if (dateStart == null)
                dateStart = DateTime.Now; // Задайте свои значения для dateStart и dateEnd
            if (dateEnd == null)
                dateEnd = DateTime.Now.AddHours(5);

            TimeSpan totalTime = dateEnd - dateStart;
            TimeSpan minInterval = TimeSpan.FromMinutes(30);

            if (totalTime < minInterval * intervalCount)
            {
                throw new Exception($"Промежуток слишком мал, чтобы разбить на {intervalCount} интервалов.");
            }

            Random random = new Random();
            TimeSpan[] intervals = new TimeSpan[intervalCount];

            for (int i = 0; i < intervalCount - 1; i++)
            {
                TimeSpan maxInterval = totalTime - minInterval * (intervalCount - i);
                TimeSpan interval = TimeSpan.FromTicks((long)(random.NextDouble() * maxInterval.Ticks));
                intervals[i] = interval;
                totalTime -= interval;
            }

            intervals[intervalCount - 1] = totalTime;

            DateTime current = dateStart;
            for (int i = 0; i < intervalCount; i++)
            {
                DateTime intervalStart = current;
                DateTime intervalEnd = current + intervals[i];
              //  Console.WriteLine($"Промежуток {i + 1}: {intervalStart} - {intervalEnd}");

                current = intervalEnd;
            }

            TimeSpan[] Finalintervals = new TimeSpan[intervalCount-1];
            for (int i = 0; i < intervalCount - 1; i++)
            {
                Finalintervals[i] = intervals[i];
            }

            return Finalintervals;
        }
    }
}