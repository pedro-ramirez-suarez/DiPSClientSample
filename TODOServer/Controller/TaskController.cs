using DiPS.Client;
using Needletail.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODOServer.Model;
namespace TODOServer.Controller
{
    public class TaskController
    {

        DBTableDataSourceBase<Task, Guid> repo = new DBTableDataSourceBase<Task, Guid>();

        DiPSClient Client { get; set; }

        /// <summary>
        /// Ctor:
        /// Subscribe to the Events
        /// </summary>
        public TaskController(DiPSClient client)
        {
            Client = client;
            Client.Subscribe("GetTasks", (t) =>
            {
                GetTasks(Guid.Parse(t.UserId.ToString()));
            });

            Client.Subscribe("InsertTask", (t) =>
            {
                InsertTask(new Task {  Completed = t.Completed, OwnerId = t.OwnerId, Description = t.Description, ViewOrder = t.Order });
            });

            Client.Subscribe("DeleteTask", (t) =>
            {
                DeleteTask(Guid.Parse(t.Id.ToString()));
            });

            Client.Subscribe("UpdateTask", (t) =>
            {
                UpdateTask(new Task { Id = t.Id, Completed = t.Completed, OwnerId = t.OwnerId, Description = t.Description });
            });
        }


        public  void  GetTasks(Guid userId)
        {
            var tasks = repo.GetMany(where: new { OwnerId = userId }, orderBy : new { ViewOrder = "Desc" });
            //return task list
            Client.Publish("TaskListFromServer", tasks);
        }

        public void InsertTask(Task task)
        {
            task.Id = Guid.NewGuid();
            repo.Insert(task);
            //return task list
            GetTasks(task.OwnerId);
        }

        public void DeleteTask(Guid taskId)
        {
            repo.Delete(where: new { Id = taskId});
        }

        public void UpdateTask(Task task)
        {
            repo.UpdateWithWhere(values: new { Description = task.Description, Completed = task.Completed}, where: new { Id = task.Id });
            GetTasks(task.OwnerId);
        }
    }
}
