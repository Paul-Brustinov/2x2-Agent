using System.ServiceModel;
using _2x2_MsAgentService.Schedules;

namespace _2x2_MsAgentService.Shedulers
{
    [ServiceContract]
    public interface IScheduler
    {
        [OperationContract]
        ScheduleItem[] GetAllShedules();
        [OperationContract]
        void AddSchedule(ScheduleItem scheduleItem);
        [OperationContract]
        void RemoveScheduleItem(int ID);
    }
}
