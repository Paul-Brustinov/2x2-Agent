using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//------------------>
//-----------------sdsd->
namespace SqlTask
{
    public abstract class TaskSQL : ITask
    {
        private SqlConnection _dbConnection = null;

        public string ProcedureName { get; set; }

        public string ProcedureCreateScript { get; set; }

       

        public TaskSQL(string connectionString)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString) {IntegratedSecurity = true };
            TaskDbConnection = new SqlConnection(sb.ConnectionString); 
            try
            {
                TaskDbConnection.Open();
                Console.WriteLine("Connection open!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! {0} ", ex.Message);
            }
        }

         ~TaskSQL()
        {
            //TaskDbConnection.Close();
        }


        public SqlConnection TaskDbConnection
        {
            get {return _dbConnection; }
            set {_dbConnection = value;}
        }

        public void CheckAndCreateSp()
        {
            string checkSP = String.Format("select * from sysobjects where type='P' and name='{0}'", ProcedureName);
            bool spExists = false;
            using (SqlCommand command = new SqlCommand(checkSP, TaskDbConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spExists = true;
                        break;
                    }
                }
            }
            if (!spExists) using (SqlCommand command = new SqlCommand(ProcedureCreateScript, TaskDbConnection)) {  command.ExecuteNonQuery();  }
        }

        public ITaskResult Run()
        {
            CheckAndCreateSp();
            using (SqlCommand command = new SqlCommand(ProcedureName, TaskDbConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }

            

            return null;

        }

        string ITask.ToString(){throw new NotImplementedException();}
    }
}
