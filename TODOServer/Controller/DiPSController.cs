using DiPS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOServer.Controller
{
    public abstract class DiPSController
    {

        protected DiPSClient DiPSClient { get; private set; }
        /// <summary>
        /// Ctor, we need the client to subscribe all the events in here
        /// </summary>
        public DiPSController(DiPSClient client)
        {
            DiPSClient = client;
            var myType = this.GetType();
            //get all the methods
            var methods = myType.GetMethods();
            foreach (var m in methods)
            {
                client.Subscribe(myType.Name + "." +m.Name, (param) => 
                {
                    var pars = new List<object>();
                    pars.Add(param);
                    m.Invoke(this,pars.ToArray());
                });
            }
        }


    }
}
