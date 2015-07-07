using DiPS.Client;
using DiPS.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPSServerSample
{
    /// <summary>
    /// This class is prepares the the DiPS server
    /// the client is not needed unless you may also want to send and respond to events
    /// for this sample, I will be publishing and subscribing to some events
    /// for a web application it's recommended that you subscribe to events only once not on every request
    /// </summary>
    public class Broker
    {
        static DiPSClient client;
        static bool IsInitialized;
        
        public static void Initialize(string url)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                client = new DiPSClient(url);
                
            }
        }

        public static void PublishMessage(string eventName, object parameter)
        {
            if (client == null)
                return;
            client.Publish(eventName, parameter);
        }

        public static void Subscribe(string eventName, Action<dynamic> action)
        {
            if (client == null)
                return;
            client.Subscribe(eventName, action);
        }

        public static void Unsubscribe(string eventName)
        {
            if (client == null)
                return;
            client.Unsubscribe(eventName);
        }
    }
}
