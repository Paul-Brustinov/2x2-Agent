using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SqlTask;
using _2x2_MsAgentService.Schedules;
using System.Text.RegularExpressions;
using System.ServiceModel;

namespace _2x2_MsAgentService.Shedulers
{
    [ServiceBehavior (InstanceContextMode = InstanceContextMode.Single)]
    public class Sheduler : IScheduler
    {

        public Sheduler()
        {

            Schedules = new List<ScheduleItem>();

            // Это типа обычная служба, но у них ничего общего or something like this...
            string _cnnStr =
                @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=admin;Initial Catalog=Общепит_ЖД;Data Source=PAUL-PC";
            // получения строки соединения из UDL-файла
            Regex r = new Regex(@"Data Source=.*?(;|$)|Initial Catalog=.*?(;|$)");
            MatchCollection matches = r.Matches(_cnnStr);
            if (matches.Count > 0)
            {
                _cnnStr = "";
                foreach (Match m in matches) _cnnStr += m.Value;
            }
            else
            {
                Console.WriteLine("Неправильная строка соединения, отсутствуют ключи 'Data Source' и 'Initial Catalog' !");
            }

            // программа создания списка задач
            var si = new ScheduleItem
            {
                Task = new TaskRebuildDatabaseIndex(
                     _cnnStr),
                Regularity = new WeeklyRegularity()
            };
            ((WeeklyRegularity)(si.Regularity)).SetTime(17, 58);
            ((WeeklyRegularity)(si.Regularity)).AddWeekDay(DayOfWeek.Thursday);
            this.AddSchedule(si);

            // запуск самого шедулера
            this.Run();

            Console.WriteLine("Конструктор шедулера отработал...");
        }
        private List<ScheduleItem> Schedules { get; set; }

        public ScheduleItem[] GetAllShedules()
        {
            return Schedules.ToArray();
        }

        public void AddSchedule(ScheduleItem scheduleItem)
        {
            Schedules.Add(scheduleItem);
        }

        public void RemoveScheduleItem(int id)
        {
            Schedules.Remove(Schedules.Find(s => s.ID == id));
        }

        public void Run()
        {
            Schedules.ForEach(s =>
            {
                var t = new Task(() =>
                {
                    if (s.Regularity.IsRepeatable())
                    {
                        while (true)
                        {
                            Console.WriteLine(s.Regularity.GetDelayToNextExecution());
                            Thread.Sleep(s.Regularity.GetDelayToNextExecution());
                            Console.WriteLine("Индексация почалася!");
                            s.Run(); // сюда вставить логгирование
                        }
                    }
                    else
                    {
                        Thread.Sleep(s.Regularity.GetDelayToNextExecution());
                        s.Run(); // сюда вставить логгирование
                    }
                });
                t.Start();
                
            }
        );

        }
    }
}
