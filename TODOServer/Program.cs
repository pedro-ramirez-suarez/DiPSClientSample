using DiPS.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
            var dipsRunning = false;
            while (true)
            {
                
                    Process[] processlist = Process.GetProcesses();
                    foreach (Process theprocess in processlist)
                    {
                        if (theprocess.ProcessName.ToLower().Trim().Contains("dips"))
                        {
                            dipsRunning = true;
                            break;
                        }
                    }
                    if (dipsRunning)
                        break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Seems like the DiPS server is not running, please make sure that the server is running.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press enter to retry, or another key to exit");
                    if (Console.ReadKey().Key != ConsoleKey.Enter)
                        Environment.Exit(1);
            }

            DiPSClient client = new DiPSClient(ConfigurationManager.AppSettings["dipsserver"]);
            

            TaskController tasks = new TaskController(client);
            UserController users = new UserController(client);
            
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
