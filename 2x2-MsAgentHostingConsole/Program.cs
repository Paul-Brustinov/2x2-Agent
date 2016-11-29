using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SqlTask;
using _2x2_MsAgentService.Schedules;

namespace _2x2_MsAgentHostingConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Это WCF
            _2x2_MsAgentService.Shedulers.Sheduler sheduler = new _2x2_MsAgentService.Shedulers.Sheduler();
            // var sh = new ServiceHost(typeof(_2x2_MsAgentService.Shedulers.Sheduler));
            var sh = new ServiceHost(sheduler);
         //   var sheduler = (_2x2_MsAgentService.Shedulers.Sheduler) sh.SingletonInstance;
            
            

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
                Task = new TaskRebuildDatabaseIndex(_cnnStr),
                Regularity = new WeeklyRegularity()
            };
            ((WeeklyRegularity)(si.Regularity)).SetTime(13, 0);
            ((WeeklyRegularity)(si.Regularity)).AddWeekDay(DayOfWeek.Friday);
            sheduler.AddSchedule(si);

            sheduler.Run();



            sh.Open();









            Console.WriteLine("To end service, press any key");
            Console.ReadLine();
        }
    }
}
