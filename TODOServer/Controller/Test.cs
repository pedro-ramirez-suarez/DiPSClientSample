using DiPS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOServer.Controller
{
    public class Test : DiPSController
    {
        public Test(DiPSClient client) : base(client) 
        {
        }


        public void TestMethod1(dynamic val)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Test Method {0}", val);
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void Echo(dynamic val)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Echo event {0}",val.UserName);
            Console.ForegroundColor = ConsoleColor.White;
            DiPSClient.Publish("EchoReturn", val);
        }

        
    }
}
