using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Project_Manager_API.Models;

namespace Project_Manager_API
{
    public class AppContext: DbContext,IAppContext
    {
        public AppContext() : base("name=AppContext")
        {

        }

        public DbSet<Task> Tasks { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Parent_Task> Parent_Task { get; set; }

    }
}