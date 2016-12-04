using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Task.SqlTasks
{
    public class TaskRebuildDatabaseIndex : TaskSql, ITask
    {

        public TaskRebuildDatabaseIndex(string connectionString) : base(connectionString)
        {
            this.ProcedureName = "2x2Agent_ReIndex";

            #region ProcedureCreateScript
            ProcedureCreateScript = @"---------------------------------------------------------------------
                                    create procedure [dbo].[2x2Agent_ReIndex]

                                    as
                                    set nocount on
	
                                    begin	

	                                    declare @Database varchar(255)   
	                                    declare @Table varchar(255)  
	                                    declare @cmd nvarchar(500)  
	                                    declare @fillfactor int

	                                    select @Database = db_name()

	                                    declare TableCursor cursor for 
	                                    select '[' + table_catalog + '].[' + table_schema + '].[' + table_name + ']' as tableName
	                                    from INFORMATION_SCHEMA.TABLES 
	                                    where table_type = 'BASE TABLE'

	                                    -- create table cursor  
	                                    open TableCursor   

	                                    fetch next from TableCursor into @Table   
	                                    while @@FETCH_STATUS = 0   
	                                    begin   
		                                    -- SQL 2005, 2008 command  
		                                    set @cmd = 'ALTER INDEX ALL ON ' + @Table + ' REBUILD'  
		                                    exec(@cmd)  
		                                    fetch next from TableCursor into @Table   
	                                    end

	                                    close TableCursor   
	                                    deallocate TableCursor  
                                    end

                                    ";
            #endregion ProcedureCreateScript



        }





        //public ITaskResult Run()
        //{
        //    return null;
        //}


        //DbConnection = new SqlConnection(connetionString);

    }
}
