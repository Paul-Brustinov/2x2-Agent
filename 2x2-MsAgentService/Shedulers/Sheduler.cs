using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using System.ServiceModel;
using _2x2_MsAgentService;
using _2x2_MsAgentService.DbLayer;
using Task = System.Threading.Tasks.Task;

namespace _2x2_MsAgentService.Shedulers
{
    [ServiceBehavior (InstanceContextMode = InstanceContextMode.Single)]
    public class Sheduler : IScheduler
    {
        //private StorageCF storageCf;

        public DbLayer.ServerDb ServerDb { get; set; }

        public Sheduler()
        {
            ServerDb = new ServerDb();
            _2x2_MsAgentService.TaskRunner.Start(ServerDb);
        }

        public DbLayer.Schedule[] GetAllShedules()
        {
            throw new NotImplementedException();
        }

        public void AddSchedule(DbLayer.Schedule scheduleItem)
        {
            throw new NotImplementedException();
        }

        public void RemoveScheduleItem(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
