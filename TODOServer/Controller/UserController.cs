using DiPS.Client;
using Needletail.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOServer.Model;

namespace TODOServer.Controller
{
    public class UserController
    {
        DBTableDataSourceBase<User, Guid> repo = new DBTableDataSourceBase<User, Guid>();

        DiPSClient Client { get; set; }

        /// <summary>
        /// Subscribe to the events
        /// </summary>
        public UserController(DiPSClient client) 
        {
            Client = client;
            Client.Subscribe("login", (u) =>
            {
                Login(u.UserName.ToString());
            });
        }
        public void Login(string userName)
        {
            //check if the user exists
            var user = repo.GetSingle(where: new { UserName = userName });
            if (user == null)
            {
                user = new User { Id = Guid.NewGuid(), UserName = userName };
                repo.Insert(user);
            }            
            //return the user
            Client.Publish("loginSuccess", user);
        }
    }
}
