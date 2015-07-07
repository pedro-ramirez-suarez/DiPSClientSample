using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPSServerSample.Models
{
    /// <summary>
    /// This is just for demo purposes, a real app does not store the users this way
    /// </summary>
    public class ChatHelper
    {

        public static List<dynamic> OnlineUsers = new List<dynamic>();


        /// <summary>
        /// add the user to the local list
        /// </summary>
        public static void UserLogin(string userName)
        {
            if (!OnlineUsers.Any(u => u.ClientId == userName))
                OnlineUsers.Add(new { ClientId  = userName });
        }

        public static void UserLogOut(string userName)
        {
            if (OnlineUsers.Any(u => u.ClientId == userName))
            {
                var usr = OnlineUsers.First(u => u.ClientId == userName);
                OnlineUsers.Remove(usr);
            }
                
        }
    }
}
