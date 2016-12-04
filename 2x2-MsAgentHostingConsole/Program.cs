using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2x2_MsAgentHostingConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Это WCF
            _2x2_MsAgentService.Shedulers.Sheduler sheduler = new _2x2_MsAgentService.Shedulers.Sheduler();
            var sh = new ServiceHost(sheduler);
            sh.Open();

            Console.WriteLine("To end service, press any key");
            Console.ReadLine();
        }
    }
}
