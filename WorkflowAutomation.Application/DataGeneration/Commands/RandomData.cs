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
    "���������",
    "������",
    "����",
    "�����",
    "�����",
    "���������",
    "�������",
    "������",
    "������",
    "�������",
    "�������",
    "�����",
    "����",
    "�����",
    "����������",
    "������",
    "������",
    "������",
    "������",
    "������",
    "�������",
    "�������",
    "������",
    "����",
    "�����",
    "�����",
    "�����",
    "��������",
    "������",
    "�������",
    "����",
    "���",
    "�������",
    "�����",
    "�����",
    "��������",
    "��������",
    "�����",
    "�����",
    "������",
    "�����",
    "�������",
    "��������",
    "��������",
    "����",
    "��������",
    "�������",
    "��������",
    "������",
    "�����"
            };
            return names[rand.Next(0, names.Length)];
        }

        public string GetRandomSurname(string name)
        {
            Random rand = new();
            string[] surnames = new string[]{
    "������",
    "������",
    "�������",
    "�������",
    "��������",
    "��������",
    "������",
    "�����",
    "��������",
    "�������",
    "�������",
    "�������",
    "�������",
    "�������",
    "������",
    "��������",
    "�������",
    "�������",
    "������",
    "������",
    "������",
    "��������",
    "��������",
    "�����",
    "�������",
    "�������",
    "�������",
    "�������",
    "������",
    "��������",
    "����������",
    "��������",
    "�������",
    "��������",
    "������",
    "������",
    "�������",
    "�����",
    "���������",
    "�����",
    "���������",
    "�������",
    "�����",
    "�����",
    "����������",
    "�������",
    "��������",
    "������",
    "��������",
    "��������",
    "�������"
};
            string lastChar = name.Substring(name.Length - 1);
            string surname = surnames[rand.Next(0, surnames.Length)];
            if (lastChar == "�" || lastChar == "�")
            {
                surname += "�";
            }
            return surname;
        }

        public string GetRandomPatronomyc(string name)
        {

            Random rand = new();
            string[,] patronomycs = new string[30, 2]
{
    {"�������������", "�������������"},
    {"����������", "����������"},
    {"�����������", "�����������"},
    {"���������", "���������"},
    {"���������", "���������"},
    {"����������", "����������"},
    {"���������", "���������"},
    {"���������", "���������"},
    {"������������", "������������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"������������", "������������"},
    {"�������������", "�������������"},
    {"������������", "������������"},
    {"������������", "������������"},
    {"�����������", "�����������"},
    {"����������", "����������"},
    {"��������", "��������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"��������", "��������"},
    {"��������", "��������"},
    {"��������������", "��������������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"����������", "����������"},
    {"��������", "��������"},
    {"��������", "��������"}
};
            string patronomyc = "����������";
            try
            {
                string lastChar = name.Substring(name.Length - 1);
                if (lastChar == "�" || lastChar == "�")
                {
                    patronomyc = patronomycs[rand.Next(1, patronomycs.Length - 1), 1];
                }
                else
                    patronomyc = patronomycs[rand.Next(1, patronomycs.Length - 1), 0];
            }
            catch
            {
                return "����������";
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
                dateStart = DateTime.Now; // ������� ���� �������� ��� dateStart � dateEnd
            if (dateEnd == null)
                dateEnd = DateTime.Now.AddHours(5);

            TimeSpan totalTime = dateEnd - dateStart;
            TimeSpan minInterval = TimeSpan.FromMinutes(30);

            if (totalTime < minInterval * intervalCount)
            {
                throw new Exception($"���������� ������� ���, ����� ������� �� {intervalCount} ����������.");
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
              //  Console.WriteLine($"���������� {i + 1}: {intervalStart} - {intervalEnd}");

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