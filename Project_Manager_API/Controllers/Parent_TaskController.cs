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
    public class Parent_TaskController : ApiController
    {
        //private DBEntities db = new DBEntities();
        private IAppContext db = new AppContext();
        public Parent_TaskController(IAppContext context)
        {
            db = context;
        }


        // GET: api/Parent_Task
        public IHttpActionResult GetParent_Task()
        {
            var x = (from a in db.Parent_Task
                     orderby a.Parent_ID select new Parent_Task
                     {
                         Parent_ID=a.Parent_ID,
                         Parent_Task1=a.Parent_Task1
                     }).ToList();
            return Ok(x);
        }

        // GET: api/Parent_Task/5
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult GetParent_Task(int id)
        {
            Parent_Task parent_Task = db.Parent_Task.Find(id);
          

            return Ok(parent_Task);
        }

        // PUT: api/Parent_Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParent_Task(int id, Parent_Task parent_Task)
        {
            

            if (id != parent_Task.Parent_ID)
            {
                return BadRequest();
            }

            //db.Entry(parent_Task).State = EntityState.Modified;


                db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Parent_Task
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult PostParent_Task(Parent_Task parent_Task)
        {
         

            db.Parent_Task.Add(parent_Task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parent_Task.Parent_ID }, parent_Task);
        }

        // DELETE: api/Parent_Task/5
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult DeleteParent_Task(int id)
        {
            Parent_Task parent_Task = db.Parent_Task.Find(id);
            
            db.Parent_Task.Remove(parent_Task);
            db.SaveChanges();

            return Ok(parent_Task);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool Parent_TaskExists(int id)
        //{
        //    return db.Parent_Task.Count(e => e.Parent_ID == id) > 0;
        //}
    }
}