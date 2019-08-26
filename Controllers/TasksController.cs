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
        private DBEntities db = new DBEntities();

        // GET: api/Tasks
        public IEnumerable<TaskView> GetTasks()
        {
            List<TaskView> alv = new List<TaskView>();
            var x = (from c in db.Tasks
                     join n in db.Parent_Task
                     on c.Parent_ID equals n.Parent_ID into Tasks
                     from n in Tasks.DefaultIfEmpty()
                     select new
                     {
                         Id = c.Task_ID,
                         Name = c.Task1,
                         Parent_Task_Name = n.Parent_Task1,
                         Project_Id = c.Project_ID,
                         Start_Date = c.Start_Date,
                         End_Date = c.End_Date,
                         Priority = c.Priority,
                         Status = c.Status
                     }).ToList();

            foreach (var item in x)
            {
                alv.Add(new TaskView
                {
                    Task_ID = item.Id,
                    Start_Date = item.Start_Date,
                    End_Date = item.End_Date,
                    Priority = item.Priority,
                    Project_ID = item.Project_Id,
                    Task1 = item.Name,
                    Status = item.Status,
                    Parent_Name = item.Parent_Task_Name
                });
            }
            return alv;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult GetTask(int id)
        {
            var filteredTask_Table = (from a in db.Tasks
                                      join c in db.Users
                                      on a.Task_ID equals c.Task_ID into Tasks
                                      from c in Tasks.DefaultIfEmpty()
                                      join b in db.Parent_Task on a.Parent_ID
                                      equals b.Parent_ID
                                      join d in db.Projects on a.Project_ID equals d.Project_ID
                                      where a.Task_ID == id
                                      select new
                                      {
                                          Id = a.Task_ID,
                                          Name = a.Task1,
                                          b.Parent_ID,
                                          Parent_Task_Name = b.Parent_Task1,
                                          a.Project_ID,
                                          a.Start_Date,
                                          a.End_Date,
                                          a.Priority,
                                          a.Status,
                                          UserId = c.User_ID == null ? null : c.User_ID,
                                          UserFirstName = c.First_Name,
                                          UserLastName = c.Last_Name,
                                          Project_Name = d.Project1
                                      }).ToList();

            return Ok(filteredTask_Table);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Tasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.Task_ID)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult PostTask(Tasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.Task_ID }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult DeleteTask(int id)
        {
            Tasks task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.Task_ID == id) > 0;
        }
    }
}