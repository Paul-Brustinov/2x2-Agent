using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _2x2_MsSqlAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            string _cnnStr = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=admin;Initial Catalog=Общепит_ЖД;Data Source=PAUL-PC";
            //_cnnStr = Regex.Replace(_cnnStr, @"Provider=.*?;", "");

            // получения строки соединения из UDL-файла
            Regex r = new Regex(@"Data Source=.*?(;|$)|Initial Catalog=.*?(;|$)");
            MatchCollection matches = r.Matches(_cnnStr);
            if (matches.Count > 0)
            {
                _cnnStr = "";
                foreach (Match m in matches) _cnnStr += m.Value;
            }
            else
            {
                Console.WriteLine("Неправильная строка соединения, отсутствуют ключи 'Data Source' и 'Initial Catalog' !");
            }


            //TaskRebuildDatabaseIndex t = new TaskRebuildDatabaseIndex(_cnnStr);
            //t.Run();
        }
    }
}
