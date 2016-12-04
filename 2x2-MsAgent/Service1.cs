using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2x2_MsAgentService.Shedulers;


namespace _2x2_MsAgent
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {


            // программа создания списка задач
            //var si = new ScheduleItem
            //{
            //    Task = new TaskRebuildDatabaseIndex(
            //        @"Provider = SQLOLEDB.1; Persist Security Info = False; User ID = admin; Initial Catalog = Общепит_ЖД; Data Source = PAUL-PC"),
            //    Regularity = new WeeklyRegularity()
            //};
            //((WeeklyRegularity)(si.Regularity)).SetTime(13, 0);
            //((WeeklyRegularity)(si.Regularity)).AddWeekDay(DayOfWeek.Friday);
            //sheduler.AddSchedule(si);

            Sheduler sheduler = new Sheduler();
            
        }

        protected override void OnStop()
        {
        }
    }
}
