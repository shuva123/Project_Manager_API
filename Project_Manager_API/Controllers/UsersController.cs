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
    public class UsersController : ApiController
    {
        private IAppContext db = new AppContext();

        public UsersController(IAppContext context)
        {
            db = context;
        }

        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            var x = (from a in db.Users
                     orderby a.User_ID select new User
                    {
                        User_ID = a.User_ID,
                        First_Name =a.First_Name,
                        Last_Name = a.Last_Name,
                        Employee_ID = a.Employee_ID,
                        Task_ID = a.Task_ID,
                        Project_ID = a.Project_ID
                    }).ToList();
            return Ok(x);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
          

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
          

            if (id != user.User_ID)
            {
                return BadRequest();
            }

            //db.Entry(user).State = EntityState.Modified;


            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
         

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.User_ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool UserExists(int id)
        //{
        //    return db.Users.Count(e => e.User_ID == id) > 0;
        //}
    }
}