using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_Manager_API.Models;

namespace Project_Manager_API.Controllers
{
    public class TasksController : ApiController
    {
        private IAppContext db = new AppContext();

        public TasksController(IAppContext context)
        {
            db = context;
        }
        // GET: api/Tasks
        public IEnumerable<TaskView> GetTasks()
        {
            List<TaskView> alv = new List<TaskView>();
            var x = (from c in db.Tasks join n in db.Parent_Task
                     on c.Parent_ID equals n.Parent_ID into Tasks from n in Tasks.DefaultIfEmpty()
                     select new
                     {
                         Id = c.Task_ID,
                         Name = c.Task1,
                         Parent_Task_Name = n.Parent_Task1,
                         Project_Id=c.Project_ID,
                         Start_Date = c.Start_Date,
                         End_Date = c.End_Date,
                         Priority = c.Priority,
                         Status=c.Status
                     }).ToList();

            foreach (var item in x)
            {
                alv.Add(new TaskView
                {
                    Task_ID = item.Id,
                    Start_Date = item.Start_Date,
                    End_Date = item.End_Date,
                    Priority = item.Priority,
                    Project_ID=item.Project_Id,
                    Task1 = item.Name,
                    Status=item.Status,
                    Parent_Name = item.Parent_Task_Name
                });
            }
            return alv;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        //public IHttpActionResult GetTask(int id)
        //{
        //    List<FilteredTask> task = new List<FilteredTask>();
        //    task = (from a in db.Tasks
        //                              join c in db.Users
        //                              on a.Task_ID equals c.Task_ID into Tasks
        //                              from c in Tasks.DefaultIfEmpty()
        //                              join b in db.Parent_Task on a.Parent_ID
        //                              equals b.Parent_ID
        //                              join d in db.Projects on a.Project_ID equals d.Project_ID
        //                              where a.Task_ID==id
        //                              select new FilteredTask
        //                              {
        //                                  Id = a.Task_ID,
        //                                  Name = a.Task1,
        //                                  Parent_ID=b.Parent_ID,
        //                                  Parent_Task_Name = b.Parent_Task1,
        //                                  Project_ID=a.Project_ID,
        //                                  Start_Date=a.Start_Date,
        //                                  End_Date=a.End_Date,
        //                                  Priority=a.Priority,
        //                                  Status=a.Status,
        //                                  UserId = c.User_ID== null ?null : c.User_ID,
        //                                  UserFirstName = c.First_Name,
        //                                  UserLastName = c.Last_Name,
        //                                  Project_Name = d.Project1
        //                              }).ToList();

        //    return Ok(task);
        //}
        public IHttpActionResult GetTask(int id)
        {
            var filteredTask_Table = (from t in (from s in (
                                      from a in db.Tasks
                                      where a.Task_ID == id
                                      join c in db.Users
                                      on a.Task_ID equals c.Task_ID into Tasks
                                      from c in Tasks.DefaultIfEmpty()
                                      select new
                                      {
                                          a.Task_ID,
                                          a.Task1,
                                          a.Project_ID,
                                          a.Parent_ID,
                                          a.Start_Date,
                                          a.End_Date,
                                          a.Priority,
                                          a.Status,
                                          c.User_ID,
                                          c.First_Name,
                                          c.Last_Name
                                      })

                                                 join b in db.Parent_Task
                                                 on s.Parent_ID
                                                 equals b.Parent_ID into ParentTasks
                                                 from b in ParentTasks.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     s.Task_ID,
                                                     s.Task1,
                                                     s.Project_ID,
                                                     s.Parent_ID,
                                                     s.Start_Date,
                                                     s.End_Date,
                                                     s.Priority,
                                                     s.Status,
                                                     s.User_ID,
                                                     s.First_Name,
                                                     s.Last_Name,
                                                     b.Parent_Task1
                                                 }
                                      )
                                      join d in db.Projects
                                      on t.Project_ID equals d.Project_ID
                                      into Projects
                                      from d in Projects.DefaultIfEmpty()
                                      select new FilteredTask
                                      {
                                          Id = t.Task_ID,
                                          Name = t.Task1,
                                          Parent_ID= t.Parent_ID,
                                          Parent_Task_Name = t.Parent_Task1,
                                          Project_ID=t.Project_ID,
                                          Start_Date= t.Start_Date,
                                          End_Date=t.End_Date,
                                          Priority=t.Priority,
                                          Status=t.Status,
                                          UserId = t.User_ID == null ? null : t.User_ID,
                                          UserFirstName = t.First_Name,
                                          UserLastName = t.Last_Name,
                                          Project_Name = d.Project1
                                      }).ToList();

            return Ok(filteredTask_Table);
        }
        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
          
            if (id != task.Task_ID)
            {
                return BadRequest();
            }

            //db.Entry(task).State = EntityState.Modified;

               db.SaveChanges();
           

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
          

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.Task_ID }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            
            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool TaskExists(int id)
        //{
        //    return db.Tasks.Count(e => e.Task_ID == id) > 0;
        //}
    }
}