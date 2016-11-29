namespace _2x2_MsAgentService.Schedules
{
    public interface IRegularity
    {
        int GetDelayToNextExecution();

        bool IsRepeatable();
    }
}
