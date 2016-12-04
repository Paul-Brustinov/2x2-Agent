using System.ServiceModel;

namespace _2x2_MsAgentService.Shedulers
{
    [ServiceContract]
    public interface IScheduler
    {
        [OperationContract]
        DbLayer.Schedule[] GetAllShedules();
        [OperationContract]
        void AddSchedule(DbLayer.Schedule scheduleItem);
        [OperationContract]
        void RemoveScheduleItem(int ID);
    }
}
