using DiPS.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TODOServer.Controller;
using TODOServer.Server;

namespace TODOServer
{
    class Program : DiPSBackEnd
    {

        static Program() 
        {
            //Initialize the backend, this setup the connection with DiPS and registers all the controllers with the public methods
            InitializeBackEnd();
        }

        
        static void Main(string[] args)
        {
            //DiPSClient client = new DiPSClient(ConfigurationManager.AppSettings["dipsserver"]);
            //TaskController tasks = new TaskController(client);
            //UserController users = new UserController(client);
            //Test controller = new Test(client);
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("ToDo backend");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Type exit to finish this process");
                var exit = Console.ReadLine();
                if (exit.ToLower() == "exit")
                    break;
                Console.Clear();
            }

        }
    }
}
