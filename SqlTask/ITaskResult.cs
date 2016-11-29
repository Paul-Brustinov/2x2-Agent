using System;
using System.Data;

namespace SqlTask
{
    public interface ITaskResult
    {
        IDbConnection DbConnection { get; set; }
        ITask Task { get; set; }
        string Message { get; set; }
        int ErrorCode { get; set; }
        DateTime Time { get; set; }
        string ToString { get; set; }
    }
}