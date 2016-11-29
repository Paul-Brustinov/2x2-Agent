using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlTask;

namespace _2x2_MsSqlAgentService.Schedules
{
    public class ScheduleItem
    {
        public ITask Task { get; set; }
        public IRegularity Regularity { get; set; }
        public ITaskResult Run() 
        {
            return Task.Run();
        }
    }
}