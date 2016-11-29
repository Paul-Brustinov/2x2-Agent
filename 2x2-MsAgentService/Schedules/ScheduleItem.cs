using SqlTask;

namespace _2x2_MsAgentService.Schedules
{
    public class ScheduleItem
    {
        public ITask Task { get; set; }
        public IRegularity Regularity { get; set; }
        public int ID { get; set; }
        public ITaskResult Run() 
        {
            return Task.Run();
        }
    }
}