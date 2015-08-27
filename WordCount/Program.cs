using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount
{




    class Program
    {
        static DirectoryInfo folder = new DirectoryInfo(ConfigurationManager.AppSettings["searchfolder"]);
        static DiPS.Client.DiPSClient client = null;
        static void Main(string[] args)
        {
            string from = Guid.NewGuid().ToString();
            Random r = new Random(DateTime.Now.Millisecond);
            try
            {
                client = new DiPS.Client.DiPSClient(ConfigurationManager.AppSettings["dipsserver"]);
                client.Subscribe("wordsearch", (m) =>
                {
                    SearchWordService(m);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(0);
            }
            while (true)
            {
                
                Console.WriteLine("Type exit to finish this process");
                var exit = Console.ReadLine();
                if (exit.ToLower() == "exit")
                    break;
                Console.Clear();
            }
        }

        private static void SearchWordService(dynamic s)
        {
            var found = new List<SearchWordResult>();
            var files = folder.GetFiles();
            int line ;
            foreach (var f in files)
            {
                if (!f.FullName.ToLower().EndsWith(".txt"))
                    return;
                line = 1;
                var sr = new StreamReader(f.FullName);
                while (!sr.EndOfStream)
                {
                    string txt = sr.ReadLine();
                    /*
					 * for some reason Mono does not like to evaluate dynamic objects on lambda expressions
					 * so we need to use a variable and use it in the .Contains method
					 */
                    string w = s.Word; 
                    if (txt.ToLower().Contains(w))
                        found.Add(new SearchWordResult { BookName = f.Name, Line = txt, LineNumber = line , Word = w }); 
                    line++;
                }

            }
            //publish the result
            client.Publish("searchresults",found);
        }

    }
}
