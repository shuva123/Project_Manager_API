using Project_Manager_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_Manager_API;
using System.Data.Entity;

namespace Project_Manager_Unit_Test
{
    class TestContext : IAppContext
    {
        public TestContext()
        {
            this.Tasks = new TestTaskDbSet();
            this.Parent_Task = new TestParentTaskDbSet();
            this.Users = new TestUserDbSet();
            this.Projects = new TestProjectDbSet();
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Parent_Task> Parent_Task { get; }
        public DbSet<User> Users { get; }
        public DbSet<Project> Projects { get; }


        public int SaveChanges()
        {
            return 0;
        }
        //public void MarkAsModified(Task_Table task) { }
        //public void MarkAsModified(Parent_Task_Table parentTask) { }
        public void Dispose()
        { }
    }
}
