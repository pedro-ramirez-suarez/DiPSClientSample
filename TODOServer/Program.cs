using DiPS.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOServer.Controller;

namespace TODOServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DiPSClient client = new DiPSClient (ConfigurationManager.AppSettings["dipsserver"]);

            TaskController tasks = new TaskController(client);
            UserController users = new UserController(client);

            while (true)
            {

                Console.WriteLine("Type exit to finish this process");
                var exit = Console.ReadLine();
                if (exit.ToLower() == "exit")
                    break;
                Console.Clear();
            }

        }
    }
}
