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
    public class ProjectsController : ApiController
    {
        private IAppContext db = new AppContext();

        public ProjectsController(IAppContext context)
        {
            db = context;
        }

        public IHttpActionResult GetProjects()
        {
            List<projectView> pv = new List<projectView>();
            var y = (from c in db.Tasks
                     where c.Status == "Completed"
                     group c by c.Project_ID into ct
                     join n in db.Projects
                     on ct.FirstOrDefault().Project_ID equals n.Project_ID

                     select new
                     {
                         key1 = ct.Key,
                         cntcompleted = ct.Count(),
                     });
            var z = (from c in db.Tasks
                     group c by c.Project_ID into ct
                     join n in db.Projects
                     on ct.FirstOrDefault().Project_ID equals n.Project_ID

                     select new
                     {
                         key2 = ct.Key,
                         cnt1 = ct.Count(),
                     });

            pv = (from y1 in y
                  join n in db.Projects on y1.key1 equals n.Project_ID
                  join z1 in z
                  on n.Project_ID equals z1.key2
                  select new projectView
                  {
                      Project_ID = n.Project_ID,
                      ProjectName = n.Project1,
                      TaskCount = z1.cnt1,
                      CompletedTaskCount = y1.cntcompleted,
                      Start_Date = n.Start_Date,
                      End_Date = n.End_Date,
                      Priority = n.Priority,
                      IsCompleted = n.IsCompleted
                  }).ToList();

            return Ok(pv);
        }
        //public IHttpActionResult GetProjects()
        //{
        //    var y = (from c in db.Tasks
        //             where c.Status == "Completed"
        //             group c by c.Project_ID into ct
        //             join n in db.Projects
        //             on ct.FirstOrDefault().Project_ID equals n.Project_ID

        //             select new
        //             {
        //                 key1 = ct.Key,
        //                 cntcompleted = ct.Count(),
        //             });
        //    var z = (from c in db.Tasks
        //             group c by c.Project_ID into ct
        //             join n in db.Projects
        //             on ct.FirstOrDefault().Project_ID equals n.Project_ID

        //             select new
        //             {
        //                 key2 = ct.Key,
        //                 cnt1 = ct.Count(),
        //             });

        //    var x = (from n in db.Projects
        //             join v in (from y1 in y
        //                        join z1 in z
        //                        on y1.key1 equals z1.key2
        //                        select new { y1.key1, y1.cntcompleted, z1.cnt1 })
        //             on n.Project_ID equals v.key1
        //             into Projects
        //             from v in Projects.DefaultIfEmpty()
        //             select new projectView
        //             {
        //                 Project_ID = n.Project_ID,
        //                 ProjectName = n.Project1,
        //                 TaskCount = v.cnt1.Equals(null) ? 0 : v.cnt1,
        //                 CompletedTaskCount = v.cntcompleted.Equals(null) ? 0 : v.cntcompleted,
        //                 Start_Date = n.Start_Date,
        //                 End_Date = n.End_Date,
        //                 Priority = n.Priority,
        //                 IsCompleted = n.IsCompleted
        //             }).ToList();

        //    return Ok(x);
        //}

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            List<FilteredProject> x = new List<FilteredProject>();
            x = (from p in db.Projects
                     where p.Project_ID == id
                     join u in db.Users
                     on p.Project_ID equals u.Project_ID
                     select new FilteredProject
                     {
                         Project_ID = p.Project_ID,
                         ProjectName = p.Project1,
                         Start_Date = p.Start_Date,
                         End_Date = p.End_Date,
                         Priority = p.Priority,
                         Employee_ID = u.Employee_ID,
                         Employee_Name = u.First_Name + " " + u.Last_Name
                     }).ToList();
            return Ok(x);
        }
        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != project.Project_ID)
            {
                return BadRequest();
            }

            //db.Entry(project).State = EntityState.Modified;

                db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {

            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.Project_ID }, project);
        }

        //// DELETE: api/Projects/5
        //[ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }
        //public IHttpActionResult PostProject(ProjectTable project)
        //{
        //    if (project.IsCompleted == null)
        //    {
        //        if (project.Project_ID == null)
        //        {
        //            Project p = new Project();
        //            User user = new User();

        //            //Project Table insert
        //            p.Project1 = project.ProjectName;
        //            p.Start_Date = project.Start_Date;
        //            p.End_Date = project.End_Date;
        //            p.Priority = Convert.ToInt32(project.Priority);
        //            db.Projects.Add(p);
        //            db.SaveChanges();

        //            if (project.Employee_ID != null)
        //            {
        //                int latestProjectId = Convert.ToInt32(p.Project_ID);

        //                var Task_TableInDb = db.Users.SingleOrDefault(c => c.Employee_ID == project.Employee_ID);

        //                //User Table update
        //                Task_TableInDb.Project_ID = latestProjectId;
        //                db.SaveChanges();
        //            }
        //        }
        //        else if (project.Project_ID != null)
        //        {
        //            User p = new User();

        //            if (project.Employee_ID != null)
        //            {
        //                //Reset Project_ID in UserTable
        //                var User_TableInDb1 = db.Users.SingleOrDefault(c => c.Project_ID == project.Project_ID);
        //                if (User_TableInDb1.Project_ID != null)
        //                    User_TableInDb1.Project_ID = null;
        //                db.SaveChanges();

        //                var User_TableInDb = db.Users.SingleOrDefault(c => c.Employee_ID == project.Employee_ID);

        //                //UserTable update
        //                User_TableInDb.Project_ID = project.Project_ID;
        //                db.SaveChanges();
        //            }
        //            //Project Table Update
        //            var pr = db.Projects.SingleOrDefault(c => c.Project_ID == project.Project_ID);
        //            pr.Project1 = project.ProjectName;
        //            pr.Start_Date = project.Start_Date;
        //            pr.End_Date = project.End_Date;
        //            pr.Priority = Convert.ToInt32(project.Priority);
        //            db.SaveChanges();
        //        }
        //    }
        //    else if (project.IsCompleted == "Y")
        //    {
        //        //End Task Code
        //        var proj = db.Projects.SingleOrDefault(c => c.Project_ID == project.Project_ID);
        //        if (proj == null)
        //            throw new HttpResponseException(HttpStatusCode.NotFound);
        //        proj.IsCompleted = project.IsCompleted;
        //        db.SaveChanges();
        //    }
        //    return Ok("Success");
        //    // return CreatedAtRoute("DefaultApi", new { id = project.Project_ID }, project);
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ProjectExists(int id)
        //{
        //    return db.Projects.Count(e => e.Project_ID == id) > 0;
        //}
    }
}